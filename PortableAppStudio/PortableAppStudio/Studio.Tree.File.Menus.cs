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
        ContextMenuStrip _destFileINITopContextMenu = new ContextMenuStrip();
        private ToolStripItem _addDestFileINITopItem;
        private ToolStripItem _pastDestFileINITopItem;
        private ToolStripItem _expandAllDestFileINITopItem;
        private ToolStripMenuItem _environRedirDestFileINITopItem;

        ContextMenuStrip _destFileINIContextMenu = new ContextMenuStrip();
        private ToolStripItem _duplicateDestFileINIItem;
        private ToolStripItem _editDestFileINIItem;
        private ToolStripItem _copyDestFileINIItem;
        private ToolStripItem _openDestFileINIItem;
        private ToolStripItem _inspectDestFileINIItem;
        private ToolStripItem _removeDestFileINIItem;

        ContextMenuStrip _destFileTopContextMenu = new ContextMenuStrip();
        private ToolStripItem _addDestFileTopItem;
        private ToolStripItem _pasteDestFileTopItem;
        private ToolStripItem _expandAllDestFileTopItem;
        private ToolStripMenuItem _environRedirDestFileTopItem;


        ContextMenuStrip _destFileContextMenu = new ContextMenuStrip();
        private ToolStripItem _removeDestFileItem;
        private ToolStripItem _expandAllDestFileItem;
        private ToolStripItem _copyDestFileItem;
        private ToolStripItem _pasteDestFileItem;
        private ToolStripItem _openDestFileItem;
        private ToolStripItem _inspectDestFileItem;
        private ToolStripItem _addDestFileItem;

        ContextMenuStrip _sourceFileContextMenu = new ContextMenuStrip();
        private ToolStripItem _removeSourceFileItem;
        private ToolStripItem _expandAllSourceFileItem;
        private ToolStripItem _copySourceFileItem;
        private ToolStripItem _pasteSourceFileItem;

        private void UpdateFileTreeContextMenus()
        {
            //App and Data Node menus / folder files edit menu
            _destFileTopContextMenu.Items.Clear();
            _addDestFileTopItem = _destFileTopContextMenu.Items.Add("Add");
            _addDestFileTopItem.Click += AddDestFileTopItem_Click;
            _pasteDestFileTopItem = _destFileTopContextMenu.Items.Add("Paste");
            _pasteDestFileTopItem.Click += PasteDestFileTopItem_Click;
            _expandAllDestFileTopItem = _destFileTopContextMenu.Items.Add("ExpandAll");
            _expandAllDestFileTopItem.Click += ExpandNodes_Click;
            _environRedirDestFileTopItem = new ToolStripMenuItem("EnvironmentRedir");
            foreach (var item in PathManager.Init.PredefEnvironments)
            {
                var tempItem = _environRedirDestFileTopItem.DropDownItems.Add(item.Key);
                tempItem.Tag = (item.Key, item.Value);
                tempItem.Click += EnvironRedirDestFileTopItem_Click;
            }
            _destFileTopContextMenu.Items.Add(_environRedirDestFileTopItem);

            _destFileContextMenu.Items.Clear();
            _openDestFileItem = _destFileContextMenu.Items.Add("Open");
            _openDestFileItem.Click += OpenDestFileItem_Click;
            _inspectDestFileItem = _destFileContextMenu.Items.Add("Inspect");
            _inspectDestFileItem.Click += InspectDestFileItem_Click;
            _addDestFileItem = _destFileContextMenu.Items.Add("Add");
            _addDestFileItem.Click += AddDestFileTopItem_Click;
            _copyDestFileItem = _destFileContextMenu.Items.Add("Copy");
            _copyDestFileItem.Click += CopyDestFileItem_Click;
            _pasteDestFileItem = _destFileContextMenu.Items.Add("Paste");
            _pasteDestFileItem.Click += PasteDestFileTopItem_Click;
            _removeDestFileItem = _destFileContextMenu.Items.Add("Remove");
            _removeDestFileItem.Click += RemoveDestFileItem_Click;
            _expandAllDestFileItem = _destFileContextMenu.Items.Add("ExpandAll");
            _expandAllDestFileItem.Click += ExpandNodes_Click;

            //INI file section menus
            _destFileINITopContextMenu.Items.Clear();
            _addDestFileINITopItem = _destFileINITopContextMenu.Items.Add("Add");
            _addDestFileINITopItem.Click += AddDestFileTopItem_Click;
            _pastDestFileINITopItem = _destFileINITopContextMenu.Items.Add("Paste");
            _pastDestFileINITopItem.Click += PasteDestFileTopItem_Click;
            _expandAllDestFileINITopItem = _destFileINITopContextMenu.Items.Add("ExpandAll");
            _expandAllDestFileINITopItem.Click += ExpandNodes_Click;
            _environRedirDestFileINITopItem = new ToolStripMenuItem("EnvironmentRedir");
            foreach (var item in PathManager.Init.PredefEnvironments)
            {
                var tempItem = _environRedirDestFileINITopItem.DropDownItems.Add(item.Key);
                tempItem.Tag = (item.Key, item.Value);
                tempItem.Click += EnvironRedirFileINITopItem_Click;
            }
            _destFileINITopContextMenu.Items.Add(_environRedirDestFileINITopItem);

            _destFileINIContextMenu.Items.Clear();
            _openDestFileINIItem = _destFileINIContextMenu.Items.Add("Open");
            _openDestFileINIItem.Click += OpenDestFileINIItem_Click;
            _inspectDestFileINIItem = _destFileINIContextMenu.Items.Add("Inspect");
            _inspectDestFileINIItem.Click += InspectDestFileINIItem_Click;
            _copyDestFileINIItem = _destFileINIContextMenu.Items.Add("Copy");
            _copyDestFileINIItem.Click += CopyDestFileItem_Click;
            _editDestFileINIItem = _destFileINIContextMenu.Items.Add("Edit");
            _editDestFileINIItem.Click += EditDestFileINIItem_Click;
            _removeDestFileINIItem = _destFileINIContextMenu.Items.Add("Remove");
            _removeDestFileINIItem.Click += RemoveDestFileItem_Click;
            _duplicateDestFileINIItem = _destFileINIContextMenu.Items.Add("Duplicate");
            _duplicateDestFileINIItem.Click += DuplicateDestFileINIItem_Click;


            //Menu Items Common for both Files and Reg trees on source side
            _sourceFileContextMenu.Items.Clear();
            _expandAllSourceFileItem = _sourceFileContextMenu.Items.Add("ExpandAll");
            _expandAllSourceFileItem.Click += ExpandNodes_Click;
            _removeSourceFileItem = _sourceFileContextMenu.Items.Add("Remove");
            _removeSourceFileItem.Click += RemoveSourceItem_Click;
            _copySourceFileItem = _sourceFileContextMenu.Items.Add("Copy");
            _copySourceFileItem.Click += CopySourceFileItem_Click;
            _pasteSourceFileItem = _sourceFileContextMenu.Items.Add("Paste");
            _pasteSourceFileItem.Click += PasteSourceFileItem_Click;
        }

        public void ShowFilesMenu(TreeViewEx selectedTree, int x, int y)
        {
            _addDestFileTopItem.Tag = null;
            _pasteDestFileTopItem.Tag = null;
            _expandAllDestFileTopItem.Tag = null;
            _environRedirDestFileTopItem.Tag = null;
            _environRedirDestFileTopItem.Visible = false;

            _removeDestFileItem.Tag = null;
            _expandAllDestFileItem.Tag = null;
            _copyDestFileItem.Tag = null;
            _openDestFileItem.Tag = null;
            _inspectDestFileItem.Tag = null;
            _pasteDestFileItem.Tag = null;
            _addDestFileItem.Tag = null;

            _addDestFileINITopItem.Tag = null;
            _pastDestFileINITopItem.Tag = null;
            _expandAllDestFileTopItem.Tag = null;
            _environRedirDestFileINITopItem.Tag = null;
            _environRedirDestFileINITopItem.Visible = false;

            _duplicateDestFileINIItem.Tag = null;
            _editDestFileINIItem.Tag = null;
            _copyDestFileINIItem.Tag = null;
            _openDestFileINIItem.Tag = null;
            _inspectDestFileINIItem.Tag = null;
            _removeDestFileINIItem.Tag = null;

            TreeNode firstNode = selectedTree?.SelectedNode;
            if (firstNode == null)
            {
                return;
            }

            _selectedTree = selectedTree;
            _expandAllDestFileTopItem.Tag = firstNode;
            _expandAllDestFileItem.Tag = firstNode;

            switch (firstNode.Text)
            {
                case "App":
                case "DefaultData":
                case "Data":
                    {
                        _addDestFileTopItem.Tag = firstNode;
                        _pasteDestFileTopItem.Tag = firstNode;
                        _environRedirDestFileTopItem.Tag = firstNode;
                        _environRedirDestFileTopItem.Visible = firstNode.Text == "Data";
                        _destFileTopContextMenu.Show(selectedTree, x, y);
                    }
                    break;
                case LaunchINI.DirectoriesCleanupForce_Tag:
                case LaunchINI.DirectoriesCleanupIfEmpty_Tag:
                case LaunchINI.DirectoriesMove_Tag:
                case LaunchINI.FilesMove_Tag:
                case LaunchINI.DirectoriesLink_Tag:
                case LaunchINI.Environment_Tag:
                case LaunchINI.PrefixPATHEnv_Tag:
                    {
                        _environRedirDestFileINITopItem.Visible = firstNode.Text == LaunchINI.Environment_Tag;
                        _addDestFileINITopItem.Tag = firstNode;
                        _pastDestFileINITopItem.Tag = firstNode;
                        _environRedirDestFileINITopItem.Tag = firstNode;
                        _destFileINITopContextMenu.Show(selectedTree, x, y);
                    }
                    break;
                default:
                    {
                        if (firstNode.IsDescendantOf("App"))
                        {
                            if (!firstNode.IsDescendantOf("AppInfo"))
                            {
                                _removeDestFileItem.Tag = firstNode;
                                _expandAllDestFileItem.Tag = firstNode;
                                _copyDestFileItem.Tag = firstNode;
                                _openDestFileItem.Tag = firstNode;
                                _inspectDestFileItem.Tag = firstNode;
                                _pasteDestFileItem.Tag = firstNode;
                                _addDestFileItem.Tag = firstNode;

                                _destFileContextMenu.Show(selectedTree, x, y);
                            }
                        }
                        else if (firstNode.IsDescendantOf("Data"))
                        {

                            _removeDestFileItem.Tag = firstNode;
                            _expandAllDestFileItem.Tag = firstNode;
                            _copyDestFileItem.Tag = firstNode;
                            _openDestFileItem.Tag = firstNode;
                            _inspectDestFileItem.Tag = firstNode;
                            _pasteDestFileItem.Tag = firstNode;
                            _addDestFileItem.Tag = firstNode;

                            _destFileContextMenu.Show(selectedTree, x, y);
                        }
                        else if (firstNode.IsDescendantOf(LaunchINI.DirectoriesCleanupForce_Tag) ||
                           firstNode.IsDescendantOf(LaunchINI.DirectoriesCleanupIfEmpty_Tag) ||
                           firstNode.IsDescendantOf(LaunchINI.DirectoriesMove_Tag) ||
                           firstNode.IsDescendantOf(LaunchINI.FilesMove_Tag) ||
                           firstNode.IsDescendantOf(LaunchINI.DirectoriesLink_Tag) ||
                           firstNode.IsDescendantOf(LaunchINI.Environment_Tag) ||
                           firstNode.IsDescendantOf(LaunchINI.PrefixPATHEnv_Tag))
                        {
                            var tagValue = firstNode.GetTopNodeName();
                            _duplicateDestFileINIItem.Tag = tagValue;
                            _editDestFileINIItem.Tag = tagValue;
                            _copyDestFileINIItem.Tag = tagValue;
                            _openDestFileINIItem.Tag = tagValue;
                            _inspectDestFileINIItem.Tag = tagValue;

                            _destFileINIContextMenu.Show(selectedTree, x, y);
                        }
                    }
                    break;
            }
        }

        public void ShowSourceFileMenu(TreeViewEx selectedTree, int x, int y)
        {
            _removeSourceFileItem.Tag = null;
            _searchAndReplaceSourceRegItem.Tag = null;
            _copySourceFileItem.Tag = null;

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
                        _expandAllSourceFileItem.Tag = firstNode;
                        _sourceFileContextMenu.Show(selectedTree, x, y);
                    }
                    break;
            }
        }

        private void SourceTreeFile_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ShowSourceFileMenu(sender as TreeViewEx, e.X, e.Y);
            }
        }

        private void CopySourceFileItem_Click(object sender, EventArgs e)
        {
            var item = _selectedTree.MultiSelectedNodes.FirstOrDefault();
            if (item != null)
            {
                Clipboard.SetText(item.Text);
            }
        }

        private void DuplicateDestFileINIItem_Click(object sender, EventArgs e)
        {
            foreach (TreeNode selectedNode in _selectedTree.MultiSelectedNodes)
            {
                if (selectedNode.Parent != null)
                {
                    selectedNode.Parent.Nodes.Add((TreeNode)selectedNode.Clone());
                }
            }
        }

        private void OpenDestFileINIItem_Click(object sender, EventArgs e)
        {
            ToolStripItem tempItem = sender as ToolStripItem;
            if (tempItem != null)
            {
                var firstNode = _selectedTree.MultiSelectedNodes.FirstOrDefault();
                if (firstNode != null && firstNode.Parent != null)
                {
                    var valuePair = firstNode.Tag as Model.IINIKeyValuePair;
                    var fileInfo = firstNode.Tag as Model.FileInfo;
                    var folderInfo = firstNode.Tag as Model.FolderInfo;
                    TreeNode parentNode = firstNode.Parent;
                    if (valuePair != null)
                    {
                        string selectionPath = "";
                        if (UserSettings.Inst.IsPortableAppRunning)
                        {
                            selectionPath = string.Format("{0}", Environment.ExpandEnvironmentVariables(valuePair.IniValue));
                        }
                        else
                        {
                            if (parentNode?.Text == LaunchINI.Environment_Tag)
                            {
                                selectionPath = Environment.ExpandEnvironmentVariables(valuePair.IniValue);
                                selectionPath = selectionPath.Replace("%PAL:AppDir%", string.Format("{0}\\App", UserSettings.Inst.PortableAppPath));
                                selectionPath = selectionPath.Replace("%PAL:DataDir%", string.Format("{0}\\Data", UserSettings.Inst.PortableAppPath));
                            }
                            else
                            {
                                selectionPath = string.Format("{0}\\Data\\{1}", UserSettings.Inst.PortableAppPath, valuePair.IniKey);
                                if (!(File.Exists(selectionPath) || Directory.Exists(selectionPath)))
                                {
                                    selectionPath = string.Format("{0}\\App\\{1}", UserSettings.Inst.PortableAppPath, valuePair.IniKey);
                                }
                            }
                        }
                        if (File.Exists(selectionPath))
                        {
                            EditInNotepad(selectionPath);
                        }
                        else if (Directory.Exists(selectionPath))
                        {
                            OpenFolder(selectionPath);
                        }
                        else
                        {
                            selectionPath = string.Format("{0}", Environment.ExpandEnvironmentVariables(valuePair.IniValue));
                            if (File.Exists(selectionPath))
                            {
                                EditInNotepad(selectionPath);
                            }
                            else if (Directory.Exists(selectionPath))
                            {
                                OpenFolder(selectionPath);
                            }
                            else
                            {
                                ErrorLog.Inst.ShowError("Path not found: {0}", selectionPath);
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
        }

        private void OpenDestFileItem_Click(object sender, EventArgs e)
        {
            ToolStripItem tempItem = sender as ToolStripItem;
            if (tempItem != null)
            {
                var firstNode = _selectedTree.MultiSelectedNodes.FirstOrDefault();
                if (firstNode != null && firstNode.Parent != null)
                {
                    var valuePair = firstNode.Tag as Model.IINIKeyValuePair;
                    var fileInfo = firstNode.Tag as Model.FileInfo;
                    var folderInfo = firstNode.Tag as Model.FolderInfo;
                    if (valuePair != null)
                    {
                        ErrorLog.Inst.ShowError("Not Supproted");
                    }
                    else if (folderInfo != null || fileInfo != null)
                    {
                        if (firstNode.IsDescendantOf("Data"))
                        {
                            string selectionPath = string.Format("{0}\\Data\\{1}", UserSettings.Inst.PortableAppPath, firstNode.ToPortableFileRelativePath());
                            if (File.Exists(selectionPath))
                            {
                                EditInNotepad(selectionPath);
                            }
                            else if (Directory.Exists(selectionPath))
                            {
                                OpenFolder(selectionPath);
                            }
                        }
                        else if (firstNode.IsDescendantOf("App"))
                        {
                            string selectionPath = string.Format("{0}\\App\\{1}", UserSettings.Inst.PortableAppPath, firstNode.ToPortableFileRelativePath());
                            if (File.Exists(selectionPath))
                            {
                                EditInNotepad(selectionPath);
                            }
                            else if (Directory.Exists(selectionPath))
                            {
                                OpenFolder(selectionPath);
                            }
                        }
                        else if (firstNode.IsDescendantOf("Other"))
                        {
                            string selectionPath = string.Format("{0}\\Data\\{1}", UserSettings.Inst.PortableAppPath, firstNode.ToPortableFileRelativePath());
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
                }
                else
                {
                    ErrorLog.Inst.ShowError("Not supported");
                }
            }
        }

        private void InspectDestFileINIItem_Click(object sender, EventArgs e)
        {
            ToolStripItem tempItem = sender as ToolStripItem;
            if (tempItem != null)
            {
                var firstNode = _selectedTree.MultiSelectedNodes.FirstOrDefault();
                if (firstNode != null && firstNode.Parent != null)
                {
                    var valuePair = firstNode.Tag as Model.IINIKeyValuePair;
                    var fileInfo = firstNode.Tag as Model.FileInfo;
                    var folderInfo = firstNode.Tag as Model.FolderInfo;
                    if (valuePair != null)
                    {
                        string selectionPath = "";
                        selectionPath = string.Format("{0}", Environment.ExpandEnvironmentVariables(valuePair.IniValue));
                        if (File.Exists(selectionPath))
                        {
                            EditInNotepad(selectionPath);
                        }
                        else if (Directory.Exists(selectionPath))
                        {
                            OpenFolder(selectionPath);
                        }
                        else
                        {
                            selectionPath = string.Format("{0}", Environment.ExpandEnvironmentVariables(valuePair.IniValue));
                            if (File.Exists(selectionPath))
                            {
                                EditInNotepad(selectionPath);
                            }
                            else if (Directory.Exists(selectionPath))
                            {
                                OpenFolder(selectionPath);
                            }
                            else
                            {
                                ErrorLog.Inst.ShowError("Path not found: {0}", selectionPath);
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
        }

        private void InspectDestFileItem_Click(object sender, EventArgs e)
        {
            ToolStripItem tempItem = sender as ToolStripItem;
            if (tempItem != null)
            {
                var firstNode = _selectedTree.MultiSelectedNodes.FirstOrDefault();
                if (firstNode != null && firstNode.Parent != null)
                {
                    var valuePair = firstNode.Tag as Model.IINIKeyValuePair;
                    var fileInfo = firstNode.Tag as Model.FileInfo;
                    var folderInfo = firstNode.Tag as Model.FolderInfo;
                    if (valuePair != null)
                    {
                        ErrorLog.Inst.ShowError("Not Supproted");
                    }
                    else if (folderInfo != null || fileInfo != null)
                    {
                        if (firstNode.IsDescendantOf("Data"))
                        {
                            string selectionPath = string.Format("{0}\\Data\\{1}", UserSettings.Inst.PortableAppPath, firstNode.ToPortableFileRelativePath());
                            if (File.Exists(selectionPath))
                            {
                                EditInNotepad(selectionPath);
                            }
                            else if (Directory.Exists(selectionPath))
                            {
                                OpenFolder(selectionPath);
                            }
                        }
                        else if (firstNode.IsDescendantOf("App"))
                        {
                            string selectionPath = string.Format("{0}\\App\\{1}", UserSettings.Inst.PortableAppPath, firstNode.ToPortableFileRelativePath());
                            if (File.Exists(selectionPath))
                            {
                                EditInNotepad(selectionPath);
                            }
                            else if (Directory.Exists(selectionPath))
                            {
                                OpenFolder(selectionPath);
                            }
                        }
                        else if (firstNode.IsDescendantOf("Other"))
                        {
                            string selectionPath = string.Format("{0}\\Data\\{1}", UserSettings.Inst.PortableAppPath, firstNode.ToPortableFileRelativePath());
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
                }
                else
                {
                    ErrorLog.Inst.ShowError("Not supported");
                }
            }
        }
        private static void OpenFolder(string folderPath)
        {
            try
            {
                System.Diagnostics.Process launchProc = new System.Diagnostics.Process();
                launchProc.StartInfo.FileName = Utility.UserSettings.Inst.FileManager.Path;
                launchProc.StartInfo.Arguments = folderPath;
                launchProc.StartInfo.UseShellExecute = true;
                launchProc.Start();
            }
            catch (Exception)
            { }
        }

        private static void EditInNotepad(string filePath)
        {
            try
            {
                System.Diagnostics.Process launchProc = new System.Diagnostics.Process();
                launchProc.StartInfo.FileName = Utility.UserSettings.Inst.NotepadPath.Path;
                launchProc.StartInfo.Arguments = string.Format("\"{0}\"",filePath);
                launchProc.StartInfo.UseShellExecute = true;
                launchProc.Start();
            }
            catch (Exception) { }
        }

        private void PasteSourceFileItem_Click(object sender, EventArgs e)
        {
            var fileData = Clipboard.GetData(TreeNode_Drop_FileDrop) as string[];
            if (fileData != null)
            {
                var folderParser = new FolderParser();
                foreach (var item in fileData)
                {
                    ImportSourceFolder(folderParser, item);
                }
            }
        }

        private void PasteDestFileTopItem_Click(object sender, EventArgs e)
        {
            ToolStripItem tempItem = sender as ToolStripItem;
            if (tempItem != null)
            {
                var firstNode = _selectedTree.MultiSelectedNodes.FirstOrDefault();
                if (firstNode != null)
                {
                    var clipTxt = Clipboard.GetText();
                    List<string> clipListData = null;
                    bool isFileDrop = false;

                    if (!string.IsNullOrWhiteSpace(clipTxt))
                    {
                        clipListData = clipTxt.ToListStr();
                    }
                    else
                    {
                        // Get the DataObject.
                        IDataObject data_object = Clipboard.GetDataObject();
                        if (data_object != null)
                        {
                            var fileDropData = data_object.GetData(DataFormats.FileDrop, true) as string[];
                            if (fileDropData != null)
                            {
                                clipListData = new List<string>();
                                isFileDrop = true;
                                foreach (var item in fileDropData)
                                {
                                    clipListData.Add(item);
                                }
                            }
                        }
                    }

                    if (clipListData != null && clipListData.Count > 0)
                    {
                        int startIndex = 0;

                        switch (firstNode.Text)
                        {
                            case "DefaultData":
                            case "Data":
                                {
                                    if (isFileDrop)
                                    {
                                        for (int index = startIndex; index < clipListData.Count; index++)
                                        {
                                            if (Directory.Exists(clipListData[index]))
                                            {
                                                string folderName = clipListData[index].ToDataDirectoryName();
                                                firstNode.CreateFolderNodes(folderName);
                                            }
                                        }
                                    }
                                }
                                break;
                            case LaunchINI.Environment_Tag:
                            case LaunchINI.PrefixPATHEnv_Tag:
                                {
                                    if (isFileDrop)
                                    {
                                        for (int index = startIndex; index < clipListData.Count; index++)
                                        {
                                            if (Directory.Exists(clipListData[index]))
                                            {
                                                string folderName = clipListData[index].ToDiskRelativePath();
                                                var nodeIdx = firstNode.Nodes.Count + 1;
                                                firstNode.Nodes.Add(string.Format("Path{0}={1}", nodeIdx, folderName));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        for (int index = startIndex; index < clipListData.Count; index++)
                                        {
                                            if (clipListData[index].IndexOf('=') != -1)
                                            {
                                                firstNode.Nodes.Add(clipListData[index]);
                                            }
                                            else if (clipListData[index].IndexOf('%') == 0)
                                            {
                                                var nodeIdx = firstNode.Nodes.Count + 1;
                                                firstNode.Nodes.Add(string.Format("Path{0}={1}", nodeIdx, clipListData[index]));
                                            }
                                            else if(clipListData[index].IndexOf("Environment") != -1)
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
                                                        for (int indexrv = 0; indexrv < clipTag.SelectedItems.Count; indexrv++)
                                                        {
                                                            var srcNode = clipTag.SelectedItems[indexrv];
                                                            var regInfo = srcNode.Tag as Model.RegInfo;
                                                            if (regInfo != null)
                                                            {
                                                                firstNode.AppendINI(regInfo.ValueName, regInfo.RegStrValue);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                var nodeIdx = firstNode.Nodes.Count + 1;
                                                string dirPair = clipListData[index].ToDataDirectoryPair(nodeIdx);
                                                if (!string.IsNullOrWhiteSpace(dirPair))
                                                {
                                                    firstNode.Nodes.Add(dirPair);
                                                }
                                            }
                                        }
                                    }
                                }
                                break;
                            case LaunchINI.DirectoriesMove_Tag:
                            case LaunchINI.DirectoriesLink_Tag:
                                {
                                    if (isFileDrop)
                                    {
                                        for (int index = startIndex; index < clipListData.Count; index++)
                                        {
                                            if (Directory.Exists(clipListData[index]))
                                            {
                                                string dirPair = clipListData[index].ToDataDirectoryPair();
                                                if (!string.IsNullOrWhiteSpace(dirPair))
                                                {
                                                    firstNode.Nodes.Add(dirPair);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        var pathReverseLookup = ClipBoardExInfo.Inst.Tag as Dictionary<string, (string DataFolderPath, string PathInDisk)>;
                                        ClipBoardExInfo.Inst.Tag = null;
                                        for (int index = startIndex; index < clipListData.Count; index++)
                                        {
                                            if (clipListData[index].IndexOf('=') != -1)
                                            {
                                                firstNode.Nodes.Add(clipListData[index]);
                                            }
                                            else if (clipListData[index].IndexOf('%') == 0)
                                            {
                                                string leftVal = "-";
                                                string rightVal = clipListData[index];
                                                if (pathReverseLookup != null)
                                                {
                                                    if (pathReverseLookup.TryGetValue(clipListData[index], out (string DataFolderPath, string PathInDisk) tempOutVal))
                                                    {
                                                        if (tempOutVal.DataFolderPath != null)
                                                        {
                                                            leftVal = tempOutVal.DataFolderPath;
                                                        }
                                                        if (tempOutVal.PathInDisk != null)
                                                        {
                                                            rightVal = tempOutVal.PathInDisk;
                                                        }
                                                    }
                                                }
                                                firstNode.Nodes.Add(string.Format("{0}={1}", leftVal, rightVal));
                                            }
                                            else
                                            {
                                                string dirPair = clipListData[index].ToDataDirectoryPair();
                                                if (!string.IsNullOrWhiteSpace(dirPair))
                                                {
                                                    firstNode.Nodes.Add(dirPair);
                                                }
                                            }
                                        }
                                    }
                                }
                                break;
                            case LaunchINI.DirectoriesCleanupForce_Tag:
                            case LaunchINI.DirectoriesCleanupIfEmpty_Tag:
                                {
                                    int dirIndex = firstNode.Nodes.Count + 1;
                                    if (isFileDrop)
                                    {
                                        for (int index = startIndex; index < clipListData.Count; index++)
                                        {
                                            if (Directory.Exists(clipListData[index]))
                                            {
                                                string dirPair = PathManager.Init.GetExpandablePath(clipListData[index]);
                                                if (!string.IsNullOrWhiteSpace(dirPair))
                                                {
                                                    firstNode.Nodes.Add(string.Format("{0}={1}", dirIndex, dirPair));
                                                    dirIndex++;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        var pathReverseLookup = ClipBoardExInfo.Inst.Tag as Dictionary<string, (string DataFolderPath, string PathInDisk)>;
                                        ClipBoardExInfo.Inst.Tag = null;
                                        for (int index = startIndex; index < clipListData.Count; index++)
                                        {
                                            if (clipListData[index].IndexOf('=') != -1)
                                            {
                                                var spiltVals = clipListData[index].Split('=');
                                                if (spiltVals.Length > 1)
                                                {
                                                    firstNode.Nodes.Add(string.Format("{0}={1}", dirIndex, spiltVals[1]));
                                                    dirIndex++;
                                                }
                                            }
                                            else if (clipListData[index].IndexOf('%') == 0)
                                            {
                                                string rightVal = clipListData[index];
                                                if (pathReverseLookup != null)
                                                {
                                                    if (pathReverseLookup.TryGetValue(clipListData[index], out (string DataFolderPath, string PathInDisk) tempOutVal))
                                                    {
                                                        if (tempOutVal.PathInDisk != null)
                                                        {
                                                            rightVal = tempOutVal.PathInDisk;
                                                        }
                                                    }
                                                }
                                                firstNode.Nodes.Add(string.Format("{0}={1}", dirIndex, rightVal));
                                                dirIndex++;
                                            }
                                            else
                                            {
                                                string dirPair = PathManager.Init.GetExpandablePath(clipListData[index]);
                                                if (!string.IsNullOrWhiteSpace(dirPair))
                                                {
                                                    firstNode.Nodes.Add(string.Format("{0}={1}", dirIndex, dirPair));
                                                    dirIndex++;
                                                }
                                            }
                                        }
                                    }
                                }
                                break;
                            case LaunchINI.FilesMove_Tag:
                                {
                                    if (isFileDrop)
                                    {
                                        for (int index = startIndex; index < clipListData.Count; index++)
                                        {
                                            if (Directory.Exists(clipListData[index]))
                                            {
                                                string dirPair = clipListData[index].ToDataFilePair();
                                                if (!string.IsNullOrWhiteSpace(dirPair))
                                                {
                                                    firstNode.Nodes.Add(dirPair);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        var pathReverseLookup = ClipBoardExInfo.Inst.Tag as Dictionary<string, (string DataFolderPath, string PathInDisk)>;
                                        ClipBoardExInfo.Inst.Tag = null;
                                        for (int index = startIndex; index < clipListData.Count; index++)
                                        {
                                            if (clipListData[index].IndexOf('=') != -1)
                                            {
                                                firstNode.Nodes.Add(clipListData[index]);
                                            }
                                            else if (clipListData[index].IndexOf('%') == 0)
                                            {
                                                string leftVal = "-";
                                                string rightVal = clipListData[index];
                                                if (pathReverseLookup != null)
                                                {
                                                    if (pathReverseLookup.TryGetValue(clipListData[index], out (string DataFolderPath, string PathInDisk) tempOutVal))
                                                    {
                                                        if (tempOutVal.DataFolderPath != null)
                                                        {
                                                            leftVal = tempOutVal.DataFolderPath;
                                                        }
                                                        if (tempOutVal.PathInDisk != null)
                                                        {
                                                            rightVal = tempOutVal.PathInDisk;
                                                        }
                                                    }
                                                }
                                                firstNode.Nodes.Add(string.Format("{0}={1}", leftVal, rightVal));
                                            }
                                            else
                                            {
                                                string dirPair = clipListData[index].ToDataFilePair();
                                                if (!string.IsNullOrWhiteSpace(dirPair))
                                                {
                                                    firstNode.Nodes.Add(dirPair);
                                                }
                                            }
                                        }
                                    }
                                }
                                break;
                        }
                    }
                }
            }
        }

        private void CopyDestFileItem_Click(object sender, EventArgs e)
        {
            List<string> listOfItemsCopied = new List<string>();
            var pathReverseLookup = new Dictionary<string, (string DataFolderPath, string PathInDisk)>();
            ClipBoardExInfo.Inst.Tag = pathReverseLookup;
            foreach (TreeNode selectedNode in _selectedTree.MultiSelectedNodes)
            {
                if (selectedNode.Parent != null)
                {
                    var valuePair = selectedNode.Tag as Model.IINIKeyValuePair;
                    var fileInfo = selectedNode.Tag as Model.FileInfo;
                    var folderInfo = selectedNode.Tag as Model.FolderInfo;
                    if (valuePair != null)
                    {
                        listOfItemsCopied.Add(valuePair.FullValue);
                    }
                    else if (folderInfo != null || fileInfo != null)
                    {
                        if (selectedNode.IsDescendantOf("Data"))
                        {
                            string datafolderPath = selectedNode.ToPortableFileRelativePath();
                            string fullPathInDisk = null;
                            var backSlash = datafolderPath.IndexOf('\\');
                            if (backSlash != -1)
                            {
                                var topFolderName = datafolderPath.Substring(0, backSlash);
                                var tailPart = datafolderPath.Substring(backSlash);
                                var envInfo = PathManager.Init.SystemEnvironments.TryGetEnvironment(topFolderName);
                                if (envInfo != null)
                                {
                                    fullPathInDisk = string.Format("{0}{1}", envInfo.RelativePath, tailPart);
                                }
                            }

                            var folderClpValue = string.Format("%PAL:DataDir%\\{0}", datafolderPath);
                            listOfItemsCopied.Add(folderClpValue);
                            pathReverseLookup.Add(folderClpValue, (datafolderPath, fullPathInDisk));
                        }
                        else if (selectedNode.IsDescendantOf("App"))
                        {
                            string appfolderPath = selectedNode.ToPortableFileRelativePath();
                            var folderClpValue = string.Format("%PAL:AppDir%\\{0}", appfolderPath);
                            listOfItemsCopied.Add(folderClpValue);
                            pathReverseLookup.Add(folderClpValue, (appfolderPath, null));
                        }
                    }
                }
            }
            var clipTxt = listOfItemsCopied.ToStringMultiline();
            if (!string.IsNullOrWhiteSpace(clipTxt))
            {
                Clipboard.SetText(clipTxt);
            }
        }

        private void EnvironRedirFileINITopItem_Click(object sender, EventArgs e)
        {
            ToolStripItem tempItem = sender as ToolStripItem;
            if (tempItem != null)
            {
                var parent = tempItem.OwnerItem;
                var firstNode = parent != null ? parent.Tag as TreeNode : null;
                if (firstNode == null)
                {
                    MessageBox.Show("Add not supported", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    var (iniKey, iniValue) = (ValueTuple<string, string>)tempItem.Tag;
                    firstNode.AppendINI(iniKey, iniValue);
                }
            }
        }

        private void EnvironRedirDestFileTopItem_Click(object sender, EventArgs e)
        {
            ToolStripItem tempItem = sender as ToolStripItem;
            if (tempItem != null)
            {
                var parent = tempItem.OwnerItem;
                var firstNode = parent != null ? parent.Tag as TreeNode : null;
                if (firstNode == null)
                {
                    MessageBox.Show("Add not supported", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    var (iniKey, iniValue) = (ValueTuple<string, string>)tempItem.Tag;
                    int startPos = iniValue.IndexOf('\\');
                    if (startPos != -1)
                    {
                        firstNode.Nodes.Add(iniValue.Substring(startPos + 1));
                    }
                }
            }
        }

        private void AddDestFileTopItem_Click(object sender, EventArgs e)
        {
            ToolStripItem tempItem = sender as ToolStripItem;
            if (tempItem != null)
            {
                var selectedNode = tempItem.Tag as TreeNode;
                if (selectedNode == null)
                {
                    MessageBox.Show("Add not supported", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
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
            }
        }

        private void EditDestFileINIItem_Click(object sender, EventArgs e)
        {
            ToolStripItem tempItem = sender as ToolStripItem;
            if (tempItem != null)
            {
                var editForm = GetEditForm(tempItem.Tag as string);
                var firstNode = _selectedTree.MultiSelectedNodes.FirstOrDefault();
                if (firstNode != null)
                {
                    editForm.FullValue = firstNode.Text;
                    _selectedTree.SelectedNode = firstNode;
                }
                editForm.Title = "Edit";
                if (editForm.ShowDialog(this) == DialogResult.OK)
                {
                    firstNode.Text = editForm.FullValue;
                }
            }
        }

        private void RemoveDestFileItem_Click(object sender, EventArgs e)
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
