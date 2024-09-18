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
using System.Diagnostics;
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
        private void ImportFilesClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var clipTxt = Clipboard.GetText();
            List<string> clipListData = null;

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
                        foreach (var item in fileDropData)
                        {
                            clipListData.Add(item);
                        }
                    }
                }
            }

            if (clipListData != null && clipListData.Count > 0)
            {
                for (int index = 0; index < clipListData.Count; index++)
                {
                    if (Directory.Exists(clipListData[index]))
                    {
                        var folderParser = new FolderParser();
                        ImportSourceFolder(folderParser, clipListData[index]);
                    }
                }
            }
            else
            {
                ErrorLog.Inst.ShowError("Clipboad data not supported");
            }
        }

        private void ImportRegistryCliboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportRegClipboard();
        }

        private void ImportRegClipboard()
        {
            var clipTxt = Clipboard.GetText();
            List<string> clipListData = null;

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
                        foreach (var item in fileDropData)
                        {
                            clipListData.Add(item);
                        }
                    }
                }
            }

            if (clipListData != null && clipListData.Count > 0)
            {
                for (int index = 0; index < clipListData.Count; index++)
                {
                    string tempRegFile = string.Format("{0}RegKey_ClipImport.reg", Path.GetTempPath());
                    string exeFullPath = FileUtility.Inst.GetSearchPath("reg.exe");
                    string args = string.Format("export \"{0}\" \"{1}\" /y", clipListData[index], tempRegFile);
                    try
                    {
                        using (Process proc = new Process())
                        {
                            proc.StartInfo.FileName = exeFullPath;
                            proc.StartInfo.UseShellExecute = false;
                            proc.StartInfo.RedirectStandardOutput = true;
                            proc.StartInfo.RedirectStandardError = true;
                            proc.StartInfo.CreateNoWindow = true;
                            proc.StartInfo.Arguments = args;
                            proc.Start();
                            string stdout = proc.StandardOutput.ReadToEnd();
                            string stderr = proc.StandardError.ReadToEnd();
                            proc.WaitForExit();
                        }

                        var regFileParser = new RegFileParser();
                        ImportSourceFolder(regFileParser, tempRegFile);
                    }
                    catch (Exception ex)
                    {
                        ErrorLog.Inst.ShowError("Clipboad data not supported : {0}", ex.Message);
                    }
                }
            }
            else
            {
                ErrorLog.Inst.ShowError("Clipboad data not supported");
            }
        }

        private void ImportRegshotCaptureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDlg.RestoreDirectory = true;
            openFileDlg.InitialDirectory = PathManager.Init.GetLastPath(Settings.Default.ImportLastPath);
            openFileDlg.Filter = "RegShot Files|*.txt";
            if (openFileDlg.ShowDialog(this) == DialogResult.OK)
            {
                Settings.Default.ImportLastPath = Path.GetDirectoryName(openFileDlg.FileName);
                var regshotParser = new RegShotParser();
                ImportSourceFolder(regshotParser, openFileDlg.FileName);
            }
        }
        private void ImportXRegshotCaptureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderBrowserDlg.SelectedPath = PathManager.Init.GetLastPath(Settings.Default.ImportLastPath);
            if (folderBrowserDlg.ShowFolderBrowser(this) == DialogResult.OK)
            {
                Settings.Default.ImportLastPath = folderBrowserDlg.SelectedPath;
                var xRegShotParser = new X_RegshotParser();
                ImportSourceFolder(xRegShotParser, folderBrowserDlg.SelectedPath);
            }
        }

        private void ImportThinAppCaptureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderBrowserDlg.SelectedPath = PathManager.Init.GetLastPath(Settings.Default.ImportLastPath);
            if (folderBrowserDlg.ShowFolderBrowser(this) == DialogResult.OK)
            {
                Settings.Default.ImportLastPath = folderBrowserDlg.SelectedPath;
                var thinAppParser = new ThinAppParser();
                ImportSourceFolder(thinAppParser, folderBrowserDlg.SelectedPath);
            }
        }

        private void ImportAppVCaptureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderBrowserDlg.SelectedPath = PathManager.Init.GetLastPath(Settings.Default.ImportLastPath);
            if (folderBrowserDlg.ShowFolderBrowser(this) == DialogResult.OK)
            {
                Settings.Default.ImportLastPath = folderBrowserDlg.SelectedPath;
                var appvParser = new AppVParser();
                ImportSourceFolder(appvParser, folderBrowserDlg.SelectedPath);
            }
        }

        private void ImportRegFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDlg.RestoreDirectory = true;
            openFileDlg.InitialDirectory = PathManager.Init.GetLastPath(Settings.Default.ImportLastPath);
            openFileDlg.Filter = "RegShot Files|*.reg";
            if (openFileDlg.ShowDialog(this) == DialogResult.OK)
            {
                Settings.Default.ImportLastPath = Path.GetDirectoryName(openFileDlg.FileName);
                var regFileParser = new RegFileParser();
                ImportSourceFolder(regFileParser, openFileDlg.FileName);
            }
        }

        private void ImportFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderBrowserDlg.SelectedPath = PathManager.Init.GetLastPath(Settings.Default.ImportLastPath);
            if (folderBrowserDlg.ShowFolderBrowser(this) == DialogResult.OK)
            {
                Settings.Default.ImportLastPath = folderBrowserDlg.SelectedPath;
                var folderParser = new FolderParser();
                ImportSourceFolder(folderParser, folderBrowserDlg.SelectedPath);
            }
        }

        private void ImportSourceFolder(ParserBase importParser, string fileOrFolderName)
        {
            ErrorLog.Inst.WriteStatus("Importing Source Files...");
            var progressBar = ProgressDialog.Run(this, "Importing Source Files and Registry Entries ...",
                    "Importing Source Files and Registry Entries From \"{0}\"", fileOrFolderName);
            try
            {
                importParser.Parse(string.Format(@"\\?\{0}", fileOrFolderName));

                this.SuspendLayout();

                importParser.PopulateSourceTreeView(sourceFilesTree, sourceRegTree);

                this.ResumeLayout();

            }
            catch (Exception ex)
            {
                ErrorLog.Inst.ShowError("Source Files Import Error : {0}", ex.Message);
            }
            finally
            {
                progressBar.ShutDown();
            }
            ErrorLog.Inst.WriteStatus("Source Files and Registry Entries Import Completed");
        }
    }
}
