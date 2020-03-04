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
    public class ThinAppParser : ParserBase
    {
        private const string KEY_HEADER = "isolation";

        private const string VALUE_HEADER = "Value=";
        private const string VALUE_HEADER_EX = "Value~";
        
        private const string REG_DWORD_HEADER = "REG_DWORD=";
        private const string REG_SZ_HEADER = "REG_SZ~";
        private const string REG_BINARY_HEADER = "REG_BINARY=";
        private const string REG_QWORD_HEADER = "REG_QWORD=";
        private const string REG_MULTI_SZ_HEADER = "REG_MULTI_SZ~";
        private const string REG_EXPAND_SZ_HEADER = "REG_EXPAND_SZ~";

        public ThinAppParser() : base()
        {
            RelativePathMap = new Dictionary<string, string> {
            {"%AppData%","%APPDATA%" },
            {"%Common AppData%","%PROGRAMDATA%"},
            {"%Local AppData%","%LOCALAPPDATA%"},
            {"%Local AppData%Low","%LOCALLOWAPPDATA%"},
            {"%USERPROFILE%","%USERPROFILE%"},
            {"%Program Files Common(x64)%","%COMMONPROGRAMFILES%"},
            {"%Program Files Common%","%COMMONPROGRAMFILES(x86)%"},
            {"%ProgramFilesDir(x64)%","%PROGRAMFILES%"},
            {"%ProgramFilesDir%","%PROGRAMFILES(X86)%"}
            };

            if(Environment.Is64BitProcess)
            {
                RelativePathMap.Add("%Program Files Common(x64)%", "%CommonProgramW6432%");
                RelativePathMap.Add("%Program Files Common%", "%COMMONPROGRAMFILES(x86)%");
                RelativePathMap.Add("%ProgramFilesDir(x64)%", "%ProgramW6432%");
                RelativePathMap.Add("%ProgramFilesDir%", "%PROGRAMFILES(X86)%");
            }
            else
            {
                RelativePathMap.Add("%Program Files Common%", "%COMMONPROGRAMFILES%");
                RelativePathMap.Add("%ProgramFilesDir%", "%PROGRAMFILES%");
            }

        }


        public override void Parse(string fileName)
        {
            ParseInternal(fileName);
        }

        private void ParseRegValues(string fileName)
        {
            List<string> rawLines = FileUtility.Inst.GetFileLines(fileName);
            
            string lastKey = "";
            string lastValueName = null;

            for(int lineCounter = 0; lineCounter < rawLines.Count; lineCounter++)
            {
                var tempVal = rawLines[lineCounter];

                //if (lineCounter == 1026)
                //{
                //    Debugger.Break();
                //}

                if (string.IsNullOrWhiteSpace(tempVal)) //if empty then move to next line
                {
                    lastKey = "";
                    lastValueName = null;
                    continue;
                }

                if (lastKey.Length == 0 && tempVal.IndexOf(KEY_HEADER) == 0) // beginning of key
                {
                    var keyStartPos = tempVal.IndexOf(' ');
                    var tempKey = tempVal.Substring(keyStartPos);
                    if (tempKey.IndexOf(HKLM_LONG, StringComparison.InvariantCultureIgnoreCase) == 0)
                    {
                        lastKey = string.Format("{0}{1}", HKLM_SHORT, tempKey.Substring(HKLM_LONG.Length));
                    }
                    else if (tempKey.IndexOf(HKCU_LONG, StringComparison.InvariantCultureIgnoreCase) == 0)
                    {
                        lastKey = string.Format("{0}{1}", HKCU_SHORT, tempKey.Substring(HKCU_LONG.Length));
                    }
                    else
                    {
                        lastKey = tempKey;
                    }
                }
                else
                {
                    var localVal = tempVal.Trim();
                    if (lastValueName == null && localVal.IndexOf(VALUE_HEADER) == 0)
                    {
                        lastValueName = "@";
                        if (localVal.Length > VALUE_HEADER.Length)
                        {
                            lastValueName = localVal.Substring(VALUE_HEADER.Length);
                        }
                    }
                    else if (lastValueName == null && localVal.IndexOf(VALUE_HEADER_EX) == 0)
                    {
                        lastValueName = "@";
                        if (localVal.Length > VALUE_HEADER_EX.Length)
                        {
                            lastValueName = localVal.Substring(VALUE_HEADER_EX.Length);
                        }
                    }
                    else
                    {
                        var regValue = "";
                        var regType = nameof(REG_SZ);
                        if (localVal.IndexOf(REG_SZ_HEADER) == 0)
                        {
                            regType = nameof(REG_SZ);
                            if (localVal.Length > REG_SZ_HEADER.Length)
                            {
                                var tempValue = localVal.Substring(REG_SZ_HEADER.Length);
                                if(tempVal.Length > 0)
                                {
                                    regValue = tempVal.RemoveStartEnd('"').ThinAppToLiteral().ToLiteral();
                                }
                            }
                        }
                        else if (localVal.IndexOf(REG_DWORD_HEADER) == 0)
                        {
                            regType = nameof(REG_DWORD);
                            if (localVal.Length > REG_DWORD_HEADER.Length)
                            {
                                tempVal = localVal.Substring(REG_DWORD_HEADER.Length);
                                if (tempVal.Length > 0)
                                {
                                    regValue = tempVal.Replace("#", ",").HexToDec();
                                }
                            }
                        }
                        else if (localVal.IndexOf(REG_EXPAND_SZ_HEADER) == 0)
                        {
                            regType = nameof(REG_EXPAND_SZ);
                            if (localVal.Length > REG_EXPAND_SZ_HEADER.Length)
                            {
                                regValue = localVal.Substring(REG_EXPAND_SZ_HEADER.Length);
                            }
                        }
                        else if (localVal.IndexOf(REG_MULTI_SZ_HEADER) == 0)
                        {
                            regType = nameof(REG_MULTI_SZ);
                            if (localVal.Length > REG_MULTI_SZ_HEADER.Length)
                            {
                                regValue = localVal.Substring(REG_MULTI_SZ_HEADER.Length);
                            }
                        }
                        else if (localVal.IndexOf(REG_QWORD_HEADER) == 0)
                        {
                            regType = nameof(REG_QWORD);
                            if (localVal.Length > REG_QWORD_HEADER.Length)
                            {
                                tempVal = localVal.Substring(REG_QWORD_HEADER.Length);
                                if (tempVal.Length > 0)
                                {
                                    regValue = tempVal.Replace("#", ",");
                                }
                            }
                        }
                        else if (localVal.IndexOf(REG_BINARY_HEADER) == 0)
                        {
                            regType = nameof(REG_BINARY);
                            if (localVal.Length > REG_BINARY_HEADER.Length)
                            {
                                tempVal = localVal.Substring(REG_BINARY_HEADER.Length);
                                if (tempVal.Length > 0)
                                {
                                    regValue = tempVal.Replace("#", ",");
                                }
                            }
                        }

                        AddRegValue(lastKey, lastValueName, regType, regValue);

                        lastValueName = null;
                    }
                }
            }
        }

        private void ParseInternal(string folderName)
        {
            var regHKCUFile = string.Format(@"{0}\HKEY_CURRENT_USER.txt", folderName);
            var regHKLMFile = string.Format(@"{0}\HKEY_LOCAL_MACHINE.txt", folderName);

            if (File.Exists(regHKCUFile))
            {
                ParseRegValues(regHKCUFile);
            }

            if (File.Exists(regHKLMFile))
            {
                ParseRegValues(regHKLMFile);
            }

            ParseFiles(folderName);
        }
    }
}
