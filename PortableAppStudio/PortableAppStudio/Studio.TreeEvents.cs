﻿using PortableAppStudio.Controls;
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
        private void SourceTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ShowSourceMenu(sender as TreeViewEx, e.X, e.Y);
            }
        }

        private void appInfoTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string selectedProprtyName = "";
            TreeNode showNode = null;
            if (e.Node.Parent == null)
            {
                showNode = e.Node;
                selectedProprtyName = "";
            }
            else if (e.Node.Parent != null && e.Node.Parent.Parent == null)
            {
                showNode = e.Node.Parent;
                int endPos = e.Node.Text.IndexOf('=');
                if (endPos != -1)
                {
                    selectedProprtyName = e.Node.Text.Substring(0, endPos);
                }
            }

            if (showNode != null)
            {
                Model.IINIList retListValue;
                object retObjValue;
                PortableApp.Inst.App.AppInfo.GetSectionToEdit(showNode.Text, out retObjValue, out retListValue);
                if (retListValue != null)
                {
                    appInfoEditor.SelectedObjects(retListValue,showNode.Text, selectedProprtyName);
                }
                else if (retObjValue != null)
                {
                    appInfoEditor.SelectedObject(retObjValue,showNode.Text, selectedProprtyName);
                }
            }
        }

        private void launchTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string selectedProprtyName = "";
            string sectionName = "";
            TreeNode showNode = null;
            if (e.Node.Parent == null)
            {
                showNode = e.Node;
                sectionName = e.Node.Text;
                selectedProprtyName = "";
            }
            else if (e.Node.Text == Model.LaunchINI.LaunchINI.WaitForEXEN_Tag)
            {
                showNode = e.Node;
                sectionName = "[Launch]";
                selectedProprtyName = e.Node.Text;
            }
            else if (e.Node.Parent != null && e.Node.Parent.Parent == null)
            {
                showNode = e.Node.Parent;
                int endPos = e.Node.Text.IndexOf('=');
                if (endPos != -1)
                {
                    selectedProprtyName = e.Node.Text.Substring(0, endPos);
                }
            }

            if (showNode != null)
            {
                Model.IINIList retListValue;
                object retObjValue;
                PortableApp.Inst.App.Launch.GetSectionToEdit(showNode.Text, out retObjValue, out retListValue);

                if (retListValue != null)
                {
                    launchEditor.SelectedObjects(retListValue,showNode.Text,selectedProprtyName);
                }
                else if (retObjValue != null)
                {
                    launchEditor.SelectedObject(retObjValue,showNode.Text,selectedProprtyName);
                }
            }
        }

        public TreeViewEx _selectedTree { get; set; }
        public Control EditControlParent { get; set; }

        ContextMenuStrip _destRegFreeComContextMenu = new ContextMenuStrip();
        private ToolStripItem _importCOMDllRegItem;
        private ToolStripItem _generateCOMDllRegItem;
        

        ContextMenuStrip _destRegContextMenu = new ContextMenuStrip();
        private ToolStripItem _addDestRegItem;
        private ToolStripItem _editDestRegItem;
        private ToolStripItem _removeDestRegItem;
        private ToolStripItem _duplicateDestRegItem;
        private ToolStripItem _addFireWallregItem;
        private ToolStripItem _addCOMComponentsregItem;

        ContextMenuStrip _sourceContextMenu = new ContextMenuStrip();
        private ToolStripItem _removeSourceItem;
        private ToolStripItem _expandAllSourceItem;
        private ToolStripItem _searchAndReplaceSourceItem;
        private ToolStripItem _copySourceItem;


        ContextMenuStrip _destFileContextMenu = new ContextMenuStrip();
        private ToolStripItem _addDestFilesItem;
        private ToolStripItem _editDestFilesItem;
        private ToolStripItem _removeDestFilesItem;
        private ToolStripItem _duplicateDestFilesItem;
        private ToolStripItem _expandAllItem;
        private ToolStripItem _convert64To32Env;
        private ToolStripItem _filePasteItems;
        private ToolStripItem _fileCopyItems;

        private void UpdateTreeContextMenus()
        {
            _destFileContextMenu.Items.Clear();
            _fileCopyItems = _destFileContextMenu.Items.Add("Copy");
            _fileCopyItems.Click += FileCopyItems_Click;
            _filePasteItems = _destFileContextMenu.Items.Add("Paste");
            _filePasteItems.Click += FilePasteItems_Click;
            _expandAllItem = _destFileContextMenu.Items.Add("ExpandAll");
            _expandAllItem.Click += ExpandAllItem_Click;
            _addDestFilesItem = _destFileContextMenu.Items.Add("Add");
            _addDestFilesItem.Click += AddItem_Click;
            _editDestFilesItem = _destFileContextMenu.Items.Add("Edit");
            _editDestFilesItem.Click += EditItem_Click;
            _removeDestFilesItem = _destFileContextMenu.Items.Add("Remove");
            _removeDestFilesItem.Click += RemoveItem_Click;
            _duplicateDestFilesItem = _destFileContextMenu.Items.Add("Duplicate");
            _duplicateDestFilesItem.Click += DuplicateRegItem_Click;
            _convert64To32Env = _destFileContextMenu.Items.Add("Convert6432Environment");
            _convert64To32Env.Click += Convert64To32Env_Click;

            _sourceContextMenu.Items.Clear();
            _expandAllSourceItem = _sourceContextMenu.Items.Add("ExpandAll");
            _expandAllSourceItem.Click += ExpandAllItem_Click;
            _removeSourceItem = _sourceContextMenu.Items.Add("Remove");
            _removeSourceItem.Click += RemoveSourceItem_Click;
            _searchAndReplaceSourceItem = _sourceContextMenu.Items.Add("SearchAndReplace");
            _searchAndReplaceSourceItem.Click += SearchAndReplaceSourceItem_Click;
            _copySourceItem = _sourceContextMenu.Items.Add("Copy");
            _copySourceItem.Click += CopySourceItem_Click;


            _destRegContextMenu.Items.Clear();
            _addDestRegItem = _destRegContextMenu.Items.Add("Add");
            _addDestRegItem.Click += AddItem_Click;
            _editDestRegItem = _destRegContextMenu.Items.Add("Edit");
            _editDestRegItem.Click += EditItem_Click;
            _removeDestRegItem = _destRegContextMenu.Items.Add("Remove");
            _removeDestRegItem.Click += RemoveItem_Click;
            _duplicateDestRegItem = _destRegContextMenu.Items.Add("Duplicate");
            _duplicateDestRegItem.Click += DuplicateRegItem_Click;
            _addFireWallregItem = _destRegContextMenu.Items.Add("Add Firewall Block");
            _addFireWallregItem.Click += AddFireWallregItem_Click;
            _addCOMComponentsregItem = _destRegContextMenu.Items.Add("Add COM Components");
            _addCOMComponentsregItem.Click += _addCOMComponentsregItem_Click;

            _destRegFreeComContextMenu.Items.Clear();
            _importCOMDllRegItem = _destRegFreeComContextMenu.Items.Add("Auto Import COM");
            _importCOMDllRegItem.Click += AutoImportCOMDll_Click;
            _generateCOMDllRegItem = _destRegFreeComContextMenu.Items.Add("Generate Manifest");
            _generateCOMDllRegItem.Click += GenerateCOMDllRegItem_Click;
        }

        

        private void GenerateCOMDllRegItem_Click(object sender, EventArgs e)
        {
            
        }

        private void Convert64To32Env_Click(object sender, EventArgs e)
        {
            foreach (TreeNode selectedNode in _selectedTree.MultiSelectedNodes)
            {
                var tempVal = selectedNode.Text;
                foreach(var item in PathManager.Init.SystemEnvironments)
                {
                    var tempVal1 = tempVal.Replace(item.Key, item.Value.DisplayName);
                    tempVal = tempVal1;
                }
                selectedNode.Text = tempVal;
            }
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

        private void ExpandAllItem_Click(object sender, EventArgs e)
        {
            ToolStripItem tempItem = sender as ToolStripItem;
            if (tempItem != null)
            {
                var selectedNode = tempItem.Tag as TreeNode;
                if (selectedNode != null)
                {
                    selectedNode.ExpandAll();
                }
            }
        }

        private void RemoveSourceItem_Click(object sender, EventArgs e)
        {
            foreach (TreeNode selectedNode in _selectedTree.MultiSelectedNodes)
            {
                if (selectedNode.Parent != null)
                {
                    selectedNode.Parent.Nodes.Remove(selectedNode);
                }
            }
        }

        private void SearchAndReplaceSourceItem_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = _selectedTree.MultiSelectedNodes.FirstOrDefault();
            string startSearchStr = Clipboard.GetText().Trim();
            if(string.IsNullOrWhiteSpace(startSearchStr))
            {
                if(selectedNode != null)
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
                    if(!string.IsNullOrWhiteSpace(form.Search) && !string.IsNullOrWhiteSpace(form.Replace))
                    {
                        selectedNode.SearchandReplace(form.Search, form.Replace);
                    }
                }
            }
        }

        private void CopySourceItem_Click(object sender, EventArgs e)
        {
            var item = _selectedTree.MultiSelectedNodes.FirstOrDefault();
            if(item != null)
            {
                Clipboard.SetText(item.Text);
            }
        }

        private void DuplicateRegItem_Click(object sender, EventArgs e)
        {
            foreach (TreeNode selectedNode in _selectedTree.MultiSelectedNodes)
            {
                if (selectedNode.Parent != null)
                {
                    var valuePair = selectedNode.Tag as Model.IINIKeyValuePair;
                    if (valuePair != null)
                    {
                    }
                    selectedNode.Parent.Nodes.Add((TreeNode)selectedNode.Clone());
                }
            }
        }



        private void RemoveItem_Click(object sender, EventArgs e)
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

        private void EditItem_Click(object sender, EventArgs e)
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


        private void _addCOMComponentsregItem_Click(object sender, EventArgs e)
        {
            ToolStripItem tempItem = sender as ToolStripItem;
            if (tempItem != null)
            {
                var selectedNode = tempItem.Tag as TreeNode;
                var firstNode = _selectedTree.MultiSelectedNodes.FirstOrDefault();
                if (firstNode != null)
                {
                    foreach (var item in Model.COMComponentsListStringConverter.GetRegistryWriteList())
                    {
                        firstNode.Nodes.Add(item);
                    }
                }
            }
        }

        private void AddFireWallregItem_Click(object sender, EventArgs e)
        {
            ToolStripItem tempItem = sender as ToolStripItem;
            if (tempItem != null)
            {
                var selectedNode = tempItem.Tag as TreeNode;
                var firstNode = _selectedTree.MultiSelectedNodes.FirstOrDefault();
                if (firstNode != null)
                {
                    foreach (var item in Model.FirewallListStringConverter.GetRegistryWriteList())
                    {
                        firstNode.Nodes.Add(item);
                    }
                }
            }
        }

        private void FilePasteItems_Click(object sender, EventArgs e)
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

                    if(!string.IsNullOrWhiteSpace(clipTxt))
                    {
                        clipListData = clipTxt.ToListStr();
                    }
                    else
                    {
                        // Get the DataObject.
                        IDataObject data_object = Clipboard.GetDataObject();
                        
                        if(data_object != null)
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
                        
                        bool dataFromDirMoveOrLinkNode = clipListData.Count > 1 ? clipListData[0] == LaunchINI.DirectoriesMove_Tag || clipListData[0] == LaunchINI.DirectoriesLink_Tag : false;
                        int startIndex = dataFromDirMoveOrLinkNode ? 1 : 0;

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
                                                var foundEnv = PathManager.Init.GetEnvironmentInfo(clipListData[index]);
                                                var folderInfo = new Model.FolderInfo(clipListData[index]);
                                                string dirPair = clipListData[index].ToDirectoryPair();
                                                if (!string.IsNullOrWhiteSpace(dirPair))
                                                {
                                                    firstNode.Nodes.Add(dirPair);
                                                }

                                                string folderName = clipListData[index].ToDirectoryName();
                                                var existingNode = firstNode.Nodes.FindNode(folderName);
                                                if (existingNode == null)
                                                {
                                                    var destTreeNode = new TreeNode(folderName);
                                                    firstNode.Nodes.Add(destTreeNode);
                                                    firstNode.Expand();
                                                }
                                            }
                                        }
                                    }
                                }
                                break;
                            case LaunchINI.DirectoriesMove_Tag:
                            case LaunchINI.DirectoriesLink_Tag:
                                {
                                    if(isFileDrop)
                                    {
                                        for (int index = startIndex; index < clipListData.Count; index++)
                                        {
                                            if (Directory.Exists(clipListData[index]))
                                            {
                                                string dirPair = clipListData[index].ToDirectoryPair();
                                                if (!string.IsNullOrWhiteSpace(dirPair))
                                                {
                                                    firstNode.Nodes.Add(dirPair);
                                                }
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
                                            else if(clipListData[index].IndexOf('%') == 0)
                                            {
                                                firstNode.Nodes.Add(string.Format("-={0}",clipListData[index]));
                                            }
                                            else
                                            {
                                                string dirPair = clipListData[index].ToDirectoryPair();
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
                                                    firstNode.Nodes.Add(string.Format("{0}={1}",dirIndex,dirPair));
                                                    dirIndex++;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
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
                                                firstNode.Nodes.Add(string.Format("{0}={1}", dirIndex,clipListData[index]));
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
                        }
                    }
                }
            }
        }

        private void FileCopyItems_Click(object sender, EventArgs e)
        {
            List<string> listOfItemsCopied = new List<string>();
            var firstNode = _selectedTree.MultiSelectedNodes.FirstOrDefault();
            if (firstNode != null)
            {
                listOfItemsCopied.Add(firstNode.Parent.Text);
            }

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
                }
            }
            var clipTxt = listOfItemsCopied.ToStringMultiline();
            if (!string.IsNullOrWhiteSpace(clipTxt))
            {
                Clipboard.SetText(clipTxt);
            }
        }
        private void AddItem_Click(object sender, EventArgs e)
        {
            ToolStripItem tempItem = sender as ToolStripItem;
            if (tempItem != null)
            {
                var selectedNode = tempItem.Tag as TreeNode;
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
}
