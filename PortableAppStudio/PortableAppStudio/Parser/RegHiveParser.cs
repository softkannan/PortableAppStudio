using Microsoft.Win32;
using PortableAppStudio.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Parser
{
    public class RegHiveParser : ParserBase
    {
        private bool IsSupported(RegistryValueKind kind)
        {
            bool retVal = false;
            switch (kind)
            {
                case RegistryValueKind.DWord:
                case RegistryValueKind.QWord:
                case RegistryValueKind.String:
                case RegistryValueKind.ExpandString:
                case RegistryValueKind.Binary:
                case RegistryValueKind.MultiString:
                    retVal = true;
                    break;
            }
            return retVal;
        }

        private string RegKindToStr(RegistryValueKind kind)
        {
            string retVal = "";
            switch (kind)
            {
                case RegistryValueKind.DWord:
                    retVal = "REG_DWORD";
                    break;
                case RegistryValueKind.QWord:
                    retVal = "REG_QWORD";
                    break;
                case RegistryValueKind.String:
                    retVal = "REG_SZ";
                    break;
                case RegistryValueKind.ExpandString:
                    retVal = "REG_EXPAND_SZ";
                    break;
                case RegistryValueKind.Binary:
                    retVal = "REG_BINARY";
                    break;
                case RegistryValueKind.MultiString:
                    retVal = "REG_MULTI_SZ";
                    break;
            }
            return retVal;
        }


        private string RegObjToStr(RegistryValueKind kind, object objValue)
        {
            string retVal = "";
            switch (kind)
            {
                case RegistryValueKind.DWord:
                    {
                        retVal = string.Format("{0:x8}", objValue);
                    }
                    break;
                case RegistryValueKind.String:
                    {
                        var tempVal = objValue.ToString();
                        if (string.IsNullOrWhiteSpace(tempVal))
                        {
                            retVal = "";
                        }
                        else
                        {
                            retVal = tempVal.ToLiteral();
                        }
                    }
                    break;
                case RegistryValueKind.ExpandString:
                    {
                        string tempVal = objValue.ToString();
                        retVal = tempVal.StrToHex();
                    }
                    break;
                case RegistryValueKind.QWord:
                    {
                        StringBuilder lineStr = new StringBuilder("");
                        if (objValue is long)
                        {
                            long tempVal = (long)objValue;
                            foreach (var item in BitConverter.GetBytes(tempVal))
                            {
                                lineStr.AppendFormat("{0:x2}", item);
                                lineStr.Append(",");
                            }
                        }
                        retVal = lineStr.ToString().TrimEnd(',');
                    }
                    break;
                case RegistryValueKind.Binary:
                    {
                        byte[] tempVal = objValue as byte[];
                        StringBuilder lineStr = new StringBuilder("");
                        if (tempVal != null)
                        {
                            foreach (var item in tempVal)
                            {
                                lineStr.AppendFormat("{0:x2}", item);
                                lineStr.Append(",");
                            }
                        }
                        retVal = lineStr.ToString().TrimEnd(',');
                    }
                    break;
                case RegistryValueKind.MultiString:
                    {
                        string[] tempVal = objValue as string[];
                        StringBuilder lineStr = new StringBuilder("");
                        if (tempVal != null)
                        {
                            foreach (var item in tempVal)
                            {
                                var hexCodedString = item.StrToHex();
                                if (!string.IsNullOrWhiteSpace(hexCodedString))
                                {
                                    lineStr.Append(hexCodedString);
                                    lineStr.Append(",");
                                }
                            }
                            lineStr.Append("00");
                        }
                        retVal = lineStr.ToString().TrimEnd(',');
                    }
                    break;
            }
            return retVal;
        }


        protected void ParseAppVBinaryRegValues(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName) || (!File.Exists(fileName)))
            {
                return;
            }

            string tempDestRegBinFile = string.Format("{0}\\DestRoot.dat", Path.GetTempPath());
            File.Copy(PathManager.Init.GetResourcePath(PathManager.REGHIVE_FILE), tempDestRegBinFile, true);

            using (RegistryKey srcRootKey = RegistryNativeMethods.RegLoadAppKey(fileName))
            using (RegistryKey destRootKey = RegistryNativeMethods.RegLoadAppKey(tempDestRegBinFile, RegistryNativeMethods.RegSAM.KEY_ALL_ACCESS))
            {
                if (srcRootKey != null && destRootKey != null)
                {
                    using (RegistryKey sourceKey = srcRootKey.OpenSubKey("REGISTRY\\USER\\[{AppVCurrentUserSID}]", false))
                    using (RegistryKey destKey = destRootKey.CreateSubKey("HKCU"))
                    {
                        sourceKey.CopyKey(destKey);
                    }

                    using (RegistryKey sourceKey = srcRootKey.OpenSubKey("REGISTRY\\USER\\[{AppVCurrentUserSID}]_Classes", false))
                    using (RegistryKey destKey = destRootKey.CreateSubKey(@"HKCU\Software\Classes"))
                    {
                        sourceKey.CopyKey(destKey);
                    }

                    using (RegistryKey sourceKey = srcRootKey.OpenSubKey("REGISTRY\\MACHINE", false))
                    using (RegistryKey destKey = destRootKey.CreateSubKey("HKLM"))
                    {
                        sourceKey.CopyKey(destKey);
                    }
                    string tempKey = "";
                    AddRecurseRegValue(destRootKey, tempKey);
                }
            }
        }

        private void AddRecurseRegValue(RegistryKey sourceKey, string key)
        {
            foreach (string valueName in sourceKey.GetValueNames())
            {
                object objValue = sourceKey.GetValue(valueName, null, RegistryValueOptions.DoNotExpandEnvironmentNames);
                if (objValue != null)
                {
                    RegistryValueKind valKind = sourceKey.GetValueKind(valueName);
                    if (IsSupported(valKind))
                    {
                        string tempValName = valueName;
                        if (string.IsNullOrWhiteSpace(valueName))
                        {
                            tempValName = "@";
                        }
                        AddRegValue(key, tempValName, RegKindToStr(valKind), RegObjToStr(valKind, objValue));
                    }
                }
            }
            foreach (string sourceSubKeyName in sourceKey.GetSubKeyNames())
            {
                //if(sourceSubKeyName == "InprocServer32")
                //{
                //    Debugger.Break();
                //}

                using (RegistryKey sourceSubKey = sourceKey.OpenSubKey(sourceSubKeyName))
                {
                    string subKey;
                    if (string.IsNullOrWhiteSpace(key))
                    {
                        subKey = sourceSubKeyName;
                    }
                    else
                    {
                        subKey = string.Format("{0}\\{1}", key, sourceSubKeyName);
                    }
                    AddRecurseRegValue(sourceSubKey, subKey);
                }
            }
        }
    }
}
