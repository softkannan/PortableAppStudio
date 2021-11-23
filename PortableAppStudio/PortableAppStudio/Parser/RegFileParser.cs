using Microsoft.Win32;
using PortableAppStudio.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppStudio.Parser
{
    public class RegFileParser : ParserBase
    {
        private Regex customKindPattern = new Regex("hex\\((?<REGKIND>\\d+)\\)\\:(?<Value>.+)", RegexOptions.CultureInvariant | RegexOptions.Compiled | RegexOptions.Singleline);

        protected RegFileType _fileType = RegFileType.UNKNOWN;

        protected enum RegFileType
        {
            UNKNOWN,
            REGEDIT5,
            REGEDIT4
        }

        protected enum RegAction
        {
            Skip,
            Write,
            Continue
        }

        protected void ParseTextFileRegValue(string regValueLine,out string regValueName,out string regKind, ref StringBuilder regValue)
        {
            regValueName = string.Empty;
            regKind = nameof(REG_SZ);
            regValue = new StringBuilder();
            Match match = null;

            if(string.IsNullOrWhiteSpace(regValueLine))
            {
                return;
            }
            var valueStartPos = -1;
            if (regValueLine.IndexOf("@=") == 0)
            {
                regValueName = "@";
                valueStartPos = 2;
            }
            else
            {
                valueStartPos = regValueLine.IndexOf("\"=");
                //if unable to find separator then just append
                if (valueStartPos == -1)
                {
                    regValue.Append(regValueLine);
                    return;
                }
                regValueName = regValueLine.Substring(1, valueStartPos - 1);
                valueStartPos += 2;
            }
            var tempRegVal = regValueLine.Substring(valueStartPos);
            if (string.IsNullOrWhiteSpace(tempRegVal))
            {
                return;
            }
            else if (tempRegVal[0] == '"')
            {
                regValue.Append(tempRegVal.Substring(1));
            }
            else if (tempRegVal.IndexOf(REG_DWORD, StringComparison.OrdinalIgnoreCase) == 0)
            {
                regKind = nameof(REG_DWORD);
                regValue.Append(tempRegVal.Substring(REG_DWORD.Length));
            }
            else if (tempRegVal.IndexOf(REG_QWORD, StringComparison.OrdinalIgnoreCase) == 0)
            {
                regKind = nameof(REG_QWORD);
                if (tempRegVal.Length > REG_QWORD.Length)
                {
                    regValue.Append(tempRegVal.Substring(REG_QWORD.Length));
                }
            }
            else if (tempRegVal.IndexOf(REG_MULTI_SZ, StringComparison.OrdinalIgnoreCase) == 0)
            {
                regKind = nameof(REG_MULTI_SZ);
                if (tempRegVal.Length > REG_MULTI_SZ.Length)
                {
                    regValue.Append(tempRegVal.Substring(REG_MULTI_SZ.Length));
                }
            }
            else if (tempRegVal.IndexOf(REG_EXPAND_SZ, StringComparison.OrdinalIgnoreCase) == 0)
            {
                regKind = nameof(REG_EXPAND_SZ);
                if (tempRegVal.Length > REG_EXPAND_SZ.Length)
                {
                    regValue.Append(tempRegVal.Substring(REG_EXPAND_SZ.Length)); 
                }
            }
            else if (tempRegVal.IndexOf(REG_BINARY, StringComparison.OrdinalIgnoreCase) == 0)
            {
                regKind = nameof(REG_BINARY);
                if (tempRegVal.Length > REG_BINARY.Length)
                {
                    regValue.Append(tempRegVal.Substring(REG_BINARY.Length));
                }
            }
            else if (tempRegVal.IndexOf(REG_CUSTOM, StringComparison.OrdinalIgnoreCase) == 0)
            {
                match = customKindPattern.Match(tempRegVal);
                if (match != null && match.Success)
                {
                    int tempRegKind;
                    int.TryParse(match.Groups["REGKIND"].Value, out tempRegKind);
                    switch (tempRegKind)
                    {
                        case 40000:
                            regKind = nameof(REG_CUSTOM);
                            break;
                        case 40001:
                        default:
                            regKind = nameof(REG_CUSTOM);
                            regValue.Append(match.Groups["Value"].Value);
                            break;
                        case 400002:
                            regKind = nameof(REG_EXPAND_SZ);
                            regValue.Append(match.Groups["Value"].Value);
                            break;
                        case 400007:
                            regKind = nameof(REG_MULTI_SZ);
                            regValue.Append(match.Groups["Value"].Value);
                            break;
                    }
                }
                else
                {
                    regValue.Append(tempRegVal);
                }
            }
            else
            {
                regValue.Append(tempRegVal);
            }
        }
        private void AddRegTextFileValue(string key, string valueName, string regKind, StringBuilder regValue)
        {
            if (key != null && valueName != null &&
                                    regKind != null && regValue != null)
            {
                if(regKind == nameof(REG_SZ))
                {
                    AddRegValue(key, valueName, regKind, regValue.ToString().RemoveEnd('"').EscapeCtrl());
                }
                else if(regKind == nameof(REG_EXPAND_SZ))
                {
                    AddRegValue(key, valueName, regKind, regValue.ToString().Replace("\\",""));
                }
                else if(regKind == nameof(REG_MULTI_SZ))
                {
                    AddRegValue(key, valueName, regKind, regValue.ToString().Replace("\\", ""));
                }
                else if(regKind == nameof(REG_BINARY))
                {
                    AddRegValue(key, valueName, regKind, regValue.ToString().Replace("\\", ""));
                }
                else if(regKind == nameof(REG_CUSTOM))
                {
                    string finalRegValue = regValue.ToString().Replace("\\", "").HexToStr(',',codingType);
                    AddRegValue(key, valueName, nameof(REG_SZ), finalRegValue);
                }
                else
                {
                    AddRegValue(key, valueName, regKind, regValue.ToString());
                }
            }
        }

        public override void Parse(string fileName)
        {
            this.ParserType = RegSourceType.RegFile;

            ParseTextRegFile(fileName);
        }
        /// <summary>
        /// Windows Registry Editor Version 5.00 
        /// [<Hive name>\<Key name>\<Subkey name>]
        /// "Value name"=<Value type>:<Value data>
        /// </summary>
        /// <param name="fileName"></param>
        protected void ParseTextRegFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName) || (!File.Exists(fileName)))
            {
                return;
            }

            string lastKey = null;
            StringBuilder lastRegValue = null;
            string lastValueName = null;
            string lastValueKind = null;
            int lineCounter = 0;

            using (TextReader reader = new StreamReader(fileName))
            {
                while (true)
                {
                    var item = reader.ReadLine();
                    lineCounter++;
                    //if (lineCounter == 14)
                    //{
                    //    Debugger.Break();
                    //}

                    if (item == null)
                    {
                        AddRegTextFileValue(lastKey, lastValueName, lastValueKind, lastRegValue);
                        break;
                    }
                    //as of now this is not supported
                    else if(item == REGEDIT4)
                    {
                        _fileType = RegFileType.REGEDIT4;
                        codingType = StringExtensionMethods.CodingType.ASCII;
                        return;
                    }
                    else if(item == REGEDIT5)
                    {
                        _fileType = RegFileType.REGEDIT5;
                        codingType = StringExtensionMethods.CodingType.Unicode;
                        continue;
                    }
                    //empty lines skip
                    else if (item == string.Empty)
                    {
                        AddRegTextFileValue(lastKey, lastValueName, lastValueKind, lastRegValue);
                        lastKey = null;
                        lastRegValue = null;
                        lastValueKind = null;
                        lastValueName = null;
                        continue;
                    }
                    //comment lines skip
                    else if (item[0] == ';')
                    {
                        continue;
                    }
                    //new key
                    else if (item[0] == '[')
                    {
                        AddRegTextFileValue(lastKey, lastValueName, lastValueKind, lastRegValue);
                        lastRegValue = null;
                        lastValueKind = null;
                        lastValueName = null;
                        var tempKey = item.RemoveStartEnd('[', ']');
                        if (string.IsNullOrWhiteSpace(tempKey))
                        {
                            continue;
                        }
                        else if (tempKey.IndexOf(HKLM_LONG, StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            lastKey = string.Format("{0}{1}", HKLM_SHORT, tempKey.Substring(HKLM_LONG.Length));
                        }
                        else if (tempKey.IndexOf(HKCU_LONG, StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            lastKey = string.Format("{0}{1}", HKCU_SHORT, tempKey.Substring(HKCU_LONG.Length));
                        }
                        else
                        {
                            lastKey = tempKey;
                        }
                    }
                    //continue of last binary type value
                    else if (item.Length > 3 && item.IndexOf("  ") == 0 && item[2] != ' ')
                    {
                        if (lastRegValue != null && lastValueName != null &&
                                    lastValueKind != null && lastKey != null)
                        {
                            string tempVal = item.Trim();
                            if (string.IsNullOrWhiteSpace(tempVal) || tempVal[tempVal.Length - 1] != '\\')
                            {
                                if (!string.IsNullOrWhiteSpace(tempVal))
                                {
                                    lastRegValue.Append(tempVal);
                                }
                                AddRegTextFileValue(lastKey, lastValueName, lastValueKind, lastRegValue);
                                lastRegValue = null;
                                lastValueKind = null;
                                lastValueName = null;
                            }
                            else
                            {
                                lastRegValue.Append(tempVal.Substring(0, tempVal.Length - 1));
                            }
                        }
                    }
                    //reg default value start 
                    else if (item.IndexOf("@=") == 0)
                    {
                        AddRegTextFileValue(lastKey, lastValueName, lastValueKind, lastRegValue);
                        lastRegValue = new StringBuilder();
                        lastValueKind = null;
                        lastValueName = null;
                        ParseTextFileRegValue(item, out lastValueName, out lastValueKind, ref lastRegValue);
                    }
                    //reg value name value start
                    else if(item[0] == '"' && item.IndexOf("\"=") > 0)
                    {
                        AddRegTextFileValue(lastKey, lastValueName, lastValueKind, lastRegValue);
                        lastRegValue = new StringBuilder();
                        lastValueKind = null;
                        lastValueName = null;
                        ParseTextFileRegValue(item, out lastValueName, out lastValueKind, ref lastRegValue);
                    }
                    // some corrupt / multiline value as REG_SZ / this is 
                    else
                    {
                        if (lastRegValue != null && lastValueName != null &&
                                    lastValueKind != null && lastKey != null)
                        {
                            lastRegValue.Append("\r\n");
                            lastRegValue.Append(item);
                        }
                    }
                }
            }
        }
    }
}
