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
    partial class MainStudio
    {

        private void SourceRegTree_ItemDrag(object sender, ItemDragEventArgs e)
        {
            var senderTree = (TreeViewEx)sender;
            List<TreeNode> selectedNodes = senderTree.MultiSelectedNodes;
            if (selectedNodes.Count > 0)
            {
                DoDragDrop(selectedNodes, DragDropEffects.Copy);
            }
            else
            {
                DoDragDrop(new List<TreeNode> { (TreeNode)e.Item }, DragDropEffects.Copy);
            }
        }

        private void AppRegTree_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }
        private void AppRegTree_DragDrop(object sender, DragEventArgs e)
        {
            string errMsg = string.Empty;
            if (e.Data.GetDataPresent(TreeNode_Drop_DataType))
            {
                var sourceTree = GetDropSourceTree(e);
                sourceFilesTree?.ClearSelection();
                Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
                TreeNode destinationNode = ((TreeView)sender).GetNodeAt(pt);
                ProcessAction dropNodeAction = ProcessAction.NotSupported;

                if (destinationNode.IsDescendantOf(LaunchINI.RegistrationFreeCOM_Tag))
                {
                    int numberOfCOMDll = 0;
                    var topNode = destinationNode.GetTopNode();
                    foreach (TreeNode srcNode in GetTreeNodeDropData(e))
                    {
                        if (topNode.TreeView != srcNode.TreeView && srcNode.Tag == null)
                        {
                            foreach (Model.FileInfo item in PortableApp.Inst.App.GetComDllOcx(srcNode.GetTopNode()))
                            {
                                int startCount = topNode.Nodes.Count + 1;
                                var newNode = new TreeNode(string.Format("{0}={1}", startCount,item.RelativePath));
                                newNode.Tag = item;
                                topNode.Nodes.Add(newNode);
                                numberOfCOMDll++;
                            }
                        }
                    }
                    dropNodeAction = numberOfCOMDll == 0 ? ProcessAction.NotSupported : ProcessAction.AlreadyHandled;
                    topNode.Expand();
                    errMsg = numberOfCOMDll == 0 ? "Unable to find any COM dll files." : "";
                }
                else if (destinationNode.IsDescendantOf(LaunchINI.RegistryKeys_Tag))
                {
                    int totalHandledCount = 0;
                    StringBuilder errMsgTemp = new StringBuilder();
                    var topNode = destinationNode.GetTopNode();
                    foreach (TreeNode srcNode in GetTreeNodeDropData(e))
                    {
                        if (topNode.TreeView != srcNode.TreeView && srcNode.Tag == null)
                        {
                            var relPath = srcNode.GetRegistryKey();
                            var regFileName = relPath.RegKeyToFileName();
                            var nodeCount = 1;
                            var newNodeText = string.Format("{0}={1}", regFileName, relPath);

                            do
                            {
                                var existingNode = topNode.Nodes.FindNode(newNodeText);
                                if (existingNode == null)
                                {
                                    var newNode = new TreeNode(newNodeText);
                                    newNode.Tag = srcNode;
                                    topNode.Nodes.Add(newNode);
                                    totalHandledCount++;
                                    break;
                                }
                                else
                                {
                                    newNodeText = string.Format("{0}_{1}={2}", regFileName, nodeCount, relPath);
                                    nodeCount++;
                                }
                            } while (true);
                        }
                    }
                    topNode.Expand();
                    dropNodeAction = totalHandledCount == 0 ? ProcessAction.NotSupported : ProcessAction.AlreadyHandled;
                    errMsg = errMsgTemp.ToString();
                }
                else if (destinationNode.IsDescendantOf(LaunchINI.RegistryCleanupForce_Tag) ||
                    destinationNode.IsDescendantOf(LaunchINI.RegistryCleanupIfEmpty_Tag) || 
                    destinationNode.IsDescendantOf(LaunchINI.QtKeysCleanup_Tag))
                {
                    int totalHandledCount = 0;
                    StringBuilder errMsgTemp = new StringBuilder();
                    var topNode = destinationNode.GetTopNode();
                    foreach (TreeNode srcNode in GetTreeNodeDropData(e))
                    {
                        if (topNode.TreeView != srcNode.TreeView && srcNode.Tag == null)
                        {
                            int startCount = topNode.Nodes.Count + 1;
                            var relPath = srcNode.GetRegistryKey();
                            var newNode = new TreeNode(string.Format("{0}={1}", startCount, relPath));
                            topNode.Nodes.Add(newNode);
                            totalHandledCount++;
                        }
                    }
                    topNode.Expand();
                    dropNodeAction = totalHandledCount == 0 ? ProcessAction.NotSupported : ProcessAction.AlreadyHandled;
                    errMsg = errMsgTemp.ToString();
                }
                else if (destinationNode.IsDescendantOf(LaunchINI.RegistryValueWrite_Tag))
                {
                    int totalHandledCount = 0;
                    StringBuilder errMsgTemp = new StringBuilder();
                    var topNode = destinationNode.GetTopNode();
                    foreach (TreeNode srcNode in GetTreeNodeDropData(e))
                    {
                        if (topNode.TreeView != srcNode.TreeView && srcNode.Tag is Model.RegInfo)
                        {
                            Model.RegInfo regInfo = srcNode.Tag as Model.RegInfo;
                            string regValue = "";
                            if (regInfo != null)
                            {
                                regValue = regInfo.RegWriteValue.Replace("\\\\","\\");
                                if (regInfo.SearchReplaceList != null && regInfo.SearchReplaceList.Count > 0)
                                {
                                    foreach (var item in regInfo.SearchReplaceList)
                                    {
                                        if (regValue.IndexOf(item.Key) == 0)
                                        {
                                            regValue = string.Format("REG_SZ:{0}{1}", item.Value, regValue.Substring(item.Key.Length));
                                            break;
                                        }
                                    }
                                }

                                string valueName = regInfo.ValueName == "@" ? "" : regInfo.ValueName;
                                var relPath = srcNode.GetRegistryKey();
                                string tempVal = string.Format("{0}\\{1}={2}", relPath, valueName, regValue);
                                dropNodeAction = ProcessAction.AlreadyHandled;
                                var existingNode = topNode.Nodes.FindNode(tempVal);
                                if (existingNode == null)
                                {
                                    var newNode = new TreeNode(tempVal);
                                    topNode.Nodes.Add(newNode);
                                    totalHandledCount++;
                                }
                            }
                        }
                    }
                    topNode.Expand();
                    dropNodeAction = totalHandledCount == 0 ? ProcessAction.NotSupported : ProcessAction.AlreadyHandled;
                    errMsg = errMsgTemp.ToString();
                }
                else if (destinationNode.IsDescendantOf(LaunchINI.RegistryValueBackupDelete_Tag))
                {
                    int totalHandledCount = 0;
                    StringBuilder errMsgTemp = new StringBuilder();
                    var topNode = destinationNode.GetTopNode();
                    foreach (TreeNode srcNode in GetTreeNodeDropData(e))
                    {
                        if (topNode.TreeView != srcNode.TreeView && srcNode.Tag is Model.RegInfo)
                        {
                            int startCount = topNode.Nodes.Count + 1;
                            int sepPos = srcNode.Text.IndexOf("=");
                            string valueName = srcNode.Text;
                            if (sepPos != -1)
                            {
                                valueName = srcNode.Text.Substring(0, sepPos);
                            }
                            var relPath = srcNode.GetRegistryKey();
                            var newNode = new TreeNode(string.Format("{0}={1}\\{2}", startCount, relPath, valueName));
                            topNode.Nodes.Add(newNode);
                            totalHandledCount++;
                        }
                    }
                    topNode.Expand();
                    dropNodeAction = totalHandledCount == 0 ? ProcessAction.NotSupported : ProcessAction.AlreadyHandled;
                    errMsg = errMsgTemp.ToString();
                }
                else if (destinationNode.IsDescendantOf(LaunchINI.FileWriteN_Tag))
                {
                    int totalHandledCount = 0;
                    StringBuilder errMsgTemp = new StringBuilder();
                    var topNode = destinationNode.GetTopNode();
                    foreach (TreeNode srcNode in GetTreeNodeDropData(e))
                    {
                        if (topNode.TreeView != srcNode.TreeView && srcNode.Tag is Model.RegInfo)
                        {
                            Model.RegInfo regInfo = srcNode.Tag as Model.RegInfo;
                            string regValue = "";
                            if (regInfo != null)
                            {
                                if (regInfo.SearchReplaceList != null && regInfo.SearchReplaceList.Count > 0 && regInfo.Kind == "REG_SZ")
                                {
                                    regValue = regInfo.RegWriteValue.Replace("\\\\", "\\");
                                    foreach (var item in regInfo.SearchReplaceList)
                                    {
                                        if (regValue.IndexOf(item.Key) == 0)
                                        {
                                            regValue = string.Format("\"{0}{1}\"", item.Value, regValue.Substring(item.Key.Length));
                                            break;
                                        }
                                    }
                                }
                                else if(regInfo.Kind == "REG_DWORD")
                                {
                                    regValue = string.Format("dword:{0}", regInfo.Value);
                                }

                                if (topNode.Tag is FileWriteNSectionList selectedList)
                                {
                                    var fileSection = new FileWriteNSection();

                                    fileSection.Type = "INI";
                                    fileSection.Section = string.Format("[{0}]", srcNode.GetRegistryKey());
                                    fileSection.Key = regInfo.ValueName == "@" ? regInfo.ValueName : string.Format("\"{0}\"", regInfo.ValueName);
                                    fileSection.Value = regValue;
                                    fileSection.IniKey = string.Format("FileWrite{0}", selectedList.Count + 1);

                                    var treeNode = topNode.Nodes.Add(fileSection.FullValue);
                                    treeNode.Tag = fileSection;
                                    selectedList.Add(fileSection);
                                    selectedList.UpdateIndex();

                                    dropNodeAction = ProcessAction.AlreadyHandled;
                                    totalHandledCount++;
                                }
                            }
                        }
                    }
                    topNode.Expand();
                    dropNodeAction = totalHandledCount == 0 ? ProcessAction.NotSupported : ProcessAction.AlreadyHandled;
                    errMsg = errMsgTemp.ToString();
                }
                //else if (destinationNode.Text == LaunchINI.Environment_Tag)
                //{
                //    dropNodeAction = ProcessAction.NotSupported;
                //    errMsg = "Drag and Drop not supported user has to use menus to add variables manually";
                //}

                if (dropNodeAction == ProcessAction.NotSupported)
                {
                    if (string.IsNullOrWhiteSpace(errMsg))
                    {
                        errMsg = string.Format("Registry Drop operation not supported on \"{0}\" node.", destinationNode.Text);
                    }
                    ErrorLog.Inst.LogError(errMsg);
                }
            }
        }
    }
}
