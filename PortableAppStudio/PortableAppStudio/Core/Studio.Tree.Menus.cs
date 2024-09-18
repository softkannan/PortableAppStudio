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

        public TreeViewEx _selectedTree { get; set; }
        public Control EditControlParent { get; set; }

        private void UpdateTreeContextMenus()
        {
            UpdateFileTreeContextMenus();

            UpdateLaunchTreeContextMenus();

            UpdateRegTreeContextMenus();
        }

        private void ExpandNodes_Click(object sender, EventArgs e)
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
    }
}
