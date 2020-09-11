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
        private Type TreeNode_Drop_DataType = typeof(List<TreeNode>);

        private enum ProcessAction
        {
            Handle,
            AlreadyHandled,
            NotSupported
        }

        private void AppFilesTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ShowFilesMenu(sender as TreeViewEx, e.X, e.Y);
            }
        }


        private void SourceFilesTree_DragDrop(object sender, DragEventArgs e)
        {
            var fileData = e.Data.GetData("FileDrop") as string[];
            if (fileData != null)
            {
                var folderParser = new FolderParser();
                foreach(var item in fileData)
                {
                    if(Directory.Exists(item))
                    {
                        ImportSourceFiles(folderParser, item);
                    }
                }
            }
        }

        private void SourceFilesTree_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void SourceFilesTree_ItemDrag(object sender, ItemDragEventArgs e)
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

        private void AppFilesTree_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void AppFilesTree_DragDrop(object sender, DragEventArgs e)
        {
            string errMsg = string.Empty;
            if (e.Data.GetDataPresent(TreeNode_Drop_DataType))
            {
                var sourceTree = GetDropSourceTree(e);
                sourceFilesTree?.ClearSelection();
                Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
                TreeNode destinationNode = ((TreeView)sender).GetNodeAt(pt);
                if (destinationNode == null)
                {
                    return;
                }
                ProcessAction dropNodeAction = ProcessAction.Handle;
                if (destinationNode.IsDescendantOf("AppInfo") || 
                    destinationNode.IsDescendantOf("Other"))
                {
                    dropNodeAction = ProcessAction.NotSupported;
                }
                else if (destinationNode.IsDescendantOf(LaunchINI.DirectoriesCleanupForce_Tag) ||
                    destinationNode.IsDescendantOf(LaunchINI.DirectoriesCleanupIfEmpty_Tag))
                {
                    var topNode = destinationNode.GetTopNode();
                    int totalHandledCount = 0;
                    foreach (TreeNode srcNode in GetDropData(e))
                    {
                        if (srcNode.Tag == null && topNode.TreeView != srcNode.TreeView)
                        {
                            int startCount = topNode.Nodes.Count + 1;
                            string cleanUpForce = string.Format("{0}={1}", startCount, srcNode.GetFullPath());
                            var existingNode = topNode.Nodes.FindNode(cleanUpForce);
                            if (existingNode == null)
                            {
                                var newNode = new TreeNode(cleanUpForce);
                                topNode.Nodes.Add(newNode);
                                totalHandledCount++;
                            }
                        }
                    }
                    topNode.Expand();
                    dropNodeAction = totalHandledCount == 0 ? ProcessAction.NotSupported : ProcessAction.AlreadyHandled;
                }
                else if (destinationNode.IsDescendantOf(LaunchINI.DirectoriesMove_Tag) ||
                    destinationNode.IsDescendantOf(LaunchINI.DirectoriesLink_Tag))
                {
                    var topNode = destinationNode.GetTopNode();
                    int totalHandledCount = 0;
                    foreach (TreeNode srcNode in GetDropData(e))
                    {
                        if (topNode.TreeView != srcNode.TreeView)
                        {
                            string cleanUpForce = srcNode.ToDirectoryPair();
                            var existingNode = topNode.Nodes.FindNode(cleanUpForce);
                            if (existingNode == null)
                            {
                                var newNode = new TreeNode(cleanUpForce);
                                topNode.Nodes.Add(newNode);
                                totalHandledCount++;
                            }
                        }
                    }
                    topNode.Expand();
                    dropNodeAction = totalHandledCount == 0 ? ProcessAction.NotSupported : ProcessAction.AlreadyHandled;
                }
                else if (destinationNode.IsDescendantOf(LaunchINI.FilesMove_Tag))
                {
                    var topNode = destinationNode.GetTopNode();
                    int totalHandledCount = 0;
                    StringBuilder errMsgTemp = new StringBuilder();
                    foreach (TreeNode srcNode in GetDropData(e))
                    {
                        if (srcNode.Tag is Model.FileInfo &&
                            topNode.TreeView != srcNode.TreeView && srcNode.Parent != null)
                        {
                            string cleanUpForce = srcNode.ToFilePair();
                            var existingNode = topNode.Nodes.FindNode(cleanUpForce);
                            if (existingNode == null)
                            {
                                var newNode = new TreeNode(cleanUpForce);
                                topNode.Nodes.Add(newNode);
                                totalHandledCount++;
                            }
                        }
                        else if(topNode.TreeView != srcNode.TreeView && srcNode.Parent != null)
                        {
                            var fileNodes = srcNode.GetChildFileNodes();
                            foreach (TreeNode item in fileNodes)
                            {
                                string cleanUpForce = item.ToFilePair(srcNode);
                                var existingNode = topNode.Nodes.FindNode(cleanUpForce);
                                if (existingNode == null)
                                {
                                    var newNode = new TreeNode(cleanUpForce);
                                    topNode.Nodes.Add(newNode);
                                    totalHandledCount++;
                                }
                            }
                        }
                        else
                        {
                            errMsgTemp.Append(string.Format("Cannot drop folders on \"[FilesMove]\" node : {0}", srcNode.Text));
                        }
                    }
                    topNode.Expand();
                    dropNodeAction = totalHandledCount == 0 ? ProcessAction.NotSupported : ProcessAction.AlreadyHandled;
                    errMsg = errMsgTemp.ToString();
                }
                else if (destinationNode.Text == "DefaultData" || 
                    destinationNode.Text == "Data")
                {
                    int totalHandledCount = 0;
                    foreach (TreeNode srcNode in GetDropData(e))
                    {
                        if (destinationNode.TreeView != srcNode.TreeView && srcNode.Tag == null)
                        {
                            string folderName = srcNode.ToDirectoryName();
                            var existingNode = destinationNode.Nodes.FindNode(folderName);
                            if (existingNode == null)
                            {
                                var destTreeNode = new TreeNode(folderName);
                                foreach (TreeNode childNode in srcNode.Nodes)
                                {
                                    destTreeNode.Nodes.Add((TreeNode)childNode.Clone());
                                }
                                destinationNode.Nodes.Add(destTreeNode);
                                destinationNode.Expand();
                            }
                            totalHandledCount++;
                        }
                    }
                    dropNodeAction = totalHandledCount == 0 ? ProcessAction.NotSupported : ProcessAction.AlreadyHandled;
                }

                if (dropNodeAction == ProcessAction.Handle)
                {
                    foreach (TreeNode srcNode in GetDropData(e))
                    {
                        if (destinationNode.TreeView != srcNode.TreeView)
                        {
                            var existingNode = destinationNode.Nodes.FindNode(srcNode.Text);
                            if (existingNode == null)
                            {
                                destinationNode.Nodes.Add((TreeNode)srcNode.Clone());
                                destinationNode.Expand();
                            }
                        }
                    }
                    UpdateFileDynamicIntelisense();
                }
                else if (dropNodeAction == ProcessAction.NotSupported)
                {
                    if (string.IsNullOrWhiteSpace(errMsg))
                    {
                        errMsg = string.Format("Cannot Drop files / folders on to \"{0}\" Node, this operation is not supported", destinationNode.Text);
                    }
                    ErrorLog.Inst.LogError(errMsg);
                }
            }
        }

        public void ShowFilesMenu(TreeViewEx selectedTree, int x, int y)
        {
            _fileCopyItems.Enabled = false;
            _fileCopyItems.Tag = null;
            _filePasteItems.Enabled = false;
            _filePasteItems.Tag = null;
            _addDestFilesItem.Enabled = false;
            _expandAllItem.Enabled = false;
            _expandAllItem.Tag = null;
            _addDestFilesItem.Tag = null;
            _editDestFilesItem.Enabled = false;
            _editDestFilesItem.Tag = null;
            _removeDestFilesItem.Enabled = false;
            _removeDestFilesItem.Tag = null;
            _duplicateDestFilesItem.Enabled = false;
            _duplicateDestFilesItem.Tag = null;
            _convert64To32Env.Enabled = false;
            _convert64To32Env.Tag = null;

            TreeNode firstNode = selectedTree?.SelectedNode;
            if (firstNode == null)
            {
                return;
            }

            _expandAllItem.Enabled = true;
            _expandAllItem.Tag = firstNode;
            _selectedTree = selectedTree;

            switch (firstNode.Text)
            {
                case "App":
                case "DefaultData":
                case "Data":
                case LaunchINI.DirectoriesCleanupForce_Tag:
                case LaunchINI.DirectoriesCleanupIfEmpty_Tag:
                case LaunchINI.DirectoriesMove_Tag:
                case LaunchINI.FilesMove_Tag:
                case LaunchINI.DirectoriesLink_Tag:
                    {
                        _addDestFilesItem.Enabled = !selectedTree.IsMultiSelected;
                        _addDestFilesItem.Tag = firstNode;
                        var enablePasteItem = !selectedTree.IsMultiSelected && firstNode.Text != "App" && firstNode.Text != LaunchINI.FilesMove_Tag;
                        _filePasteItems.Enabled = enablePasteItem;
                        _filePasteItems.Tag = firstNode;
                        _destFileContextMenu.Show(selectedTree, x, y);
                    }
                    break;
                default:
                    {
                        bool showMenuFlag = false;
                        bool showfileMenu = false;
                        if (firstNode.IsDescendantOf("App"))
                        {
                            showfileMenu = !firstNode.IsDescendantOf("AppInfo");
                        }
                        else if (firstNode.IsDescendantOf("Data"))
                        {
                            showfileMenu = true;
                        }
                        else if (firstNode.IsDescendantOf(LaunchINI.DirectoriesCleanupForce_Tag) ||
                           firstNode.IsDescendantOf(LaunchINI.DirectoriesCleanupIfEmpty_Tag) ||
                           firstNode.IsDescendantOf(LaunchINI.DirectoriesMove_Tag) ||
                           firstNode.IsDescendantOf(LaunchINI.FilesMove_Tag) ||
                           firstNode.IsDescendantOf(LaunchINI.DirectoriesLink_Tag))
                        {
                            showMenuFlag = true;
                        }

                        if (showMenuFlag)
                        {
                            _addDestFilesItem.Tag = firstNode.GetTopNodeName();
                            _editDestFilesItem.Tag = _addDestFilesItem.Tag;
                            _removeDestFilesItem.Tag = _addDestFilesItem.Tag;
                            _duplicateDestFilesItem.Tag = _addDestFilesItem.Tag;
                            _convert64To32Env.Tag = _addDestFilesItem.Tag;
                            _fileCopyItems.Tag = _addDestFilesItem.Tag;

                            _addDestFilesItem.Enabled = false;
                            _editDestFilesItem.Enabled = !selectedTree.IsMultiSelected;
                            _removeDestFilesItem.Enabled = true;
                            _duplicateDestFilesItem.Enabled = true;
                            _convert64To32Env.Enabled = true;
                            _fileCopyItems.Enabled = true;
                            _destFileContextMenu.Show(selectedTree, x, y);
                        }
                        else if(showfileMenu)
                        {
                            _addDestFilesItem.Enabled = !(firstNode.Tag is Model.FileInfo) && !selectedTree.IsMultiSelected;
                            _editDestFilesItem.Enabled = firstNode.Tag == null && !selectedTree.IsMultiSelected;
                            _removeDestFilesItem.Enabled = !firstNode.IsDescendantOf("settings");
                            _fileCopyItems.Enabled = true;
                            _destFileContextMenu.Show(selectedTree, x, y);
                        }
                    }
                    break;
            }
        }
    }
}
