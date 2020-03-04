using Microsoft.Win32;
using PortableAppStudio.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PortableAppStudio.Parser
{
    public class RegShotParser : ParserBase
    {
        private const string REG_TYPE_VALUES = "Values";
        private const string FILE_TYPE_FILES = "Files";

        private const string ACTION_ADDED = "added";
        private const string ACTION_MODIFIED = "modified";

        private const string SECTION_START = "----------------------------------";
        private const string SECTION_END = "----------------------------------";

        //"00 00 00 00 00 00 00 00".Length = 
        private const int REG_QWORD_LENGTH = 23;

        public override void Parse(string fileName)
        {
            ParseInternal(fileName);
        }

        private void AddRegShotValue(string key, string valueName, string regKind, StringBuilder regValue)
        {
            if (key != null && valueName != null &&
                regKind != null && regValue != null)
            {
                if (regKind == nameof(REG_SZ))
                {
                    AddRegValue(key, valueName, regKind, regValue.ToString().RemoveStartEnd('"').EscapeCtrl());
                }
                else if(regKind == nameof(REG_MULTI_SZ))
                {
                    AddRegValue(key, valueName, regKind, regValue.ToString().StrToHex());
                }
                else if (regKind == nameof(REG_QWORD))
                {
                    AddRegValue(key, valueName, regKind, regValue.ToString().Replace(" ", ","));
                }
                else if (regKind == nameof(REG_BINARY))
                {
                    AddRegValue(key, valueName, regKind, regValue.ToString().Replace(" ", ","));
                }
                else
                {
                    AddRegValue(key, valueName, regKind, regValue.ToString());
                }
            }
        }

        private void ParseReshotValue(string tempVal,out string regType,out StringBuilder regValue)
        {
            regValue = new StringBuilder();
            regType = "";
            if (tempVal[0] == '"')
            {
                if (tempVal.Length > 0)
                {
                    regValue.Append(tempVal);
                }
                regType = nameof(REG_SZ);
            }
            else if (tempVal[0] == '\'')
            {
                if (tempVal.Length > 0)
                {
                    regValue.Append(tempVal);
                }
                regType = nameof(REG_MULTI_SZ);
            }
            else if (tempVal.Length > 1 && (tempVal[0] == '0' && (tempVal[1] == 'x' || tempVal[1] == 'X')))
            {
                if (tempVal.Length > 2)
                {
                    regValue.Append(tempVal.Substring(2));
                }
                regType = nameof(REG_DWORD);
            }
            else if (tempVal.Length == REG_QWORD_LENGTH)
            {
                if (tempVal.Length > 0)
                {
                    regValue.Append(tempVal);
                }
                regType = nameof(REG_QWORD);
            }
            else
            {
                if (tempVal.Length > 0)
                {
                    regValue.Append(tempVal);
                }
                regType = nameof(REG_BINARY);
            }
        }

        private void ParseInternal(string fileName)
        {
            List<string> rawLines = FileUtility.Inst.GetFileLines(fileName);

            var sectionHeader = new Regex(@"(?<Type>[a-zA-Z]+)\s+(?<Action>[a-zA-Z]+)\:\s+(?<NoOfRecord>\d+)", 
                RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.CultureInvariant | RegexOptions.Compiled);

            SectionType lastSectionType = SectionType.None;
            string lastKey = null;
            StringBuilder lastRegValue = null;
            string lastValueName = null;
            string lastValueKind = null;
            for (int lineCounter = 0; lineCounter < rawLines.Count; lineCounter++)
            {
                var tempVal = rawLines[lineCounter];

                //if(lineCounter == 66419)
                //{
                //    Debugger.Break();
                //}

                if(string.IsNullOrWhiteSpace(tempVal)) //if empty then move to next line
                {
                    AddRegShotValue(lastKey, lastValueName, lastValueKind, lastRegValue);
                    lastKey = null;
                    lastRegValue = null;
                    lastValueName = null;
                    lastValueKind = null;
                    continue;
                }
                //look for end of section then jump
                else if (tempVal.IndexOf(SECTION_END) == 0)
                {
                    AddRegShotValue(lastKey, lastValueName, lastValueKind, lastRegValue);
                    lastKey = null;
                    lastRegValue = null;
                    lastValueName = null;
                    lastValueKind = null;
                    lastSectionType = SectionType.None;
                    continue;
                }
                if (lastSectionType != SectionType.None)
                {
                    if (lastSectionType == SectionType.RegValues)
                    {
                        //skip HKU
                        if(tempVal.IndexOf(HKU_SHORT, StringComparison.InvariantCultureIgnoreCase) == 0)
                        {
                            AddRegShotValue(lastKey, lastValueName, lastValueKind, lastRegValue);
                            lastKey = null;
                            lastRegValue = null;
                            lastValueName = null;
                            lastValueKind = null;
                            continue;
                        }
                        //new value start
                        else if (tempVal.IndexOf(HKLM_SHORT, StringComparison.InvariantCultureIgnoreCase) == 0 ||
                            tempVal.IndexOf(HKCU_SHORT, StringComparison.InvariantCultureIgnoreCase) == 0)
                        {
                            AddRegShotValue(lastKey, lastValueName, lastValueKind, lastRegValue);
                            lastKey = null;
                            lastRegValue = null;
                            lastValueName = null;
                            lastValueKind = null;
                            int valuePartStartPos = tempVal.IndexOf(':');
                            if (valuePartStartPos == -1)
                            {
                                continue;
                            }
                            var keyValueNamePair = tempVal.Substring(0, valuePartStartPos);
                            lastValueName = "@";
                            if (keyValueNamePair.Length > 0 && keyValueNamePair[keyValueNamePair.Length -1] == '\\')
                            {
                                lastKey = keyValueNamePair.RemoveEnd('\\');
                            }
                            else
                            {
                                int valueNameStartPos = keyValueNamePair.LastIndexOf("\\");
                                if (valueNameStartPos == -1)
                                {
                                    lastKey = null;
                                    lastRegValue = null;
                                    lastValueName = null;
                                    lastValueKind = null;
                                    continue;
                                }
                                else
                                {
                                    lastKey = keyValueNamePair.Substring(0, valueNameStartPos);
                                    int valueNameLen = valuePartStartPos- valueNameStartPos;
                                    if (valueNameLen > 0)
                                    {
                                        lastValueName = tempVal.Substring(valueNameStartPos + 1, valueNameLen - 1).RemoveEnd('\\');

                                        if(string.IsNullOrWhiteSpace(lastValueName))
                                        {
                                            Debugger.Break();
                                        }

                                    }
                                    else
                                    {
                                        lastKey = null;
                                        lastRegValue = null;
                                        lastValueName = null;
                                        lastValueKind = null;
                                        continue;
                                    }
                                }
                            }

                            if(lastKey == null || lastValueName == null)
                            {
                                lastKey = null;
                                lastRegValue = null;
                                lastValueName = null;
                                lastValueKind = null;
                                continue;
                            }

                            var tempRegValue = tempVal.Substring(valuePartStartPos + 1).Trim();

                            ParseReshotValue(tempRegValue, out lastValueKind, out lastRegValue);
                        }
                        //Last Value corrupt REG_SZ
                        else
                        {
                            if(lastKey != null && lastRegValue != null &&
                                lastValueKind != null && lastRegValue != null)
                            {
                                lastRegValue.Append(tempVal);
                            }
                        }
                    }
                    else if (lastSectionType == SectionType.Files)
                    {
                        AddFile(string.Format(@"\\?\{0}",tempVal.Trim()));
                    }
                }
                else
                {
                    lastSectionType = SectionType.None;
                    var match = sectionHeader.Match(tempVal);
                    if(match != null && match.Success)
                    {
                        var actionName = match.Groups["Action"].Value;
                        var typeName = match.Groups["Type"].Value;
                        var noOfRecords = int.Parse(match.Groups["NoOfRecord"].Value);

                        if(typeName == REG_TYPE_VALUES && actionName == ACTION_ADDED) //beginning of registry values 
                        {
                            lastSectionType = SectionType.RegValues;
                            lineCounter++;
                        }
                        else if(typeName == FILE_TYPE_FILES && actionName == ACTION_ADDED) //beginning of file added
                        {
                            lastSectionType = SectionType.Files;
                            lineCounter++;
                        }
                    }
                }
            }
        }
    }
}
