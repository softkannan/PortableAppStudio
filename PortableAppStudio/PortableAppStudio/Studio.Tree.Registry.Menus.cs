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
        ContextMenuStrip _destRegFreeComContextMenu = new ContextMenuStrip();
        private ToolStripItem _importCOMDllRegItem;
        private ToolStripItem _generateCOMDllRegItem;

        ContextMenuStrip _destRegKeyContextMenu = new ContextMenuStrip();
        private ToolStripItem _editInNotepadRegKeyItem;
        private ToolStripItem _mergeDefaultRegKeyItem;
        private ToolStripItem _launchInRegEditRegKeyItem;
        private ToolStripItem _editDestRegKeyItem;
        private ToolStripItem _removeDestRegKeyItem;
        private ToolStripItem _copyDestRegKeyItem;

        ContextMenuStrip _destRegEditContextMenu = new ContextMenuStrip();
        private ToolStripItem _editDestRegItem;
        private ToolStripItem _copyDestRegItem;
        private ToolStripItem _removeDestRegItem;
        private ToolStripItem _duplicateDestRegItem;
        private ToolStripItem _launchInRegEditDestRegItem;

        ContextMenuStrip _destRegContextMenu = new ContextMenuStrip();
        private ToolStripItem _addDestRegItem;
        private ToolStripItem _pasteDestRegItem;

        ContextMenuStrip _sourceRegContextMenu = new ContextMenuStrip();
        private ToolStripItem _removeSourceRegItem;
        private ToolStripItem _expandAllSourceRegItem;
        private ToolStripItem _searchAndReplaceSourceRegItem;
        private ToolStripItem _copySourceRegItem;
        private ToolStripItem _pasteSourceRegItem;


        ContextMenuStrip _destFileWriteNContextMenu = new ContextMenuStrip();
        private ToolStripItem _addDestFileWriteNItem;
        private ToolStripItem _pasteDestFileWriteNItem;

        ContextMenuStrip _destFileWriteNItemContextMenu = new ContextMenuStrip();
        private ToolStripItem _openDestFileWriteNItem;
        private ToolStripItem _editDestFileWriteNItem;
        private ToolStripItem _removeDestFileWriteNItem;
        private ToolStripItem _duplicateDestFileWriteNItem;


        private (string ShortKey, string LongKey)[] REG_MAP = { ("HKCU", "HKEY_CURRENT_USER"), ("HKLM", "HKEY_LOCAL_MACHINE") };

        private void UpdateRegTreeContextMenus()
        {
            _destRegContextMenu.Items.Clear();
            _addDestRegItem = _destRegContextMenu.Items.Add("Add");
            _addDestRegItem.Click += AddDestRegItem_Click;
            _pasteDestRegItem = _destRegContextMenu.Items.Add("Paste");
            _pasteDestRegItem.Click += PasteDestRegItem_Click;

            _destRegKeyContextMenu.Items.Clear();
            _copyDestRegKeyItem = _destRegKeyContextMenu.Items.Add("Copy");
            _copyDestRegKeyItem.Click += CopyDestRegItem_Click;
            _editDestRegKeyItem = _destRegKeyContextMenu.Items.Add("Edit");
            _editDestRegKeyItem.Click += EditDestRegKeyItem_Click;
            _editInNotepadRegKeyItem = _destRegKeyContextMenu.Items.Add("EditInNotepad");
            _editInNotepadRegKeyItem.Click += EditDestNotepadRegItem_Click;
            _mergeDefaultRegKeyItem = _destRegKeyContextMenu.Items.Add("MergeDefaultRegFile");
            _mergeDefaultRegKeyItem.Click += MergeDefaultRegItem_Click;
            _launchInRegEditRegKeyItem = _destRegKeyContextMenu.Items.Add("LaunchInRegEdit");
            _launchInRegEditRegKeyItem.Click += LaunchInRegEditRegItem_Click;
            _removeDestRegKeyItem = _destRegKeyContextMenu.Items.Add("Remove");
            _removeDestRegKeyItem.Click += RemoveDestRegItem_Click;

            _destRegEditContextMenu.Items.Clear();
            _copyDestRegItem = _destRegEditContextMenu.Items.Add("Copy");
            _copyDestRegItem.Click += CopyDestRegItem_Click;
            _editDestRegItem = _destRegEditContextMenu.Items.Add("Edit");
            _editDestRegItem.Click += EditDestRegKeyItem_Click;
            _launchInRegEditDestRegItem = _destRegEditContextMenu.Items.Add("LaunchInRegEdit");
            _launchInRegEditDestRegItem.Click += LaunchInRegEditRegItem_Click;
            _removeDestRegItem = _destRegEditContextMenu.Items.Add("Remove");
            _removeDestRegItem.Click += RemoveDestRegItem_Click;
            _duplicateDestRegItem = _destRegEditContextMenu.Items.Add("Duplicate");
            _duplicateDestRegItem.Click += DuplicateDestRegItem_Click;

            _destRegFreeComContextMenu.Items.Clear();
            _importCOMDllRegItem = _destRegFreeComContextMenu.Items.Add("Auto Import COM");
            _importCOMDllRegItem.Click += AutoImportCOMDll_Click;
            _generateCOMDllRegItem = _destRegFreeComContextMenu.Items.Add("Generate Manifest");
            _generateCOMDllRegItem.Click += GenerateCOMDllRegItem_Click;

            _sourceRegContextMenu.Items.Clear();
            _copySourceRegItem = _sourceRegContextMenu.Items.Add("Copy");
            _copySourceRegItem.Click += CopySourceRegItem_Click;
            _pasteSourceRegItem = _sourceRegContextMenu.Items.Add("Paste");
            _pasteSourceRegItem.Click += PasteSourceRegItem_Click;
            _expandAllSourceRegItem = _sourceRegContextMenu.Items.Add("ExpandAll");
            _expandAllSourceRegItem.Click += ExpandNodes_Click;
            _searchAndReplaceSourceRegItem = _sourceRegContextMenu.Items.Add("SearchAndReplace");
            _searchAndReplaceSourceRegItem.Click += SearchAndReplaceSourceRegItem_Click;
            _removeSourceRegItem = _sourceRegContextMenu.Items.Add("Remove");
            _removeSourceRegItem.Click += RemoveSourceItem_Click;


            _destFileWriteNContextMenu.Items.Clear();
            _addDestFileWriteNItem = _destFileWriteNContextMenu.Items.Add("Add");
            _addDestFileWriteNItem.Click += AddDestFileWriteNItem_Click;
            _pasteDestFileWriteNItem = _destFileWriteNContextMenu.Items.Add("Paste");
            _pasteDestFileWriteNItem.Click += PasteDestFileWriteNItem_Click;

            _destFileWriteNItemContextMenu.Items.Clear();
            _openDestFileWriteNItem = _destFileWriteNItemContextMenu.Items.Add("Open");
            _openDestFileWriteNItem.Click += OpenDestFileWriteNItem_Click;
            _editDestFileWriteNItem = _destFileWriteNItemContextMenu.Items.Add("Edit");
            _editDestFileWriteNItem.Click += EditDestFileWriteNItem_Click;
            _duplicateDestFileWriteNItem = _destFileWriteNItemContextMenu.Items.Add("Duplicate");
            _duplicateDestFileWriteNItem.Click += DuplicateDestFileWriteNItem_Click;
            _removeDestFileWriteNItem = _destFileWriteNItemContextMenu.Items.Add("Remove");
            _removeDestFileWriteNItem.Click += RemoveDestFileWriteNItem_Click;

        }

        

        public void ShowSourceRegMenu(TreeViewEx selectedTree, int x, int y)
        {
            _importCOMDllRegItem.Tag = null;
            _generateCOMDllRegItem.Tag = null;

            _editInNotepadRegKeyItem.Tag = null;
            _mergeDefaultRegKeyItem.Tag = null;
            _launchInRegEditRegKeyItem.Tag = null;
            _editDestRegKeyItem.Tag = null;
            _removeDestRegKeyItem.Tag = null;
            _copyDestRegKeyItem.Tag = null;

            _editDestRegItem.Tag = null;
            _copyDestRegItem.Tag = null;
            _removeDestRegItem.Tag = null;
            _duplicateDestRegItem.Tag = null;
            _launchInRegEditDestRegItem.Tag = null;

            _addDestRegItem.Tag = null;
            _pasteDestRegItem.Tag = null;

            _removeSourceRegItem.Tag = null;
            _expandAllSourceRegItem.Tag = null;
            _searchAndReplaceSourceRegItem.Tag = null;
            _copySourceRegItem.Tag = null;
            _pasteSourceRegItem.Tag = null;


            _addDestFileWriteNItem.Tag = null;
            _pasteDestFileWriteNItem.Tag = null;

            _openDestFileWriteNItem.Tag = null;
            _editDestFileWriteNItem.Tag = null;
            _removeDestFileWriteNItem.Tag = null;
            _duplicateDestFileWriteNItem.Tag = null;

            TreeNode firstNode = selectedTree?.SelectedNode;
            if (firstNode == null)
            {
                return;
            }
            _selectedTree = selectedTree;

            switch (firstNode.Text)
            {
                default:
                    {
                        _expandAllSourceRegItem.Tag = firstNode;
                        _sourceRegContextMenu.Show(selectedTree, x, y);
                    }
                    break;
            }
        }

        public void ShowRegFreeComMenu(TreeViewEx sourceRegTree, TreeViewEx selectedTree, int x, int y)
        {
            _importCOMDllRegItem.Tag = null;
            _generateCOMDllRegItem.Tag = null;

            _editInNotepadRegKeyItem.Tag = null;
            _mergeDefaultRegKeyItem.Tag = null;
            _launchInRegEditRegKeyItem.Tag = null;
            _editDestRegKeyItem.Tag = null;
            _removeDestRegKeyItem.Tag = null;
            _copyDestRegKeyItem.Tag = null;

            _editDestRegItem.Tag = null;
            _copyDestRegItem.Tag = null;
            _removeDestRegItem.Tag = null;
            _duplicateDestRegItem.Tag = null;
            _launchInRegEditDestRegItem.Tag = null;

            _addDestRegItem.Tag = null;
            _pasteDestRegItem.Tag = null;

            _removeSourceRegItem.Tag = null;
            _expandAllSourceRegItem.Tag = null;
            _searchAndReplaceSourceRegItem.Tag = null;
            _copySourceRegItem.Tag = null;
            _pasteSourceRegItem.Tag = null;


            _addDestFileWriteNItem.Tag = null;
            _pasteDestFileWriteNItem.Tag = null;

            _openDestFileWriteNItem.Tag = null;
            _editDestFileWriteNItem.Tag = null;
            _removeDestFileWriteNItem.Tag = null;
            _duplicateDestFileWriteNItem.Tag = null;

            _selectedTree = selectedTree;
            _importCOMDllRegItem.Tag = sourceRegTree;
            _generateCOMDllRegItem.Tag = selectedTree;
            _selectedTree = selectedTree;
            _destRegFreeComContextMenu.Show(selectedTree, x, y);
        }

        public void ShowRegMenu(TreeViewEx selectedTree, int x, int y)
        {
            _importCOMDllRegItem.Tag = null;
            _generateCOMDllRegItem.Tag = null;

            _editInNotepadRegKeyItem.Tag = null;
            _mergeDefaultRegKeyItem.Tag = null;
            _launchInRegEditRegKeyItem.Tag = null;
            _editDestRegKeyItem.Tag = null;
            _removeDestRegKeyItem.Tag = null;
            _copyDestRegKeyItem.Tag = null;

            _editDestRegItem.Tag = null;
            _copyDestRegItem.Tag = null;
            _removeDestRegItem.Tag = null;
            _duplicateDestRegItem.Tag = null;
            _launchInRegEditDestRegItem.Tag = null;

            _addDestRegItem.Tag = null;
            _pasteDestRegItem.Tag = null;

            _removeSourceRegItem.Tag = null;
            _expandAllSourceRegItem.Tag = null;
            _searchAndReplaceSourceRegItem.Tag = null;
            _copySourceRegItem.Tag = null;
            _pasteSourceRegItem.Tag = null;


            _addDestFileWriteNItem.Tag = null;
            _pasteDestFileWriteNItem.Tag = null;

            _openDestFileWriteNItem.Tag = null;
            _editDestFileWriteNItem.Tag = null;
            _removeDestFileWriteNItem.Tag = null;
            _duplicateDestFileWriteNItem.Tag = null;

            TreeNode firstNode = selectedTree?.SelectedNode;
            if (firstNode == null)
            {
                return;
            }
            _selectedTree = selectedTree;

            switch (firstNode.Text)
            {
                case LaunchINI.FileWriteN_Tag:
                    {
                        _addDestFileWriteNItem.Tag = firstNode;
                        _pasteDestFileWriteNItem.Tag = firstNode;

                        _destFileWriteNContextMenu.Show(selectedTree, x, y);
                    }
                    break;
                case LaunchINI.RegistryKeys_Tag:
                case LaunchINI.RegistryCleanupForce_Tag:
                case LaunchINI.RegistryCleanupIfEmpty_Tag:
                case LaunchINI.RegistryValueBackupDelete_Tag:
                case LaunchINI.RegistryValueWrite_Tag:
                case LaunchINI.QtKeysCleanup_Tag:
                    {
                        _addDestRegItem.Tag = firstNode;
                        _pasteDestRegItem.Tag = firstNode;

                        _destRegContextMenu.Show(selectedTree, x, y);
                    }
                    break;
                default:
                    {
                        if (firstNode.IsDescendantOf(LaunchINI.RegistryCleanupForce_Tag) ||
                            firstNode.IsDescendantOf(LaunchINI.RegistryCleanupIfEmpty_Tag) ||
                            firstNode.IsDescendantOf(LaunchINI.RegistryValueBackupDelete_Tag) ||
                            firstNode.IsDescendantOf(LaunchINI.RegistryValueWrite_Tag) ||
                            firstNode.IsDescendantOf(LaunchINI.RegistrationFreeCOM_Tag) ||
                            firstNode.IsDescendantOf(LaunchINI.QtKeysCleanup_Tag))
                        {
                            _editDestRegItem.Tag = firstNode;

                            _destRegEditContextMenu.Show(selectedTree, x, y);
                        }
                        else if (firstNode.IsDescendantOf(LaunchINI.RegistryKeys_Tag))
                        {
                            _editDestRegItem.Tag = firstNode;

                            _destRegKeyContextMenu.Show(selectedTree, x, y);
                        }
                        else if (firstNode.IsDescendantOf(LaunchINI.FileWriteN_Tag))
                        {
                            _editDestFileWriteNItem.Tag = firstNode;
                            _openDestFileWriteNItem.Tag = firstNode;

                            _destFileWriteNItemContextMenu.Show(selectedTree, x, y);
                        }
                    }
                    break;
            }
        }

        private void SourceTreeReg_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ShowSourceRegMenu(sender as TreeViewEx, e.X, e.Y);
            }
        }

        private void AppRegTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
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

        private void SearchAndReplaceSourceRegItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = _selectedTree.MultiSelectedNodes.FirstOrDefault();
            string startSearchStr = Clipboard.GetText().Trim();
            if (string.IsNullOrWhiteSpace(startSearchStr))
            {
                if (selectedNode != null)
                {
                    startSearchStr = selectedNode.Text;
                }
            }

            SearchAndReplaceForm form = new SearchAndReplaceForm();
            form.Search = startSearchStr;
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (selectedNode != null)
                {
                    if (!string.IsNullOrWhiteSpace(form.Search) && !string.IsNullOrWhiteSpace(form.Replace))
                    {
                        selectedNode.SearchandReplace(form.Search, form.Replace);
                    }
                }
            }
        }

        private void CopySourceRegItem_Click(object sender, EventArgs e)
        {
            ClipBoardExInfo.Inst.Tag = null;
            var firstNode = _selectedTree.MultiSelectedNodes.FirstOrDefault();
            if (firstNode != null)
            {
                string relativePath = firstNode.GetRegistryKey();
                Clipboard.SetText(relativePath);
                ClipBoardExInfo.Inst.Tag = (Action: "Copy", SelectedItems: _selectedTree.MultiSelectedNodes);
            }
        }

        private void EditDestNotepadRegItem_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripItem tempItem = sender as ToolStripItem;
                if (tempItem != null)
                {
                    var firstNode = _selectedTree.MultiSelectedNodes.FirstOrDefault();
                    if (firstNode != null)
                    {
                        var equalIndex = firstNode.Text.IndexOf("=");
                        if (equalIndex != -1)
                        {
                            var firstPart = firstNode.Text.Substring(0, equalIndex);
                            var regFileName = string.Format("\"{0}\\Data\\settings\\{1}.reg\"", Utility.UserSettings.Inst.PortableAppPath, firstPart);
                            System.Diagnostics.Process launchProc = new System.Diagnostics.Process();
                            launchProc.StartInfo.FileName = Utility.UserSettings.Inst.NotepadPath.Path;
                            launchProc.StartInfo.Arguments = regFileName;
                            launchProc.StartInfo.UseShellExecute = true;
                            launchProc.Start();
                        }
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void MergeDefaultRegItem_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripItem tempItem = sender as ToolStripItem;
                if (tempItem != null)
                {
                    var selectedNodeCount = _selectedTree.MultiSelectedNodes.Count;
                    if (selectedNodeCount == 1)
                    {
                        var firstNode = _selectedTree.MultiSelectedNodes.FirstOrDefault();
                        if (firstNode != null)
                        {
                            var equalIndex = firstNode.Text.IndexOf("=");
                            if (equalIndex != -1)
                            {
                                var firstPart = firstNode.Text.Substring(0, equalIndex);
                                var srcRegFile = string.Format("\"{0}\\Data\\settings\\{1}.reg\"", Utility.UserSettings.Inst.PortableAppPath, firstPart);
                                var destRegFile = string.Format("\"{0}\\App\\DefaultData\\settings\\{1}.reg\"", Utility.UserSettings.Inst.PortableAppPath, firstPart);
                                System.Diagnostics.Process launchProc = new System.Diagnostics.Process();
                                launchProc.StartInfo.FileName = Utility.UserSettings.Inst.DiffToolPath.Path;
                                launchProc.StartInfo.Arguments = string.Format("{0} {1}", srcRegFile, destRegFile);
                                launchProc.StartInfo.UseShellExecute = true;
                                launchProc.Start();
                            }
                        }
                    }
                    else if (selectedNodeCount == 2)
                    {
                        var firstNode = _selectedTree.MultiSelectedNodes[0];
                        var secondNode = _selectedTree.MultiSelectedNodes[1];
                        if (firstNode != null && secondNode != null)
                        {
                            var equalIndex1 = firstNode.Text.IndexOf("=");
                            var equalIndex2 = secondNode.Text.IndexOf("=");
                            if (equalIndex1 != -1 && equalIndex2 != -1)
                            {
                                var firstPart1 = firstNode.Text.Substring(0, equalIndex1);
                                var firstPart2 = secondNode.Text.Substring(0, equalIndex2);
                                var regFile1 = string.Format("\"{0}\\Data\\settings\\{1}.reg\"", Utility.UserSettings.Inst.PortableAppPath, firstPart1);
                                var regFile2 = string.Format("\"{0}\\Data\\settings\\{1}.reg\"", Utility.UserSettings.Inst.PortableAppPath, firstPart2);
                                System.Diagnostics.Process launchProc = new System.Diagnostics.Process();
                                launchProc.StartInfo.FileName = Utility.UserSettings.Inst.DiffToolPath.Path;
                                launchProc.StartInfo.Arguments = string.Format("{0} {1}", regFile1, regFile2);
                                launchProc.StartInfo.UseShellExecute = true;
                                launchProc.Start();
                            }
                            else
                            {
                                ErrorLog.Inst.ShowError("Not Supproted");
                            }
                        }
                    }
                    else
                    {
                        ErrorLog.Inst.ShowError("Multi node selection not suppoert, selecte one node to merge default reg file / select 2 node to merge with other reg file");
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void LaunchInRegEditRegItem_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripItem tempItem = sender as ToolStripItem;
                if (tempItem != null)
                {
                    var firstNode = _selectedTree.MultiSelectedNodes.FirstOrDefault();
                    if (firstNode != null)
                    {
                        var equalIndex = firstNode.Text.IndexOf("=");
                        if (equalIndex != -1)
                        {
                            var regKey = "";
                            if (firstNode.IsDescendantOf(LaunchINI.RegistryValueWrite_Tag))
                            {
                                var firstPart = firstNode.Text.Substring(0, equalIndex);
                                regKey = firstPart.Substring(0, firstPart.LastIndexOf('\\'));
                            }
                            else
                            {
                                regKey = firstNode.Text.Substring(equalIndex + 1);
                            }
                            var exePath = UserSettings.Inst.RegJumpPath;
                            var args = string.Format("\"{0}\"", regKey);
                            if (exePath.Path.EndsWith("RegScanner.exe", StringComparison.OrdinalIgnoreCase))
                            {
                                args = string.Format("/regedit \"{0}\"", regKey);
                            }
                            System.Diagnostics.Process launchProc = new System.Diagnostics.Process();
                            launchProc.StartInfo.FileName = exePath.Path;
                            launchProc.StartInfo.Verb = "runas";
                            launchProc.StartInfo.Arguments = args;
                            launchProc.StartInfo.UseShellExecute = true;
                            launchProc.Start();
                        }
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void GenerateCOMDllRegItem_Click(object sender, EventArgs e)
        {

        }

        private void AutoImportCOMDll_Click(object sender, EventArgs e)
        {
            TreeNode firstNode = _selectedTree?.SelectedNode;
            if (firstNode == null)
            {
                return;
            }
            TreeViewEx sourceRegTree = ((ToolStripItem)sender)?.Tag as TreeViewEx;
            var comDll = GetComDLLFiles(sourceRegTree);
            comDll.Sort();
            int startIndex = 1;
            firstNode.Nodes.Clear();
            foreach (var item in comDll)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    var fileInfo = new Model.FileInfo(item);
                    var newNode = firstNode.Nodes.Add(string.Format("{0}={1}", startIndex, fileInfo.RelativePath));
                    startIndex++;
                    newNode.Tag = fileInfo;
                }
            }
        }

        private void CopyDestRegItem_Click(object sender, EventArgs e)
        {
            List<string> listOfItemsCopied = new List<string>();
            var pathReverseLookup = new Dictionary<string, (string DataFolderPath, string PathInDisk)>();
            ClipBoardExInfo.Inst.Tag = pathReverseLookup;
            foreach (TreeNode selectedNode in _selectedTree.MultiSelectedNodes)
            {
                if (selectedNode.Parent != null)
                {
                    listOfItemsCopied.Add(selectedNode.Text);
                }
            }
            var clipTxt = listOfItemsCopied.ToStringMultiline();
            if (!string.IsNullOrWhiteSpace(clipTxt))
            {
                Clipboard.SetText(clipTxt);
            }

        }

        private void PasteSourceRegItem_Click(object sender, EventArgs e)
        {
            var firstNode = _selectedTree.MultiSelectedNodes.FirstOrDefault();
            if (firstNode != null)
            {
                var topMostNode = firstNode.GetTopNode();

                var clipTxt = Clipboard.GetText();
                List<string> clipListData = null;
                var parentNode = firstNode.Parent;
                if (!string.IsNullOrWhiteSpace(clipTxt))
                {
                    clipListData = clipTxt.ToListStr();
                }

                if (clipListData != null && clipListData.Count > 0)
                {
                    var clipTag = (Action: "Copy", SelectedItems: new List<TreeNode>());
                    var isDataFromSourceTree = false;
                    if (ClipBoardExInfo.Inst.Tag != null && ClipBoardExInfo.Inst.Tag is ValueTuple<string, List<TreeNode>>)
                    {
                        clipTag = ((string Action, List<TreeNode> SelectedItems))ClipBoardExInfo.Inst.Tag;
                        ClipBoardExInfo.Inst.Tag = null;
                        isDataFromSourceTree = true;
                    }

                    if (isDataFromSourceTree)
                    {
                        if (clipTag.Action == "Copy")
                        {
                            for (int index = 0; index < clipTag.SelectedItems.Count; index++)
                            {
                                var srcNode = clipTag.SelectedItems[index];
                                firstNode.Merge(srcNode);
                            }
                        }
                    }
                    else
                    {
                        ImportRegClipboard();
                    }
                }
            }
        }

        private void PasteDestFileWriteNItem_Click(object sender, EventArgs e)
        {
            var firstNode = _selectedTree.MultiSelectedNodes.FirstOrDefault();
            if (firstNode != null && firstNode.Parent == null && ClipBoardExInfo.Inst.Tag != null)
            {
                var clipTag = (Action: "Copy", SelectedItems: new List<TreeNode>());
                var isDataFromSourceTree = false;
                if (ClipBoardExInfo.Inst.Tag is ValueTuple<string, List<TreeNode>>)
                {
                    clipTag = ((string Action, List<TreeNode> SelectedItems))ClipBoardExInfo.Inst.Tag;
                    ClipBoardExInfo.Inst.Tag = null;
                    if (clipTag.Action == "Copy")
                    {
                        isDataFromSourceTree = true;
                    }
                }

                if (isDataFromSourceTree  && firstNode.Tag is FileWriteNSectionList selectedList)
                {
                    for (int index = 0; index < clipTag.SelectedItems.Count; index++)
                    {
                        var srcNode = clipTag.SelectedItems[index];
                        Model.RegInfo regInfo = srcNode.Tag as Model.RegInfo;
                        if (regInfo != null)
                        {
                            string regValue = "";
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
                            else if (regInfo.Kind == "REG_DWORD")
                            {
                                regValue = string.Format("dword:{0}", regInfo.Value);
                            }

                            var fileSection = new FileWriteNSection();

                            fileSection.Type = "INI";
                            fileSection.Section = string.Format("[{0}]", srcNode.GetRegistryKey());
                            fileSection.Key = regInfo.ValueName == "@" ? regInfo.ValueName : string.Format("\"{0}\"", regInfo.ValueName);
                            fileSection.Value = regValue;
                            fileSection.IniKey = string.Format("FileWrite{0}", selectedList.Count + 1);

                            var treeNode = firstNode.Nodes.Add(fileSection.FullValue);
                            treeNode.Tag = fileSection;
                            selectedList.Add(fileSection);
                            selectedList.UpdateIndex();
                        }
                    }
                }
                else
                {
                    ErrorLog.Inst.ShowError("Not Supported");
                }
            }
            else
            {
                ErrorLog.Inst.ShowError("Not Supported");
            }
        }

        private void PasteDestRegItem_Click(object sender, EventArgs e)
        {
            var firstNode = _selectedTree.MultiSelectedNodes.FirstOrDefault();
            if (firstNode != null)
            {
                var clipTxt = Clipboard.GetText();
                List<string> clipListData = null;

                if (!string.IsNullOrWhiteSpace(clipTxt))
                {
                    clipListData = clipTxt.ToListStr();
                }

                if (clipListData != null && clipListData.Count > 0)
                {
                    var clipTag = (Action: "Copy", SelectedItems: new List<TreeNode>());
                    var isDataFromSourceTree = false;
                    if (ClipBoardExInfo.Inst.Tag != null && ClipBoardExInfo.Inst.Tag is ValueTuple<string, List<TreeNode>>)
                    {
                        clipTag = ((string Action, List<TreeNode> SelectedItems))ClipBoardExInfo.Inst.Tag;
                        ClipBoardExInfo.Inst.Tag = null;
                        isDataFromSourceTree = true;
                    }

                    if (isDataFromSourceTree)
                    {
                        if (clipTag.Action == "Copy")
                        {
                            for (int index = 0; index < clipTag.SelectedItems.Count; index++)
                            {
                                var srcNode = clipTag.SelectedItems[index];
                                var regKey = srcNode.GetRegistryKey();
                                var regFilename = "-";
                                if (firstNode.Text == LaunchINI.RegistryKeys_Tag)
                                {
                                    regFilename = regKey.RegKeyToFileName();
                                }
                                else if (firstNode.Text == LaunchINI.RegistryValueWrite_Tag)
                                {
                                    //HKCU\SOFTWARE\RegKey\RegValueName=<TYPE>:Value

                                    Model.RegInfo regInfo = srcNode.Tag as Model.RegInfo;
                                    if (regInfo == null || (regInfo.Kind != "REG_SZ" && regInfo.Kind != "REG_DWORD"))
                                    {
                                        continue;
                                    }

                                    var regValue = regInfo.RegWriteValue.Replace("\\\\", "\\");
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
                                    regFilename = string.Format("{0}\\{1}", relPath, valueName);
                                    regKey = regValue;
                                }

                                var nodeCount = 1;
                                var newNodeText = string.Format("{0}={1}", regFilename, regKey);
                                do
                                {
                                    var existingNode = firstNode.Nodes.FindNode(newNodeText);
                                    if (existingNode == null)
                                    {
                                        var newNode = new TreeNode(newNodeText);
                                        if (firstNode.Text == LaunchINI.RegistryKeys_Tag)
                                        {
                                            newNode.Tag = srcNode;
                                        }
                                        firstNode.Nodes.Add(newNode);
                                        break;
                                    }
                                    else
                                    {
                                        newNodeText = string.Format("{0}_{1}={2}", regFilename, nodeCount, regKey);
                                        nodeCount++;
                                    }
                                } while (true);
                            }
                        }
                    }
                    else
                    {
                        for (int index = 0; index < clipListData.Count; index++)
                        {
                            var regFilename = "-";

                            var regKey = clipListData[index];
                            var foundIdx = regKey.IndexOf('\\');
                            if (foundIdx == -1)
                            {
                                continue;
                            }
                            var hkeyPart = regKey.Substring(0, foundIdx);
                            if (string.Equals(hkeyPart, "HKEY_LOCAL_MACHINE", StringComparison.OrdinalIgnoreCase))
                            {
                                regKey = string.Format("{0}{1}", "HKLM", regKey.Substring(foundIdx));
                            }
                            else if (string.Equals(hkeyPart, "HKEY_CURRENT_USER"))
                            {
                                regKey = string.Format("{0}{1}", "HKCU", regKey.Substring(foundIdx));
                            }
                            else
                            {
                                continue;
                            }

                            var foundIndex = clipListData[index].IndexOf('=');
                            if (foundIndex != -1)
                            {
                                regKey = clipListData[index].Substring(foundIndex + 1);
                            }
                            if (firstNode.Text == LaunchINI.RegistryKeys_Tag)
                            {
                                regFilename = regKey.RegKeyToFileName();
                            }
                            var nodeCount = 1;
                            var newNodeText = string.Format("{0}={1}", regFilename, regKey);
                            do
                            {
                                var existingNode = firstNode.Nodes.FindNode(newNodeText);
                                if (existingNode == null)
                                {
                                    var newNode = new TreeNode(newNodeText);
                                    firstNode.Nodes.Add(newNode);
                                    break;
                                }
                                else
                                {
                                    newNodeText = string.Format("{0}_{1}={2}", regFilename, nodeCount, regKey);
                                    nodeCount++;
                                }
                            } while (true);
                        }
                    }
                }
            }
        }

        private void DuplicateDestFileWriteNItem_Click(object sender, EventArgs e)
        {
            foreach (TreeNode selectedNode in _selectedTree.MultiSelectedNodes)
            {
                if (selectedNode.Parent != null && selectedNode.Parent.Tag is FileWriteNSectionList selectedList)
                {
                    var treeNode = (TreeNode)selectedNode.Clone();
                    selectedNode.Parent.Nodes.Add(treeNode);
                    var section = selectedNode.Tag as Model.LaunchINI.FileWriteNSection;
                    var newSection = section.Clone();
                    newSection.IniKey = string.Format("FileWrite{0}", selectedList.Count + 1);
                    treeNode.Tag = newSection;
                    selectedList.Add(newSection);
                    selectedList.UpdateIndex();
                }
            }
        }

        private void DuplicateDestRegItem_Click(object sender, EventArgs e)
        {
            foreach (TreeNode selectedNode in _selectedTree.MultiSelectedNodes)
            {
                if (selectedNode.Parent != null && selectedNode.Tag is Model.IINIKeyValuePair valuePair)
                {
                    selectedNode.Parent.Nodes.Add((TreeNode)selectedNode.Clone());
                }
            }
        }

        private void AddDestFileWriteNItem_Click(object sender, EventArgs e)
        {
            ToolStripItem tempItem = sender as ToolStripItem;
            if (tempItem != null && tempItem.Tag is TreeNode selectedNode)
            {
                FileWriteNForm editForm = GetEditForm(selectedNode.Text) as FileWriteNForm;
                editForm.Title = "Add";
                editForm.Index = selectedNode.Nodes.Count + 1;

                var fileSection = new FileWriteNSection();
                fileSection.Type = "INI";

                editForm.SelectedSection = fileSection;
                if (editForm.ShowDialog(this) == DialogResult.OK)
                {
                    var firstNode = _selectedTree.MultiSelectedNodes.FirstOrDefault();
                    if (firstNode != null && selectedNode.Tag is FileWriteNSectionList selectedList)
                    {
                        fileSection.IniKey = string.Format("FileWrite{0}", selectedList.Count + 1);
                        var treeNode = firstNode.Nodes.Add(fileSection.FullValue);
                        treeNode.Tag = fileSection;
                        selectedList.Add(fileSection);
                        selectedList.UpdateIndex();
                    }
                }
            }
            else
            {
                MessageBox.Show("Add not supported", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddDestRegItem_Click(object sender, EventArgs e)
        {
            ToolStripItem tempItem = sender as ToolStripItem;
            if (tempItem != null && tempItem.Tag is TreeNode selectedNode)
            {
                var editForm = GetEditForm(selectedNode.Text);
                editForm.Title = "Add";
                editForm.Index = selectedNode.Nodes.Count + 1;
                if (editForm.ShowDialog(this) == DialogResult.OK)
                {
                    var firstNode = _selectedTree.MultiSelectedNodes.FirstOrDefault();
                    if (firstNode != null)
                    {
                        firstNode.Nodes.Add(editForm.FullValue);
                    }
                }
            }
            else
            {
                MessageBox.Show("Add not supported", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenDestFileWriteNItem_Click(object sender, EventArgs e)
        {
            ToolStripItem tempItem = sender as ToolStripItem;
            if (tempItem != null && tempItem.Tag is TreeNode selectedNode)
            {
                FileWriteNForm editForm = GetEditForm(selectedNode.Parent.Text) as FileWriteNForm;
                var fileWriteSection = selectedNode.Tag as Model.LaunchINI.FileWriteNSection;

                var selectionPath = Environment.ExpandEnvironmentVariables(fileWriteSection.File);
                selectionPath = selectionPath.Replace("%PAL:AppDir%", string.Format("{0}\\App", UserSettings.Inst.PortableAppPath));
                selectionPath = selectionPath.Replace("%PAL:DataDir%", string.Format("{0}\\Data", UserSettings.Inst.PortableAppPath));

                if (File.Exists(selectionPath))
                {
                    EditInNotepad(selectionPath);
                }
                else if (Directory.Exists(selectionPath))
                {
                    OpenFolder(selectionPath);
                }
            }
        }
        private void EditDestFileWriteNItem_Click(object sender, EventArgs e)
        {
            ToolStripItem tempItem = sender as ToolStripItem;
            if (tempItem != null && tempItem.Tag is TreeNode selectedNode)
            {
                FileWriteNForm editForm = GetEditForm(selectedNode.Parent.Text) as FileWriteNForm;
                editForm.SelectedSection = selectedNode.Tag as Model.LaunchINI.FileWriteNSection;
                _selectedTree.SelectedNode = selectedNode;
                editForm.Title = "Edit";
                if (editForm.ShowDialog(this) == DialogResult.OK)
                {

                }
            }
        }

        private void EditDestRegKeyItem_Click(object sender, EventArgs e)
        {
            ToolStripItem tempItem = sender as ToolStripItem;
            if (tempItem != null && tempItem.Tag is TreeNode selectedNode)
            {
                var editForm = GetEditForm(selectedNode.Parent.Text);
                editForm.FullValue = selectedNode.Text;
                _selectedTree.SelectedNode = selectedNode;
                editForm.Title = "Edit";
                if (editForm.ShowDialog(this) == DialogResult.OK)
                {
                    selectedNode.Text = editForm.FullValue;
                }
            }
        }

        private void RemoveDestFileWriteNItem_Click(object sender, EventArgs e)
        {
            foreach (TreeNode selectedNode in _selectedTree.MultiSelectedNodes)
            {
                if (selectedNode.Parent != null && selectedNode.Parent.Tag is FileWriteNSectionList selectedList)
                {
                    var removeObj = selectedNode.Tag as Model.LaunchINI.FileWriteNSection;
                    if (removeObj != null)
                    {
                        if (removeObj is IDisposable disposable)
                        {
                            disposable.Dispose();
                        }
                        selectedList.Remove(removeObj);
                    }
                    selectedList.UpdateIndex();
                    selectedNode.Parent.Nodes.Remove(selectedNode);
                }
            }
        }

        private void RemoveDestRegItem_Click(object sender, EventArgs e)
        {
            bool showWarningMsg = true;
            bool continuePerform = false;
            foreach (TreeNode selectedNode in _selectedTree.MultiSelectedNodes)
            {
                if (selectedNode.Parent != null)
                {
                    var valuePair = selectedNode.Tag as Model.IINIKeyValuePair;
                    var fileInfo = selectedNode.Tag as Model.FileInfo;
                    var folderInfo = selectedNode.Tag as Model.FolderInfo;
                    if (valuePair != null)
                    {
                        valuePair.IsRemoved = true;
                    }
                    else if (fileInfo != null && fileInfo.CanDelete)
                    {
                        if (showWarningMsg)
                        {
                            continuePerform = ErrorLog.Inst.ShowYesNo("Do you want to delete this file. {0}", fileInfo.AbsolutePath);
                            showWarningMsg = false;
                        }
                        if (continuePerform)
                        {
                            try { File.Delete(fileInfo.AbsolutePath); } catch (Exception) { }
                        }
                    }
                    else if (folderInfo != null && folderInfo.CanDelete)
                    {
                        if (showWarningMsg)
                        {
                            continuePerform = ErrorLog.Inst.ShowYesNo("Do you want to delete this folder. {0}", folderInfo.FolderName);
                            showWarningMsg = false;
                        }
                        if (continuePerform)
                        {
                            try { Directory.Delete(folderInfo.FolderName, true); }
                            catch (Exception)
                            {
                                ErrorLog.Inst.ShowYesNo("First remove all files from this folder. {0}", folderInfo.FolderName);
                                break;
                            }
                        }
                    }
                    selectedNode.Parent.Nodes.Remove(selectedNode);
                }
            }
        }
    }
}
