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
        

        public void ShowSourceMenu(TreeViewEx selectedTree, int x, int y)
        {
            _removeSourceItem.Enabled = false;
            _removeSourceItem.Tag = null;
           
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
                        bool showMenuFlag = false;
                        if (firstNode.Parent != null)
                        {
                            showMenuFlag = true;
                        }

                        if (showMenuFlag)
                        {
                            _removeSourceItem.Enabled = true;
                            _expandAllSourceItem.Enabled = true;
                            _expandAllSourceItem.Tag = firstNode;
                            _sourceContextMenu.Show(selectedTree, x, y);
                        }
                    }
                    break;
            }
        }

    }
}
