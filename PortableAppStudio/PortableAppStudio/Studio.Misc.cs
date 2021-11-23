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
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClearToolStripButton_Click(object sender, EventArgs e)
        {
            ClipBoardExInfo.Inst.Tag = null;
            sourceFilesTree.Reset();
            sourceRegTree.Reset();
        }

        private void RefreshToolStripButton_Click(object sender, EventArgs e)
        {
            ClipBoardExInfo.Inst.Tag = null;
            PortableApp.Inst.RefreshAppTrees(true);
        }

        private void FileConvertLabel_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm form = new AboutForm();

            form.ShowDialog(this);
        }

        private void FileConvertLabel_DragDrop(object sender, DragEventArgs e)
        {
            var fileData = e.Data.GetData("FileDrop") as string[];
            if (fileData != null)
            {
                var folderParser = new FolderParser();
                foreach (var item in fileData)
                {
                    if (Directory.Exists(item))
                    {
                        string selectedSrcFolder = item;
                        string selectedDestFolder = item + "_Fixed";

                        if (Directory.Exists(selectedDestFolder))
                        {
                            Directory.CreateDirectory(selectedDestFolder);
                        }

                        var progressBar = ProgressDialog.Run(this, "Fixing Directory and File Names ...", "Fixing Directory and File Names \"{0}\"", selectedDestFolder);

                        try
                        {

                            FileUtility.Inst.CopyAllFix(string.Format(@"\\?\{0}", selectedSrcFolder.Trim()), string.Format(@"\\?\{0}", selectedDestFolder.Trim()));

                        }
                        catch (Exception ex)
                        {
                            ErrorLog.Inst.ShowError("Failed to fix directory names : {0}", ex.Message);
                        }

                        Settings.Default.ImportLastPath = selectedDestFolder;

                        progressBar.ShutDown();

                        ErrorLog.Inst.ShowInfo("File name conversion completed. \"{0}\"", selectedDestFolder);
                    }
                }
            }
        }

    }
}
