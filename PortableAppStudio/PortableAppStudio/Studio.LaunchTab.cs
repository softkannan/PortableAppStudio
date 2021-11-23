using PortableAppStudio.Controls;
using PortableAppStudio.Dialogs;
using PortableAppStudio.Model.AppInfoINI;
using PortableAppStudio.Model.FolderLayout;
using PortableAppStudio.Model.LaunchINI;
using PortableAppStudio.Parser;
using PortableAppStudio.Properties;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppStudio
{
    partial class MainStudio
    {
        ContextMenuStrip _launchTreeContextMenu = new ContextMenuStrip();
        private ToolStripItem _updateWaitForExeNLaunchItem;
        private ToolStripItem _updateKillProcNLaunchItem;
        private ToolStripItem _addProcLaunchItem;
        private ToolStripItem _updateDynamicLaunchItem;

        private void LaunchTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                ShowLaunchIniMenu(sender as TreeViewEx, e.X, e.Y);
            }
        }
        private void LaunchTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string selectedProprtyName = "";
            TreeNode showNode = null;
            if (e.Node.Parent == null)
            {
                showNode = e.Node;
                selectedProprtyName = "";
            }
            else if (e.Node.Text == Model.LaunchINI.LaunchINI.WaitForEXEN_Tag)
            {
                showNode = e.Node;
                selectedProprtyName = e.Node.Text;
            }
            else if (e.Node.Text == Model.LaunchINI.LaunchINI.KillProcN_Tag)
            {
                showNode = e.Node;
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

            UpdateLaunchPropertyEditor(selectedProprtyName, showNode);
        }

        private void UpdateLaunchPropertyEditor(string selectedProprtyName, TreeNode showNode)
        {
            if (showNode != null)
            {
                Model.IINIList retListValue;
                object retObjValue;
                PortableApp.Inst.App.Launch.GetSectionToEdit(showNode.Text, out retObjValue, out retListValue);

                if (retListValue != null)
                {
                    launchEditor.SelectedObjects(retListValue, showNode.Text, selectedProprtyName);
                }
                else if (retObjValue != null)
                {
                    launchEditor.SelectedObject(retObjValue, showNode.Text, selectedProprtyName);
                }
            }
        }

        void UpdateLaunchTreeContextMenus()
        {
            _launchTreeContextMenu.Items.Clear();
            _addProcLaunchItem = _launchTreeContextMenu.Items.Add("Add EXE");
            _addProcLaunchItem.Click += AddProcLaunchItem_Click;
            _updateDynamicLaunchItem = _launchTreeContextMenu.Items.Add("Update Dynamic");
            _updateDynamicLaunchItem.Click += UpdateDynamicLaunchItem_Click; ;
            _updateWaitForExeNLaunchItem = _launchTreeContextMenu.Items.Add("Force UpdateWaitForExeN");
            _updateWaitForExeNLaunchItem.Click += UpdateWaitForExeNLaunchItem_Click;
            _updateKillProcNLaunchItem = _launchTreeContextMenu.Items.Add("Force UpdateKillProcN");
            _updateKillProcNLaunchItem.Click += UpdateKillProcNLaunchItem_Click;
        }

        void ShowLaunchIniMenu(TreeViewEx selectedTree, int x, int y)
        {
            TreeNode firstNode = selectedTree?.SelectedNode;
            if (firstNode == null)
            {
                return;
            }

            _selectedTree = selectedTree;
            
            _addProcLaunchItem.Tag = firstNode;
            _updateDynamicLaunchItem.Tag = firstNode;
            _updateWaitForExeNLaunchItem.Tag = firstNode;
            _updateKillProcNLaunchItem.Tag = firstNode;


            switch (firstNode.Text)
            {
                case "WaitForEXEN":
                case "KillProcN":
                    {
                        _launchTreeContextMenu.Show(selectedTree, x, y);
                    }
                    break;
                default:
                    break;
            }
        }

        private void AddProcLaunchItem_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                TreeNode selectedNode = menuItem.Tag as TreeNode;
                if (selectedNode != null)
                {
                    var addForm = new AddProcForm();
                    if (addForm.ShowDialog() == DialogResult.OK)
                    {
                        if (selectedNode.Text == Model.LaunchINI.LaunchINI.KillProcN_Tag)
                        {
                            PortableApp.Inst.App.Launch.Launch.UpdateKillProcList(addForm.SelectedItems, true, selectedNode);
                            UpdateLaunchPropertyEditor(selectedNode.Text, selectedNode);
                        }
                        else if (selectedNode.Text == Model.LaunchINI.LaunchINI.WaitForEXEN_Tag)
                        {
                            PortableApp.Inst.App.Launch.Launch.UpdateWaitForExeList(addForm.SelectedItems, true, selectedNode);
                            UpdateLaunchPropertyEditor(selectedNode.Text, selectedNode);
                        }
                    }
                }
            }
        }

        private void UpdateWaitForExeNLaunchItem_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                TreeNode selectedNode = menuItem.Tag as TreeNode;
                if (selectedNode != null)
                {
                    PortableApp.Inst.App.Launch.Launch.UpdateWaitForExeList(Model.ExeFileNameListStringConverter.ExeFileNameList, true, selectedNode);

                }
            }
        }

        private void UpdateKillProcNLaunchItem_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                TreeNode selectedNode = menuItem.Tag as TreeNode;
                if (selectedNode != null)
                {
                    PortableApp.Inst.App.Launch.Launch.UpdateKillProcList(Model.ExeFileNameListStringConverter.ExeFileNameList, true, selectedNode);
                }
            }
        }

        private void UpdateDynamicLaunchItem_Click(object sender, EventArgs e)
        {
            UpdateFileDynamicIntelisense();
        }
    }
}
