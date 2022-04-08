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
        private void DeleteFoldersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FolderDelete();

            form.ShowDialog(this);
        }

        private void IgnoreFoldersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IgnoreListForm form = new IgnoreListForm();

            form.InitalizeExcludeFolders();

            form.ShowDialog(this);
        }

        private void IgnoreFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IgnoreListForm form = new IgnoreListForm();

            form.InitalizeExcludeFiles();

            form.ShowDialog(this);
        }

        private void IgnoreRegistryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IgnoreListForm form = new IgnoreListForm();

            form.InitalizeExcludeRegKeys();

            form.ShowDialog(this);
        }

        private void FixDirectoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderBrowserDlg.SelectedPath = PathManager.Init.GetLastPath(Settings.Default.ImportLastPath);
            if (folderBrowserDlg.ShowFolderBrowser(this) == DialogResult.OK)
            {
                string selectedSrcFolder = folderBrowserDlg.SelectedPath;
                string selectedDestFolder = folderBrowserDlg.SelectedPath + "_Fixed";

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

                this.BringToFront();

                ErrorLog.Inst.ShowInfo("File name conversion completed. \"{0}\"", selectedDestFolder);
            }
        }

        private void appVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new SearchReplaceHelperForm();

            form.Initialize("AppVIntellisense.txt");

            form.ShowDialog(this);
        }

        private void thinAppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new SearchReplaceHelperForm();

            form.Initialize("ThinAppIntellisense.txt");

            form.ShowDialog(this);
        }

        private void RegLaunchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var menuItem = sender as ToolStripMenuItem;
                if (sender != null)
                {
                    string cmdStr = menuItem.Tag as string;
                    System.Diagnostics.Process launchProc = new System.Diagnostics.Process();
                    launchProc.StartInfo.FileName = Utility.UserSettings.Inst.RegJump.Path;
                    launchProc.StartInfo.Verb = "runas";
                    launchProc.StartInfo.Arguments = string.Format("\"{0}\"", Environment.ExpandEnvironmentVariables(cmdStr));
                    launchProc.StartInfo.UseShellExecute = true;
                    launchProc.Start();
                }
            }
            catch (Exception)
            { }
        }

        private void FolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var menuItem = sender as ToolStripMenuItem;
                if (sender != null)
                {
                    string cmdStr = menuItem.Tag as string;
                    System.Diagnostics.Process launchProc = new System.Diagnostics.Process();
                    launchProc.StartInfo.FileName = Utility.UserSettings.Inst.FileManager.Path;
                    launchProc.StartInfo.Arguments = string.Format("\"{0}\"", Environment.ExpandEnvironmentVariables(cmdStr));
                    launchProc.StartInfo.UseShellExecute = true;
                    launchProc.Start();
                }
            }
            catch(Exception)
            { }
        }

        private void WhatChangedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process launchProc = new System.Diagnostics.Process();
                launchProc.StartInfo.FileName = Utility.UserSettings.Inst.WhatChanged.Path;
                launchProc.StartInfo.UseShellExecute = true;
                launchProc.StartInfo.Verb = "runas";
                launchProc.Start();
            }
            catch (Exception)
            { }
        }

        private void ResourceHackerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process launchProc = new System.Diagnostics.Process();
                launchProc.StartInfo.FileName = Utility.UserSettings.Inst.ResourceHacker.Path;
                if (Directory.Exists(Utility.UserSettings.Inst.PortableAppPath))
                {
                    var exeAppPath = string.Format("{0}\\App\\{1}", Utility.UserSettings.Inst.PortableAppPath, PortableApp.Inst.App.Launch.Launch.ProgramExecutable);
                    launchProc.StartInfo.Arguments = string.Format("\"{0}\"", exeAppPath);
                }
                launchProc.StartInfo.UseShellExecute = true;
                launchProc.Start();
            }
            catch (Exception)
            { }
        }

        private void ProcessMonitorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process launchProc = new System.Diagnostics.Process();
                launchProc.StartInfo.FileName = Utility.UserSettings.Inst.ProcMon.Path;
                launchProc.StartInfo.UseShellExecute = true;
                launchProc.StartInfo.Verb = "runas";
                launchProc.Start();
            }
            catch (Exception)
            { }
        }

        private void ProcessExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process launchProc = new System.Diagnostics.Process();
                launchProc.StartInfo.FileName = Utility.UserSettings.Inst.ProcExp.Path;
                launchProc.StartInfo.UseShellExecute = true;
                launchProc.StartInfo.Verb = "runas";
                launchProc.Start();
            }
            catch (Exception)
            { }
        }

        private void RegistryChangesViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process launchProc = new System.Diagnostics.Process();
                launchProc.StartInfo.FileName = Utility.UserSettings.Inst.RegistryChangesView.Path;
                launchProc.StartInfo.UseShellExecute = true;
                launchProc.StartInfo.Verb = "runas";
                launchProc.Start();
            }
            catch (Exception)
            { }
        }

        private void RegFromAppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process launchProc = new System.Diagnostics.Process();
                launchProc.StartInfo.FileName = Utility.UserSettings.Inst.RegFromApp.Path;
                launchProc.StartInfo.UseShellExecute = true;
                launchProc.StartInfo.Verb = "runas";
                launchProc.Start();
            }
            catch (Exception)
            { }
        }

        private void IconsExtractToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process launchProc = new System.Diagnostics.Process();
                launchProc.StartInfo.FileName = Utility.UserSettings.Inst.IconsExtract.Path;
                if (Directory.Exists(Utility.UserSettings.Inst.PortableAppPath))
                {
                    var exeAppPath = string.Format("{0}\\App\\*.*", Utility.UserSettings.Inst.PortableAppPath, PortableApp.Inst.App.Launch.Launch.ProgramExecutable);
                    launchProc.StartInfo.Arguments = string.Format("-scanpath \"{0}\"", exeAppPath);
                }
                launchProc.StartInfo.UseShellExecute = true;
                launchProc.StartInfo.Verb = "runas";
                launchProc.Start();
            }
            catch (Exception)
            { }
        }

        private void ExeInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process launchProc = new System.Diagnostics.Process();
                launchProc.StartInfo.FileName = Utility.UserSettings.Inst.ExeInfo.Path;
                if (Directory.Exists(Utility.UserSettings.Inst.PortableAppPath))
                {
                    var exeAppPath = string.Format("{0}\\App\\{1}", Utility.UserSettings.Inst.PortableAppPath, PortableApp.Inst.App.Launch.Launch.ProgramExecutable);
                    launchProc.StartInfo.Arguments = string.Format("\"{0}\"", exeAppPath);
                }
                launchProc.StartInfo.UseShellExecute = true;
                launchProc.StartInfo.Verb = "runas";
                launchProc.Start();
            }
            catch (Exception)
            { }
        }

        private void SmartSniffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process launchProc = new System.Diagnostics.Process();
                launchProc.StartInfo.FileName = Utility.UserSettings.Inst.SmartSniff.Path;
                launchProc.StartInfo.UseShellExecute = true;
                launchProc.StartInfo.Verb = "runas";
                launchProc.Start();
            }
            catch (Exception)
            { }
        }

        private void FileActivityWatchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process launchProc = new System.Diagnostics.Process();
                launchProc.StartInfo.FileName = Utility.UserSettings.Inst.FileActivityWatch.Path;
                launchProc.StartInfo.UseShellExecute = true;
                launchProc.StartInfo.Verb = "runas";
                launchProc.Start();
            }
            catch (Exception)
            { }
        }

        private void TcpviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process launchProc = new System.Diagnostics.Process();
                launchProc.StartInfo.FileName = Utility.UserSettings.Inst.Tcpview.Path;
                launchProc.StartInfo.UseShellExecute = true;
                launchProc.StartInfo.Verb = "runas";
                launchProc.Start();
            }
            catch (Exception)
            { }
        }

    }
}
