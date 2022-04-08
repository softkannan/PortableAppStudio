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
    public partial class MainStudio : Form
    {
        public MainStudio()
        {
            InitializeComponent();

            var lastFileNode = sourceFilesTree.Nodes.Add("%APPDATA%\\", "%APPDATA%");
            sourceFilesTree.KeyNodes.Add("%APPDATA%\\", lastFileNode);

            var lastRegNode = sourceRegTree.Nodes.Add("HKCU\\", "HKCU");
            sourceRegTree.KeyNodes.Add("HKCU\\", lastRegNode);

            lastRegNode = sourceRegTree.Nodes.Add("HKLM\\", "HKLM");
            sourceRegTree.KeyNodes.Add("HKLM\\", lastRegNode);

            Utility.UserSettings.LoadSettings();

            UpdateTreeContextMenus();

            ErrorLog.Inst.Initialize(this, statusLabelMain, statusLabelKeyPress);

            fileConvertLabel.DragDrop += FileConvertLabel_DragDrop;
            fileConvertLabel.DragEnter += FileConvertLabel_DragEnter;

            #region Tree Items

            appInfoTree.AfterSelect += AppInfoTree_AfterSelect;
            launchTree.AfterSelect += LaunchTree_AfterSelect;
            launchTree.NodeMouseClick += LaunchTree_NodeMouseClick;

            sourceFilesTree.NodeMouseClick += SourceTreeFile_NodeMouseClick;
            sourceFilesTree.ItemDrag += SourceFilesTree_ItemDrag;
            sourceFilesTree.DragEnter += SourceFilesTree_DragEnter;
            sourceFilesTree.DragDrop += SourceFilesTree_DragDrop;

            sourceRegTree.NodeMouseClick += SourceTreeReg_NodeMouseClick;
            sourceRegTree.ItemDrag += SourceRegTree_ItemDrag;

            appFilesTree.NodeMouseClick += AppFilesTree_NodeMouseClick;
            appFilesTree.DragDrop += AppFilesTree_DragDrop;
            appFilesTree.DragEnter += AppFilesTree_DragEnter;

            appRegTree.NodeMouseClick += AppRegTree_NodeMouseClick;
            appRegTree.DragEnter += AppRegTree_DragEnter;
            appRegTree.DragDrop += AppRegTree_DragDrop;

            #endregion

            #region Menu Bar Items

            createToolStripMenuItem.Click += CreateToolStripMenuItem_Click;
            openToolStripMenuItem.Click += OpenToolStripMenuItem_Click;
            saveToolStripMenuItem.Click += SaveToolStripMenuItem_Click;
            exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;

            importThinAppCaptureToolStripMenuItem.Click += ImportThinAppCaptureToolStripMenuItem_Click;
            importRegshotCaptureToolStripMenuItem.Click += ImportRegshotCaptureToolStripMenuItem_Click;
            importAppVCaptureToolStripMenuItem.Click += ImportAppVCaptureToolStripMenuItem_Click;
            importXRegshotCaptureToolStripMenuItem.Click += ImportXRegshotCaptureToolStripMenuItem_Click;
            importRegFileToolStripMenuItem.Click += ImportRegFileToolStripMenuItem_Click;
            importFolderToolStripMenuItem.Click += ImportFolderToolStripMenuItem_Click;
            importRegistryCliboardToolStripMenuItem.Click += ImportRegistryCliboardToolStripMenuItem_Click;
            importFilesClipboardToolStripMenuItem.Click += ImportFilesClipboardToolStripMenuItem_Click;

            deleteFoldersToolStripMenuItem.Click += DeleteFoldersToolStripMenuItem_Click;

            ignoreFilesToolStripMenuItem.Click += IgnoreFilesToolStripMenuItem_Click;
            ignoreFoldersToolStripMenuItem.Click += IgnoreFoldersToolStripMenuItem_Click;
            ignoreRegistryToolStripMenuItem.Click += IgnoreRegistryToolStripMenuItem_Click;

            #endregion

            #region Toolbar Buttons

            importThinAppToolStripButtonDropMenuItem.Click += ImportThinAppCaptureToolStripMenuItem_Click;
            importXRegshotToolStripButtonDropMenuItem.Click += ImportXRegshotCaptureToolStripMenuItem_Click;
            importRegshotToolStripButtonDropMenuItem.Click += ImportRegshotCaptureToolStripMenuItem_Click;
            importRegFileToolStripButtonDropMenuItem.Click += ImportRegFileToolStripMenuItem_Click;
            importAppVToolStripButton.Click += ImportAppVCaptureToolStripMenuItem_Click;
            importFolderToolStripButton.Click += ImportFolderToolStripMenuItem_Click;
            importRegClipboardToolStripMenuItem.Click += ImportRegistryCliboardToolStripMenuItem_Click;

            fixDirectoriesToolStripMenuItem.Click += FixDirectoriesToolStripMenuItem_Click;
            aboutToolStripMenuItem.Click += AboutToolStripMenuItem_Click;

            createToolStripButton.Click += CreateToolStripMenuItem_Click;
            openToolStripButton.Click += OpenToolStripMenuItem_Click;
            saveToolStripButton.Click += SaveToolStripMenuItem_Click;

            clearToolStripButton.Click += ClearToolStripButton_Click;
            refreshToolStripButton.Click += RefreshToolStripButton_Click;

            createLauncherToolStripButton.Click += CreateLauncherToolStripButton_Click;
            runPortableToolStripButton.ButtonClick += RunPortableToolStripButton_Click;
            runWithProcMonToolStripMenuItem.Click += RunWithProcMonToolStripMenuItem_Click;
            runWithRegMonToolStripMenuItem.Click += RunWithRegMonToolStripMenuItem_Click;
            runWithTracerToolStripMenuItem.Click += RunWithTracerToolStripMenuItem_Click;
            mergeCustomNSIToolStripButton.ButtonClick += MergeCustomNSIToolStripButton_Click;
            compareToolStripMenuItem.Click += MergeCustomNSIToolStripButton_Click;
            forceMergeToolStripMenuItem.Click += ForceMergeToolStripMenuItem_Click;
            viewDebugLogToolStripButton.Click += ViewDebugLogToolStripButton_Click;
            editToolStripButton.Click += EditToolStripButton_Click;

            mergeListtoolStripComboBox.Items.Add("Custom.nsh");
            mergeListtoolStripComboBox.Items.Add("Launch.ini");
            mergeListtoolStripComboBox.Items.Add("AppInfo Folder");
            mergeListtoolStripComboBox.SelectedIndex = 0;

            regFromAppToolStripMenuItem.Click += RegFromAppToolStripMenuItem_Click;
            registryChangesViewToolStripMenuItem.Click += RegistryChangesViewToolStripMenuItem_Click;
            processExplorerToolStripMenuItem.Click += ProcessExplorerToolStripMenuItem_Click;
            processMonitorToolStripMenuItem.Click += ProcessMonitorToolStripMenuItem_Click;
            resourceHackerToolStripMenuItem.Click += ResourceHackerToolStripMenuItem_Click;
            whatChangedToolStripMenuItem.Click += WhatChangedToolStripMenuItem_Click;
            tcpviewToolStripMenuItem.Click += TcpviewToolStripMenuItem_Click;
            fileActivityWatchToolStripMenuItem.Click += FileActivityWatchToolStripMenuItem_Click;
            smartSniffToolStripMenuItem.Click += SmartSniffToolStripMenuItem_Click;
            exeInfoToolStripMenuItem.Click += ExeInfoToolStripMenuItem_Click;
            iconsExtractToolStripMenuItem.Click += IconsExtractToolStripMenuItem_Click;

            foreach (var item in PathManager.Init.RegistryQuickLaunch)
            {
                var toolItem = quickLaunchRegToolStripDropDownButton.DropDownItems.Add(item.Key);
                toolItem.Tag = item.Value;
                toolItem.Click += RegLaunchToolStripMenuItem_Click;
            }

            foreach (var item in PathManager.Init.FileQuickLaunch)
            {
                var toolItem = foldersToolStripSplitButton.DropDownItems.Add(item.Key);
                toolItem.Tag = item.Value;
                toolItem.Click += FolderToolStripMenuItem_Click;
            }

            crossPlatformTemplateToolStripMenuItem.Click += CrossPlatformTemplateToolStripMenuItem_Click;
            opensourceTemplateToolStripMenuItem.Click += OpensourceTemplateToolStripMenuItem_Click;
            qTFrameworkTemplateToolStripMenuItem.Click += QTFrameworkTemplateToolStripMenuItem_Click;
            pythonApplicationTemplateToolStripMenuItem.Click += PythonApplicationTemplateToolStripMenuItem_Click;
            jAVAApplicationTemplateToolStripMenuItem.Click += JAVAApplicationTemplateToolStripMenuItem_Click;

            #endregion

            statusLabelKeyPress.Text = "";
            statusLabelMain.Text = "";

            DEBUG_OUTPUT_Combo.SelectedIndex = 1;

            PortableApp.Inst.Initailize(sourceFilesTree, sourceRegTree, appFilesTree, appRegTree, appInfoTree, launchTree);

            appInfoEditor.PropertyValueChanged += PortableApp.Inst.OnValueChanged;
            launchEditor.PropertyValueChanged += PortableApp.Inst.OnValueChanged;
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

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderBrowserDlg.SelectedPath = PathManager.Init.GetLastPath(Settings.Default.PortableAppLastPath);
            if (folderBrowserDlg.ShowFolderBrowser(this) == DialogResult.OK)
            {
                var appFolder = string.Format("{0}\\App", folderBrowserDlg.SelectedPath);
                if(!Directory.Exists(appFolder))
                {
                    ErrorLog.Inst.ShowError("Invalid Portable Application folder : {0}", folderBrowserDlg.SelectedPath);
                    return;
                }
                Settings.Default.PortableAppLastPath = folderBrowserDlg.SelectedPath;
                LoadPortableApp(folderBrowserDlg.SelectedPath, true);
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!PortableApp.Inst.IsAlreadyOpen)
            {
                MessageBox.Show("Open Portable App before saving", "Info", MessageBoxButtons.OK);
                return;
            }
            SaveInternal();
        }

        private void CreateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new NewPortableAppForm
            {
                SelectedPath = Path.GetDirectoryName(PathManager.Init.GetLastPath(Settings.Default.PortableAppLastPath)),
                ImportFolderName = Settings.Default.ImportLastPath
            };
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                Settings.Default.PortableAppLastPath = form.SelectedPath;
                LoadPortableApp(form.SelectedPath, false);
            }
        }

        private void CreateLauncherToolStripButton_Click(object sender, EventArgs e)
        {
            GeneratePortableLauncher();
        }

        private void RunPortableToolStripButton_Click(object sender, EventArgs e)
        {
            if(UserSettings.Inst.IsPortableAppRunning)
            {
                ErrorLog.Inst.ShowError("Portable App already running");
                return;
            }
            Task.Factory.StartNew(() =>
            { 
                try
                {
                    UserSettings.Inst.IsPortableAppRunning = true;
                    var foundFiles = Directory.GetFiles(Utility.UserSettings.Inst.PortableAppPath, "*.exe", SearchOption.TopDirectoryOnly);
                    if (foundFiles != null && foundFiles.Length > 0)
                    {
                        System.Diagnostics.Process launchProc = new System.Diagnostics.Process();
                        launchProc.StartInfo.FileName = foundFiles.FirstOrDefault();
                        launchProc.StartInfo.UseShellExecute = true;
                        launchProc.Start();
                        launchProc.WaitForExit();
                    }
                }
                catch (Exception)
                {
                }
                finally
                {
                    UserSettings.Inst.IsPortableAppRunning = false;
                }
            });
        }

        private void RunWithTracerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UserSettings.Inst.IsPortableAppRunning)
            {
                ErrorLog.Inst.ShowError("Portable App already running");
                return;
            }
            try
            {
                var foundFiles = Directory.GetFiles(Utility.UserSettings.Inst.PortableAppPath, "*.exe", SearchOption.TopDirectoryOnly);
                if (foundFiles != null && foundFiles.Length > 0)
                {
                    {
                        System.Diagnostics.Process launchProc = new System.Diagnostics.Process();
                        launchProc.StartInfo.FileName = string.Format("{0}\\x64\\RegistryChangesView.exe", PathManager.Init.GetResourcePath("3rdPartyApps"));
                        launchProc.StartInfo.Verb = "runas";
                        launchProc.StartInfo.UseShellExecute = true;
                        launchProc.Start();
                    }
                    {
                        System.Diagnostics.Process launchProc = new System.Diagnostics.Process();
                        launchProc.StartInfo.FileName = string.Format("{0}\\x64\\FileActivityWatch.exe", PathManager.Init.GetResourcePath("3rdPartyApps"));
                        launchProc.StartInfo.Verb = "runas";
                        launchProc.StartInfo.UseShellExecute = true;
                        launchProc.Start();
                    }
                    {
                        MessageBox.Show("Before Click Ok make sure file and registry monitor is running", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        System.Diagnostics.Process launchProc = new System.Diagnostics.Process();
                        launchProc.StartInfo.FileName = foundFiles.FirstOrDefault();
                        launchProc.StartInfo.UseShellExecute = true;
                        launchProc.Start();
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void RunWithRegMonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UserSettings.Inst.IsPortableAppRunning)
            {
                ErrorLog.Inst.ShowError("Portable App already running");
                return;
            }
            try
            {
                var foundFiles = Directory.GetFiles(Utility.UserSettings.Inst.PortableAppPath, "*.exe", SearchOption.TopDirectoryOnly);
                if (foundFiles != null && foundFiles.Length > 0)
                {
                    System.Diagnostics.Process launchProc = new System.Diagnostics.Process();
                    launchProc.StartInfo.Arguments = string.Format("/StartImmediately 0 /RunProcess \"{0}\"", foundFiles.FirstOrDefault());
                    launchProc.StartInfo.FileName = string.Format("{0}\\x86\\RegFromApp.exe", PathManager.Init.GetResourcePath("3rdPartyApps"));
                    launchProc.StartInfo.Verb = "runas";
                    launchProc.StartInfo.UseShellExecute = true;

                    launchProc.Start();
                }
            }
            catch (Exception)
            { }
        }

        private void RunWithProcMonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (UserSettings.Inst.IsPortableAppRunning)
            {
                ErrorLog.Inst.ShowError("Portable App already running");
                return;
            }
            try
            {
                var foundFiles = Directory.GetFiles(Utility.UserSettings.Inst.PortableAppPath, "*.exe", SearchOption.TopDirectoryOnly);
                if (foundFiles != null && foundFiles.Length > 0)
                {
                    string exeFileName = Path.GetFileName(foundFiles.FirstOrDefault());
                    {
                        Clipboard.SetText(exeFileName);
                        System.Diagnostics.Process launchProc = new System.Diagnostics.Process();
                        launchProc.StartInfo.FileName = Utility.UserSettings.Inst.ProcMon.Path;
                        launchProc.StartInfo.Arguments = string.Format("/LoadConfig \"{0}\\ProcMonConfig.pmc\"", PathManager.Init.GetResourcePath("Other"));
                        launchProc.StartInfo.UseShellExecute = true;
                        launchProc.Start();
                    }
                    {
                        MessageBox.Show("Before Click Ok make sure procmon is running", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        System.Diagnostics.Process launchProc = new System.Diagnostics.Process();
                        launchProc.StartInfo.FileName = foundFiles.FirstOrDefault();
                        launchProc.StartInfo.UseShellExecute = true;
                        launchProc.Start();
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void ViewDebugLogToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process launchProc = new System.Diagnostics.Process();

                launchProc.StartInfo.FileName = Utility.UserSettings.Inst.NotepadPath.Path;
                launchProc.StartInfo.Arguments = string.Format("\"{0}\\Data\\debug.log\"", Utility.UserSettings.Inst.PortableAppPath);
                launchProc.StartInfo.UseShellExecute = true;

                launchProc.Start();
            }
            catch(Exception)
            { }
        }

        private void ForceMergeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MergeFile(mergeListtoolStripComboBox.SelectedItem as string, true);
        }

        private void MergeCustomNSIToolStripButton_Click(object sender, EventArgs e)
        {
            MergeFile(mergeListtoolStripComboBox.SelectedItem as string, false);
        }

        private void MergeFile(string action, bool forceMerge)
        {
            string compareItem1 = string.Empty;
            string compareItem2 = string.Empty;
            try
            {
                //Custom.nsh
                //Launch.ini
                //AppInfo Folder
                if (string.Equals(action, "Custom.nsh", StringComparison.OrdinalIgnoreCase))
                {
                    var resourcePortableApp = PathManager.Init.GetResourcePath("PortableApp");
                    compareItem1 = string.Format("{0}\\App\\AppInfo\\Launcher\\Custom.nsh", resourcePortableApp);
                    compareItem2 = string.Format("{0}\\App\\AppInfo\\Launcher\\Custom.nsh", Utility.UserSettings.Inst.PortableAppPath);

                    if(forceMerge && 
                        MessageBox.Show(string.Format("Do you want to overwrite the \"{0}\" file.",compareItem2),"Warning",MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        File.Copy(compareItem1, compareItem2, true);
                    }
                }
                else if (string.Equals(action, "Launch.ini", StringComparison.OrdinalIgnoreCase))
                {
                    var launchIniPath = Directory.GetFiles(string.Format("{0}\\App\\AppInfo\\Launcher", Utility.UserSettings.Inst.PortableAppPath), "*.ini", SearchOption.TopDirectoryOnly);
                    if (launchIniPath != null && launchIniPath.Length > 0)
                    {
                        compareItem2 = launchIniPath.FirstOrDefault();

                        using (var opnDlg = new OpenFileDialog())
                        {
                            opnDlg.ValidateNames = false;
                            opnDlg.CheckFileExists = true;
                            opnDlg.CheckPathExists = true;
                            opnDlg.Multiselect = false;
                            opnDlg.Filter = "Launch INI (*.ini)|*.ini";
                            if (opnDlg.ShowDialog() == DialogResult.OK)
                            {
                                compareItem1 = opnDlg.FileName;
                            }
                        }

                        if (forceMerge &&
                        MessageBox.Show(string.Format("Do you want to overwrite the \"{0}\" file.", compareItem2), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            File.Copy(compareItem1, compareItem2, true);
                        }
                    }
                }
                else if (string.Equals(action, "AppInfo Folder", StringComparison.OrdinalIgnoreCase))
                {
                    compareItem1 = Utility.UserSettings.Inst.PortableAppPath;
                    using (var fbd = new FolderBrowserDialog())
                    {
                        if (fbd.ShowDialog() == DialogResult.OK)
                        {
                            compareItem2 = fbd.SelectedPath;
                        }
                    }
                }

                if ((!string.IsNullOrWhiteSpace(compareItem1)) && (!string.IsNullOrWhiteSpace(compareItem2)))
                {
                    System.Diagnostics.Process launchProc = new System.Diagnostics.Process();
                    launchProc.StartInfo.FileName = Utility.UserSettings.Inst.DiffToolPath.Path;
                    launchProc.StartInfo.Arguments = string.Format("\"{0}\" \"{1}\"", compareItem1, compareItem2);
                    launchProc.StartInfo.UseShellExecute = true;
                    launchProc.Start();
                }
            }
            catch (Exception)
            { }
        }

        private void EditToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.Equals(mergeListtoolStripComboBox.SelectedItem as string, "Custom.nsh", StringComparison.OrdinalIgnoreCase))
                {
                    var customNSHPath = string.Format("{0}\\App\\AppInfo\\Launcher\\Custom.nsh", Utility.UserSettings.Inst.PortableAppPath);
                    System.Diagnostics.Process launchProc = new System.Diagnostics.Process();
                    launchProc.StartInfo.FileName = Utility.UserSettings.Inst.NSISEditorPath.Path;
                    launchProc.StartInfo.Arguments = string.Format("\"{0}\"", customNSHPath);
                    launchProc.StartInfo.UseShellExecute = true;
                    launchProc.Start();
                }
                else if (string.Equals(mergeListtoolStripComboBox.SelectedItem as string, "Launch.ini", StringComparison.OrdinalIgnoreCase))
                {
                    var launchIniPath = Directory.GetFiles(string.Format("{0}\\App\\AppInfo\\Launcher", Utility.UserSettings.Inst.PortableAppPath), "*.ini", SearchOption.TopDirectoryOnly);
                    if (launchIniPath != null && launchIniPath.Length > 0)
                    {
                        var customNSHPath = launchIniPath.FirstOrDefault();
                        System.Diagnostics.Process launchProc = new System.Diagnostics.Process();
                        launchProc.StartInfo.FileName = Utility.UserSettings.Inst.NotepadPath.Path;
                        launchProc.StartInfo.Arguments = string.Format("\"{0}\"", customNSHPath);
                        launchProc.StartInfo.UseShellExecute = true;
                        launchProc.Start();
                    }
                }
                else if (string.Equals(mergeListtoolStripComboBox.SelectedItem as string, "AppInfo Folder", StringComparison.OrdinalIgnoreCase))
                {
                    System.Diagnostics.Process launchProc = new System.Diagnostics.Process();
                    launchProc.StartInfo.FileName = Utility.UserSettings.Inst.FileManager.Path;
                    launchProc.StartInfo.Arguments = string.Format("\"{0}\"", Utility.UserSettings.Inst.PortableAppPath);
                    launchProc.StartInfo.UseShellExecute = true;
                    launchProc.Start();
                }
            }
            catch(Exception)
            { }
        }

        
    }
}
