using PortableAppStudio.Controls;
using PortableAppStudio.Dialogs;
using PortableAppStudio.Model.AppInfoINI;
using PortableAppStudio.Model.FolderLayout;
using PortableAppStudio.Model.LaunchINI;
using PortableAppStudio.Parser;
using PortableAppStudio.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppStudio
{
    partial class Studio
    {
        public void UpdateRegDynamicIntelisense()
        {
            Model.COMComponentsListStringConverter.COMComponentsList.Clear();
            Model.COMComponentsListStringConverter.COMComponentsList.Add("");

            var regNode = sourceRegTree.Nodes.FindNode("App");
            if (regNode == null)
            {
                return;
            }

            List<string> comComponentsList = new List<string>();
            Model.COMComponentsListStringConverter.COMComponentsList.AddRange(comComponentsList.Distinct());
        }

        private void UpdateRegDynamicIntelisenseInternal(TreeNode appNode, List<string> comComponentsList)
        {

        }

        public void UpdateFileDynamicIntelisense()
        {
            Model.ExeFileListStringConverter.ExeList.Clear();
            Model.ExeFileListStringConverter.ExeList.Add("");
            Model.ExeFileNameListStringConverter.ExeFileNameList.Clear();
            Model.ExeFileNameListStringConverter.ExeFileNameList.Add("");
            Model.AppPathListStringConverter.AppPath.Clear();
            Model.AppPathListStringConverter.AppPath.Add("");
            Model.OtherFileListStringConverter.OtherList.Clear();
            Model.OtherFileListStringConverter.OtherList.Add("");
            Model.DllOcxFileListStringConverter.DllOcxList.Clear();
            Model.DllOcxFileListStringConverter.DllOcxList.Add("");
            Model.FirewallListStringConverter.FirewallList.Clear();
            Model.FirewallListStringConverter.FirewallList.Add("");

            var appNode = appFilesTree.Nodes.FindNode("App");
            if (appNode == null)
            {
                return;
            }

            List<string> exeList = new List<string>();
            List<string> firewallList = new List<string>();
            List<string> exeFileNameList = new List<string>();
            List<string> appPathList = new List<string>();
            List<string> otherFilesList = new List<string>();
            List<string> dllOcxList = new List<string>();
            UpdateFileDynamicIntelisenseInternal(appNode, exeList, exeFileNameList, appPathList, otherFilesList, dllOcxList, firewallList);

            Model.FirewallListStringConverter.FirewallList.AddRange(firewallList.Distinct());
            Model.ExeFileListStringConverter.ExeList.AddRange(exeList.Distinct());
            Model.ExeFileNameListStringConverter.ExeFileNameList.AddRange(exeFileNameList.Distinct());
            Model.AppPathListStringConverter.AppPath.AddRange(appPathList.Distinct());
            Model.OtherFileListStringConverter.OtherList.AddRange(otherFilesList.Distinct());
            Model.DllOcxFileListStringConverter.DllOcxList.AddRange(dllOcxList.Distinct());

            PortableApp.Inst.App.Launch.Launch.UpdateWaitForExeN(Model.ExeFileNameListStringConverter.ExeFileNameList);
        }

        private void UpdateFileDynamicIntelisenseInternal(TreeNode appNode, List<string> appList, List<string> exeFileNameList, List<string> appPathList,
            List<string> otherList, List<string> dllOcxList, List<string> firewallList)
        {
            if (appNode == null)
            {
                return;
            }

            Model.FileInfo fileInfo = appNode.Tag as Model.FileInfo;
            if (fileInfo != null)
            {
                string fileExt = Path.GetExtension(fileInfo.AbsolutePath).ToLower();
                if (fileExt == ".exe" || fileExt == ".com")
                {
                    var tempFullPath = appNode.GetFullPath("App");
                    appList.Add(tempFullPath);
                    exeFileNameList.Add(appNode.Text);
                    appPathList.Add(appNode.GetDirectory().Trim('\\'));
                    firewallList.Add(string.Format("%PAL:AppDir%\\{0}", tempFullPath));
                }
                else if (fileExt == ".bat")
                {
                    appList.Add(appNode.GetFullPath("App"));
                    exeFileNameList.Add(appNode.Text);
                    appPathList.Add(appNode.GetDirectory().Trim('\\'));
                }
                else if (fileExt == ".dll" || fileExt == ".ocx")
                {
                    dllOcxList.Add(appNode.GetFullPath("App"));
                }
                else if (appNode.IsDescendantOf("DefaultData"))
                {
                    if (fileExt == ".reg" || fileExt == ".ini" || fileExt == ".xml")
                    {
                        string fullPath = string.Format("%PAL:DataDir%\\{0}", appNode.GetFullPath("DefaultData"));
                        otherList.Add(fullPath);
                    }
                    else if (fileExt == ".exe")
                    {
                        var tempFullPath = appNode.GetFullPath("App");
                        firewallList.Add(string.Format("%PAL:AppDir%\\{0}", tempFullPath));
                    }
                }
                else if (appNode.IsDescendantOf("Data"))
                {
                    if (fileExt == ".reg" || fileExt == ".ini" || fileExt == ".xml")
                    {
                        string fullPath = string.Format("%PAL:DataDir%\\{0}", appNode.GetFullPath("Data"));
                        otherList.Add(fullPath);
                    }
                    else if (fileExt == ".exe")
                    {
                        var tempFullPath = appNode.GetFullPath("Data");
                        firewallList.Add(string.Format("%PAL:DataDir%\\{0}", tempFullPath));
                    }
                }
            }

            foreach (TreeNode item in appNode.Nodes)
            {
                UpdateFileDynamicIntelisenseInternal(item, appList, exeFileNameList, appPathList, otherList, dllOcxList, firewallList);
            }
        }
    }
}
