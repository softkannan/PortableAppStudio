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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppStudio
{
    public partial class Studio : Form
    {
        public Studio()
        {
            InitializeComponent();

            UpdateTreeContextMenus();

            ErrorLog.Inst.Initialize(this,statusLabelMain, statusLabelKeyPress);

            labelFileConvert.DragDrop += LabelFileConvert_DragDrop;
            labelFileConvert.DragEnter += LabelFileConvert_DragEnter;

            appInfoTree.AfterSelect += appInfoTree_AfterSelect;
            launchTree.AfterSelect += this.launchTree_AfterSelect;

            sourceFilesTree.NodeMouseClick += SourceTree_NodeMouseClick;
            sourceFilesTree.ItemDrag += SourceFilesTree_ItemDrag;

            sourceRegTree.NodeMouseClick += SourceTree_NodeMouseClick;
            sourceRegTree.ItemDrag += SourceRegTree_ItemDrag;
            sourceFilesTree.DragEnter += SourceFilesTree_DragEnter;
            sourceFilesTree.DragDrop += SourceFilesTree_DragDrop;

            appFilesTree.NodeMouseClick += AppFilesTree_NodeMouseClick;
            appFilesTree.DragDrop += AppFilesTree_DragDrop;
            appFilesTree.DragEnter += AppFilesTree_DragEnter;

            appRegTree.NodeMouseClick += appRegTree_NodeMouseClick;
            appRegTree.DragEnter += AppRegTree_DragEnter;
            appRegTree.DragDrop += AppRegTree_DragDrop;

            createToolStripMenuItem.Click += createToolStripMenuItem_Click;
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;

            importThinAppCaptureToolStripMenuItem.Click += importThinAppCaptureToolStripMenuItem_Click;
            importRegshotCaptureToolStripMenuItem.Click += importRegshotCaptureToolStripMenuItem_Click;
            importAppVCaptureToolStripMenuItem.Click += importAppVCaptureToolStripMenuItem_Click;
            importXRegshotCaptureToolStripMenuItem.Click += importXRegshotCaptureToolStripMenuItem_Click;
            importRegFileToolStripMenuItem.Click += importRegFileToolStripMenuItem_Click;
            importFolderToolStripMenuItem.Click += importFolderToolStripMenuItem_Click;

            fixDirectoriesToolStripMenuItem.Click += fixDirectoriesToolStripMenuItem_Click;
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;


            toolStripButtonCreate.Click += createToolStripMenuItem_Click;
            toolStripButtonOpen.Click += openToolStripMenuItem_Click;
            toolStripButtonSave.Click += saveToolStripMenuItem_Click;
            toolStripButtonClear.Click += toolStripButtonClear_Click;
            toolStripButtonRefresh.Click += toolStripButtonReferesh_Click;
            toolStripButtonImportAppv.Click += importAppVCaptureToolStripMenuItem_Click;
            toolStripButtonImportThinApp.Click += importThinAppCaptureToolStripMenuItem_Click;
            toolStripButtonImportX_RegShot.Click += importXRegshotCaptureToolStripMenuItem_Click;
            toolStripButtonImportRegShot.Click += importRegshotCaptureToolStripMenuItem_Click;
            toolStripButtonImportRegFile.Click += importRegFileToolStripMenuItem_Click;
            toolStripButtonImportFolder.Click += importFolderToolStripMenuItem_Click;
            deleteFoldersToolStripMenuItem.Click += DeleteFoldersToolStripMenuItem_Click;

            ignoreFilesToolStripMenuItem.Click += ignoreFilesToolStripMenuItem_Click;
            ignoreFoldersToolStripMenuItem.Click += ignoreFoldersToolStripMenuItem_Click;
            ignoreRegistryToolStripMenuItem.Click += ignoreRegistryToolStripMenuItem_Click;

            statusLabelKeyPress.Text = "";
            statusLabelMain.Text = "";

            DEBUG_OUTPUT_Combo.SelectedIndex = 1;

            PortableApp.Inst.Initailize(sourceFilesTree, sourceRegTree, appFilesTree, appRegTree, appInfoTree, launchTree);

            appInfoEditor.PropertyValueChanged += PortableApp.Inst.OnValueChanged;
            launchEditor.PropertyValueChanged += PortableApp.Inst.OnValueChanged;
        }
        
        private void DeleteFoldersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FolderDelete();

            form.ShowDialog(this);
        }

        private void importRegshotCaptureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDlg.RestoreDirectory = true;
            openFileDlg.InitialDirectory = PathManager.Init.GetLastPath(Settings.Default.ImportLastPath);
            openFileDlg.Filter = "RegShot Files|*.txt";
            if (openFileDlg.ShowDialog(this) == DialogResult.OK)
            {
                Settings.Default.ImportLastPath = Path.GetDirectoryName(openFileDlg.FileName);
                var regshotParser = new RegShotParser();
                ImportSourceFiles(regshotParser, openFileDlg.FileName);
            }
        }
        private void importXRegshotCaptureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderBrowserDlg.SelectedPath = PathManager.Init.GetLastPath(Settings.Default.ImportLastPath);
            if (folderBrowserDlg.ShowFolderBrowser(this) == DialogResult.OK)
            {
                Settings.Default.ImportLastPath = folderBrowserDlg.SelectedPath;
                var xRegShotParser = new X_RegshotParser();
                ImportSourceFiles(xRegShotParser, folderBrowserDlg.SelectedPath);
            }
        }

        private void importThinAppCaptureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderBrowserDlg.SelectedPath = PathManager.Init.GetLastPath(Settings.Default.ImportLastPath);
            if (folderBrowserDlg.ShowFolderBrowser(this) == DialogResult.OK)
            {
                Settings.Default.ImportLastPath = folderBrowserDlg.SelectedPath;
                var thinAppParser = new ThinAppParser();
                ImportSourceFiles(thinAppParser, folderBrowserDlg.SelectedPath);
            }
        }

        private void importAppVCaptureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderBrowserDlg.SelectedPath = PathManager.Init.GetLastPath(Settings.Default.ImportLastPath);
            if (folderBrowserDlg.ShowFolderBrowser(this) == DialogResult.OK)
            {
                Settings.Default.ImportLastPath = folderBrowserDlg.SelectedPath;
                var appvParser = new AppVParser();
                ImportSourceFiles(appvParser, folderBrowserDlg.SelectedPath);
            }
        }

        private void importRegFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDlg.RestoreDirectory = true;
            openFileDlg.InitialDirectory = PathManager.Init.GetLastPath(Settings.Default.ImportLastPath);
            openFileDlg.Filter = "RegShot Files|*.reg";
            if (openFileDlg.ShowDialog(this) == DialogResult.OK)
            {
                Settings.Default.ImportLastPath = Path.GetDirectoryName(openFileDlg.FileName);
                var regFileParser = new RegFileParser();
                ImportSourceFiles(regFileParser, openFileDlg.FileName);
            }
        }

        private void importFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderBrowserDlg.SelectedPath = PathManager.Init.GetLastPath(Settings.Default.ImportLastPath);
            if (folderBrowserDlg.ShowFolderBrowser(this) == DialogResult.OK)
            {
                Settings.Default.ImportLastPath = folderBrowserDlg.SelectedPath;
                var folderParser = new FolderParser();
                ImportSourceFiles(folderParser, folderBrowserDlg.SelectedPath);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderBrowserDlg.SelectedPath = PathManager.Init.GetLastPath(Settings.Default.PortableAppLastPath);
            if (folderBrowserDlg.ShowFolderBrowser(this) == DialogResult.OK)
            {
                Settings.Default.PortableAppLastPath = folderBrowserDlg.SelectedPath;
                LoadPortableApp(folderBrowserDlg.SelectedPath,true);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveInternal();
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new NewPortableAppForm();
            form.SelectedPath = Path.GetDirectoryName(PathManager.Init.GetLastPath(Settings.Default.PortableAppLastPath));
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                Settings.Default.PortableAppLastPath = form.SelectedPath;
                LoadPortableApp(form.SelectedPath,false);
            }
        }

        private void ignoreFoldersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IgnoreListForm form = new IgnoreListForm();

            form.InitalizeExcludeFolders();

            form.ShowDialog(this);
        }

        private void ignoreFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IgnoreListForm form = new IgnoreListForm();

            form.InitalizeExcludeFiles();

            form.ShowDialog(this);
        }

        private void ignoreRegistryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IgnoreListForm form = new IgnoreListForm();

            form.InitalizeExcludeRegKeys();

            form.ShowDialog(this);
        }
        private void Studio_Load(object sender, EventArgs e)
        {
            //RegShotParser parser = new RegShotParser();

            //parser.Parse(@"G:\csharp\PortableAppStudio\TestingData\LightRoomCapture\RegSnapshots\~res-x64.txt");

            //BuildTree(parser);

            //AppVParser parser = new AppVParser();

            //parser.Parse(@"G:\csharp\PortableAppStudio\TestingData\SampleApp\SampleApp_2_3");
            //parser.Parse(@"D:\Downloads\ExpressoRegEx\ExpressoRegEx");
            //parser.Parse(@"D:\Downloads\TestingData\SampleQuick\SampleQuick");

            //BuildTree(parser);

            //X_RegshotParser parser = new X_RegshotParser();

            //parser.Parse(@"D:\Downloads\Expresso\Expresso_Regshot");

            //BuildTree(parser);

            //ThinAppParser parser = new ThinAppParser();

            //parser.Parse(@"G:\csharp\PortableAppStudio\TestingData\LightRoomCapture\ThinInstall");

            //BuildTree(parser);


            //LoadPortableApp(@"G:\csharp\PortableAppStudio\TestingData\Application_Template_3.5.0");

        }

        private void Studio_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClosePortableApp();
            Settings.Default.Save();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void toolStripButtonClear_Click(object sender, EventArgs e)
        {
            sourceFilesTree.Reset();
            sourceRegTree.Reset();
        }

        private void toolStripButtonReferesh_Click(object sender, EventArgs e)
        {
            PortableApp.Inst.RefreshAppTrees(true);
        }

        private void LabelFileConvert_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void LabelFileConvert_DragDrop(object sender, DragEventArgs e)
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

        private void fixDirectoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderBrowserDlg.SelectedPath = PathManager.Init.GetLastPath(Settings.Default.ImportLastPath);
            if (folderBrowserDlg.ShowFolderBrowser(this) == DialogResult.OK)
            {
                string selectedSrcFolder = folderBrowserDlg.SelectedPath;
                string selectedDestFolder = folderBrowserDlg.SelectedPath + "_Fixed";

                if(Directory.Exists(selectedDestFolder))
                {
                    Directory.CreateDirectory(selectedDestFolder);
                }

                var progressBar = ProgressDialog.Run(this, "Fixing Directory and File Names ...", "Fixing Directory and File Names \"{0}\"", selectedDestFolder);

                try
                {

                    FileUtility.Inst.CopyAllFix(string.Format(@"\\?\{0}",selectedSrcFolder.Trim()), string.Format(@"\\?\{0}",selectedDestFolder.Trim()));

                }
                catch(Exception ex)
                {
                    ErrorLog.Inst.ShowError("Failed to fix directory names : {0}", ex.Message);
                }

                Settings.Default.ImportLastPath = selectedDestFolder;

                progressBar.ShutDown();

                this.BringToFront();

                ErrorLog.Inst.ShowInfo("File name conversion completed. \"{0}\"", selectedDestFolder);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm form = new AboutForm();

            form.ShowDialog(this);
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
    }
}
