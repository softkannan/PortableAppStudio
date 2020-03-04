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
        private void appRegTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.Node.Text == LaunchINI.RegistrationFreeCOM_Tag)
                {
                    ShowRegFreeComMenu(sourceRegTree as TreeViewEx, sender as TreeViewEx, e.X, e.Y);
                }
                else
                {
                    ShowRegMenu(sender as TreeViewEx, e.X, e.Y);
                }
            }
        }
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
                    foreach (TreeNode srcNode in GetDropData(e))
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
                    foreach (TreeNode srcNode in GetDropData(e))
                    {
                        if (topNode.TreeView != srcNode.TreeView && srcNode.Tag == null)
                        {
                            var newNodeText = string.Format("{0}_{1}_registry={2}",
                                srcNode.Text.Replace(" ", "_"),
                                srcNode.GetTopNodeName(),
                                srcNode.Name.RemoveEnd('\\'));
                            var existingNode = topNode.Nodes.FindNode(newNodeText);
                            if (existingNode == null)
                            {
                                var newNode = new TreeNode(newNodeText);
                                newNode.Tag = srcNode;
                                topNode.Nodes.Add(newNode);
                                totalHandledCount++;
                            }
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
                    foreach (TreeNode srcNode in GetDropData(e))
                    {
                        if (topNode.TreeView != srcNode.TreeView && srcNode.Tag == null)
                        {
                            int startCount = topNode.Nodes.Count + 1;
                            var newNode = new TreeNode(string.Format("{0}={1}", startCount, srcNode.Name.RemoveEnd('\\')));
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
                    foreach (TreeNode srcNode in GetDropData(e))
                    {
                        if (topNode.TreeView != srcNode.TreeView && srcNode.Tag is Model.RegInfo)
                        {
                            Model.RegInfo regInfo = srcNode.Tag as Model.RegInfo;
                            string regValue = "";
                            if (regInfo != null)
                            {
                                regValue = regInfo.RegWriteValue;
                                if (!string.IsNullOrWhiteSpace(regValue))
                                {
                                    string valueName = regInfo.ValueName == "@" ? "" : regInfo.ValueName;
                                    string tempVal = string.Format("{0}{1}={2}", srcNode.Name, valueName, regValue);
                                    dropNodeAction = ProcessAction.AlreadyHandled;
                                    var existingNode = topNode.Nodes.FindNode(tempVal);
                                    if (existingNode == null)
                                    {
                                        var newNode = new TreeNode(tempVal);
                                        topNode.Nodes.Add(newNode);
                                        totalHandledCount++;
                                    }
                                }
                                else
                                {
                                    errMsgTemp.Append(string.Format("Registry Type {0} not supported", regInfo.Kind));
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
                    foreach (TreeNode srcNode in GetDropData(e))
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
                            var newNode = new TreeNode(string.Format("{0}={1}{2}", startCount, srcNode.Name, valueName));
                            topNode.Nodes.Add(newNode);
                            totalHandledCount++;
                        }
                    }
                    topNode.Expand();
                    dropNodeAction = totalHandledCount == 0 ? ProcessAction.NotSupported : ProcessAction.AlreadyHandled;
                    errMsg = errMsgTemp.ToString();
                }
                else if (destinationNode.Text == LaunchINI.Environment_Tag)
                {
                    dropNodeAction = ProcessAction.NotSupported;
                    errMsg = "Drag and Drop not supported user has to use menus to add variables manually";
                }

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

       
        public void ShowRegFreeComMenu(TreeViewEx sourceRegTree, TreeViewEx selectedTree, int x, int y)
        {
            _selectedTree = selectedTree;
            _importCOMDllRegItem.Tag = sourceRegTree;
            _generateCOMDllRegItem.Tag = selectedTree;
            _selectedTree = selectedTree;
            _destRegFreeComContextMenu.Show(selectedTree, x, y);
        }

        public void ShowRegMenu(TreeViewEx selectedTree, int x, int y)
        {
            _addDestRegItem.Enabled = false;
            _addDestRegItem.Tag = null;
            _editDestRegItem.Enabled = false;
            _editDestRegItem.Tag = null;
            _removeDestRegItem.Enabled = false;
            _removeDestRegItem.Tag = null;
            _duplicateDestRegItem.Enabled = false;
            _duplicateDestRegItem.Tag = null;


            TreeNode firstNode = selectedTree?.SelectedNode;
            if (firstNode == null)
            {
                return;
            }
            _selectedTree = selectedTree;

            switch (firstNode.Text)
            {

                case LaunchINI.RegistryCleanupForce_Tag:
                case LaunchINI.RegistryCleanupIfEmpty_Tag:
                case LaunchINI.RegistryKeys_Tag:
                case LaunchINI.RegistryValueBackupDelete_Tag:
                case LaunchINI.RegistryValueWrite_Tag:
                case LaunchINI.Environment_Tag:
                case LaunchINI.QtKeysCleanup_Tag:
                    {
                        _addDestRegItem.Enabled = !selectedTree.IsMultiSelected;
                        _addDestRegItem.Tag = firstNode;

                        _destRegContextMenu.Show(selectedTree, x, y);
                    }
                    break;
                default:
                    {
                        bool showMenuFlag = false;
                        if (firstNode.IsDescendantOf(LaunchINI.RegistryCleanupForce_Tag) ||
                            firstNode.IsDescendantOf(LaunchINI.RegistryCleanupIfEmpty_Tag) ||
                            firstNode.IsDescendantOf(LaunchINI.RegistryKeys_Tag)||
                            firstNode.IsDescendantOf(LaunchINI.RegistryValueBackupDelete_Tag)||
                            firstNode.IsDescendantOf(LaunchINI.RegistryValueWrite_Tag)||
                            firstNode.IsDescendantOf(LaunchINI.RegistrationFreeCOM_Tag) ||
                            firstNode.IsDescendantOf(LaunchINI.QtKeysCleanup_Tag) ||
                            firstNode.IsDescendantOf(LaunchINI.Environment_Tag))
                        {
                            _addDestRegItem.Tag = firstNode.GetTopNodeName();
                            _editDestRegItem.Tag = _addDestRegItem.Tag;
                            _removeDestRegItem.Tag = _addDestRegItem.Tag;
                            _duplicateDestRegItem.Tag = _addDestRegItem.Tag;
                            showMenuFlag = true;
                        }

                        if (showMenuFlag)
                        {
                            _addDestRegItem.Enabled = false;
                            _editDestRegItem.Enabled = !selectedTree.IsMultiSelected;
                            _removeDestRegItem.Enabled = true;
                            _duplicateDestRegItem.Enabled = true;
                            _destRegContextMenu.Show(selectedTree, x, y);
                        }
                    }
                    break;
            }
        }
    }
}
