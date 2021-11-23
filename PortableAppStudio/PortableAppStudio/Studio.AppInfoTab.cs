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
        private void AppInfoTree_AfterSelect(object sender, TreeViewEventArgs e)
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
                    appInfoEditor.SelectedObjects(retListValue, showNode.Text, selectedProprtyName);
                }
                else if (retObjValue != null)
                {
                    appInfoEditor.SelectedObject(retObjValue, showNode.Text, selectedProprtyName);
                }
            }
        }

    }
}
