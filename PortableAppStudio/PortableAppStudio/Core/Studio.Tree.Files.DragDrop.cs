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
        private Type TreeNode_Drop_DataType = typeof(List<TreeNode>);
        private const string TreeNode_Drop_FileDrop = "FileDrop";

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
            var fileData = e.Data.GetData(TreeNode_Drop_FileDrop) as string[];
            if (fileData != null)
            {
                var folderParser = new FolderParser();
                foreach(var item in fileData)
                {
                    ImportSourceFolder(folderParser, item);
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
            var sourceTree = GetDropSourceTree(e);
            sourceFilesTree?.ClearSelection();
            Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
            TreeNode destinationNode = ((TreeView)sender).GetNodeAt(pt);
            if (destinationNode == null)
            {
                return;
            }

            if (e.Data.GetDataPresent(TreeNode_Drop_DataType))
            {
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
                    foreach (TreeNode srcNode in GetTreeNodeDropData(e))
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
                else if (destinationNode.IsDescendantOf(LaunchINI.PrefixPATHEnv_Tag) ||
                   destinationNode.IsDescendantOf(LaunchINI.Environment_Tag))
                {
                    var topNode = destinationNode.GetTopNode();
                    int totalHandledCount = 0;
                    foreach (TreeNode srcNode in GetTreeNodeDropData(e))
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
                    foreach (TreeNode srcNode in GetTreeNodeDropData(e))
                    {
                        if (topNode.TreeView != srcNode.TreeView)
                        {
                            string cleanUpForce = srcNode.ToDataDirectoryPair((destinationNode.IsDescendantOf(LaunchINI.PrefixPATHEnv_Tag) || destinationNode.IsDescendantOf(LaunchINI.Environment_Tag)));
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
                    foreach (TreeNode srcNode in GetTreeNodeDropData(e))
                    {
                        if (srcNode.Tag is Model.FileInfo &&
                            topNode.TreeView != srcNode.TreeView && srcNode.Parent != null)
                        {
                            string cleanUpForce = srcNode.ToDataFilePair();
                            var existingNode = topNode.Nodes.FindNode(cleanUpForce);
                            if (existingNode == null)
                            {
                                var newNode = new TreeNode(cleanUpForce);
                                topNode.Nodes.Add(newNode);
                                totalHandledCount++;
                            }
                        }
                        else if (topNode.TreeView != srcNode.TreeView && srcNode.Parent != null)
                        {
                            var fileNodes = srcNode.GetChildFileNodes();
                            foreach (TreeNode item in fileNodes)
                            {
                                string cleanUpForce = item.ToDataFilePair();
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
                    foreach (TreeNode srcNode in GetTreeNodeDropData(e))
                    {
                        if (destinationNode.TreeView != srcNode.TreeView && srcNode.Tag == null)
                        {
                            string folderName = srcNode.ToDataDirectoryName();
                            var existingNode = destinationNode.CreateFolderNodes(folderName);
                            if (existingNode != null)
                            {
                                foreach (TreeNode childNode in srcNode.Nodes)
                                {
                                    existingNode.Nodes.Add((TreeNode)childNode.Clone());
                                }
                                destinationNode.Expand();
                            }
                            totalHandledCount++;
                        }
                    }
                    dropNodeAction = totalHandledCount == 0 ? ProcessAction.NotSupported : ProcessAction.AlreadyHandled;
                }

                if (dropNodeAction == ProcessAction.Handle)
                {
                    foreach (TreeNode srcNode in GetTreeNodeDropData(e))
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
            else
            {
                if (string.IsNullOrWhiteSpace(errMsg))
                {
                    errMsg = string.Format("Cannot Drop files / folders directly on to \"{0}\" Node, this operation is not supported, try importing folder to source file tree first then drop to destination file tree", destinationNode.Text);
                }
                ErrorLog.Inst.LogError(errMsg);
            }
        }

        
    }
}
