using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppStudio.Parser
{
    public class RegFileExport : ParserBase
    {
        protected void GenerateRegTextFileInternal(TreeNode node, TextWriter writer)
        {
            bool writeKey = true;
            foreach (TreeNode item in node.Nodes)
            {
                Model.RegInfo regInfo = item.Tag as Model.RegInfo;
                if (regInfo == null)
                {
                    GenerateRegTextFileInternal(item, writer);
                }
                else
                {
                    if (writeKey)
                    {
                        writer.WriteLine();

                        string regKey;
                        string tempRegKey = node.FullPath;
                        if (tempRegKey.IndexOf(HKLM_SHORT) == 0)
                        {
                            regKey = string.Format("[{0}{1}]", HKLM_LONG, tempRegKey.Substring(HKLM_SHORT.Length));
                        }
                        else if (tempRegKey.IndexOf(HKCU_SHORT) == 0)
                        {
                            regKey = string.Format("[{0}{1}]", HKCU_LONG, tempRegKey.Substring(HKCU_SHORT.Length));
                        }
                        else
                        {
                            regKey = string.Format("[{0}]", tempRegKey);
                        }
                        writer.WriteLine(regKey);
                        writeKey = false;
                    }
                    string regKind;
                    string regValue;
                    GetRegFileValue(regInfo, out regKind, out regValue);
                    if (regInfo.ValueName == "@")
                    {
                        if (!string.IsNullOrWhiteSpace(regValue))
                        {
                            writer.WriteLine("{0}={1}{2}", regInfo.ValueName, regKind, regValue);
                        }
                    }
                    else
                    {
                        writer.WriteLine("\"{0}\"={1}{2}", regInfo.ValueName, regKind, regValue);
                    }
                }
            }
        }

        private void GetRegFileValue(Model.RegInfo regInfo, out string regKind, out string regValue)
        {
            regKind = "";
            regValue = "";
            switch (regInfo.Kind)
            {
                case nameof(REG_DWORD):
                    regKind = REG_DWORD;
                    regValue = regInfo.Value;
                    break;
                case nameof(REG_QWORD):
                    regKind = REG_QWORD;
                    regValue = regInfo.Value;
                    break;
                case nameof(REG_SZ):
                    regKind = "";
                    if (!string.IsNullOrWhiteSpace(regInfo.Value))
                    {
                        regValue = string.Format("\"{0}\"", regInfo.Value);
                    }
                    break;
                case nameof(REG_EXPAND_SZ):
                    regKind = REG_EXPAND_SZ;
                    regValue = regInfo.Value;
                    break;
                case nameof(REG_BINARY):
                    regKind = REG_BINARY;
                    regValue = regInfo.Value;
                    break;
                case nameof(REG_MULTI_SZ):
                    regKind = REG_MULTI_SZ;
                    regValue = regInfo.Value;
                    break;
                default:
                    regKind = regInfo.Kind;
                    regValue = regInfo.Value;
                    break;
            }
        }

        public void GenerateRegTextFile(TreeNode node, StreamWriter writer)
        {
            //byte[] bomData = { 0xFF, 0xFE };
            //writer.Write(bomData,0, bomData.Length);
            writer.WriteLine(REGEDIT5);
            GenerateRegTextFileInternal(node, writer);
        }
    }
}
