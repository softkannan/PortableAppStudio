namespace PortableAppStudio
{
    partial class Studio
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("DEBUG_SEGMENT_[Launch]");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Studio));
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importThinAppCaptureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importRegshotCaptureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importAppVCaptureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importXRegshotCaptureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importRegFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fixDirectoriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteFoldersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ignoreListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ignoreFoldersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ignoreFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ignoreRegistryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainTab = new System.Windows.Forms.TabControl();
            this.filesPage = new System.Windows.Forms.TabPage();
            this.filesTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.sourceFilesTree = new PortableAppStudio.Controls.TreeViewEx();
            this.appFilesTree = new PortableAppStudio.Controls.TreeViewEx();
            this.registryTab = new System.Windows.Forms.TabPage();
            this.registryTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.appRegTree = new PortableAppStudio.Controls.TreeViewEx();
            this.sourceRegTree = new PortableAppStudio.Controls.TreeViewEx();
            this.appInfoPage = new System.Windows.Forms.TabPage();
            this.appInfoEditor = new PortableAppStudio.Controls.PropertyEditorUserControl();
            this.appInfoTree = new PortableAppStudio.Controls.TreeViewEx();
            this.launchPage = new System.Windows.Forms.TabPage();
            this.launchEditor = new PortableAppStudio.Controls.PropertyEditorUserControl();
            this.launchTree = new PortableAppStudio.Controls.TreeViewEx();
            this.optionsPage = new System.Windows.Forms.TabPage();
            this.groupBoxFileConvert = new System.Windows.Forms.GroupBox();
            this.registryGroupBox = new System.Windows.Forms.GroupBox();
            this.generateManifestChk = new System.Windows.Forms.CheckBox();
            this.generateRegFilesCheck = new System.Windows.Forms.CheckBox();
            this.debugGroupBox = new System.Windows.Forms.GroupBox();
            this.DEBUG_SEGMENT_List = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.DEBUG_GLOBAL_Check = new System.Windows.Forms.CheckBox();
            this.DEBUG_OUTPUT_Combo = new System.Windows.Forms.ComboBox();
            this.DEBUG_SEGWRAP_Check = new System.Windows.Forms.CheckBox();
            this.DEBUG_ALL_Check = new System.Windows.Forms.CheckBox();
            this.openFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDlg = new System.Windows.Forms.FolderBrowserDialog();
            this.saveFileDlg = new System.Windows.Forms.SaveFileDialog();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonCreate = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonImportAppv = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonImportThinApp = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonImportX_RegShot = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonImportRegShot = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonImportRegFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonImportFolder = new System.Windows.Forms.ToolStripButton();
            this.mainStatus = new System.Windows.Forms.StatusStrip();
            this.statusLabelMain = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelKeyPress = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelFileConvert = new System.Windows.Forms.Label();
            this.mainMenu.SuspendLayout();
            this.mainTab.SuspendLayout();
            this.filesPage.SuspendLayout();
            this.filesTableLayoutPanel.SuspendLayout();
            this.registryTab.SuspendLayout();
            this.registryTableLayoutPanel.SuspendLayout();
            this.appInfoPage.SuspendLayout();
            this.launchPage.SuspendLayout();
            this.optionsPage.SuspendLayout();
            this.groupBoxFileConvert.SuspendLayout();
            this.registryGroupBox.SuspendLayout();
            this.debugGroupBox.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.mainStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.importStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(1230, 24);
            this.mainMenu.TabIndex = 0;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // createToolStripMenuItem
            // 
            this.createToolStripMenuItem.Name = "createToolStripMenuItem";
            this.createToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.createToolStripMenuItem.Text = "Create";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.saveToolStripMenuItem.Text = "Save";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // importStripMenuItem
            // 
            this.importStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importThinAppCaptureToolStripMenuItem,
            this.importRegshotCaptureToolStripMenuItem,
            this.importAppVCaptureToolStripMenuItem,
            this.importXRegshotCaptureToolStripMenuItem,
            this.importRegFileToolStripMenuItem,
            this.importFolderToolStripMenuItem});
            this.importStripMenuItem.Name = "importStripMenuItem";
            this.importStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.importStripMenuItem.Text = "Import";
            // 
            // importThinAppCaptureToolStripMenuItem
            // 
            this.importThinAppCaptureToolStripMenuItem.Name = "importThinAppCaptureToolStripMenuItem";
            this.importThinAppCaptureToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.importThinAppCaptureToolStripMenuItem.Text = "Import ThinApp Capture";
            // 
            // importRegshotCaptureToolStripMenuItem
            // 
            this.importRegshotCaptureToolStripMenuItem.Name = "importRegshotCaptureToolStripMenuItem";
            this.importRegshotCaptureToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.importRegshotCaptureToolStripMenuItem.Text = "Import Regshot Capture";
            // 
            // importAppVCaptureToolStripMenuItem
            // 
            this.importAppVCaptureToolStripMenuItem.Name = "importAppVCaptureToolStripMenuItem";
            this.importAppVCaptureToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.importAppVCaptureToolStripMenuItem.Text = "Import AppV Capture";
            // 
            // importXRegshotCaptureToolStripMenuItem
            // 
            this.importXRegshotCaptureToolStripMenuItem.Name = "importXRegshotCaptureToolStripMenuItem";
            this.importXRegshotCaptureToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.importXRegshotCaptureToolStripMenuItem.Text = "Import X-Regshot Capture";
            // 
            // importRegFileToolStripMenuItem
            // 
            this.importRegFileToolStripMenuItem.Name = "importRegFileToolStripMenuItem";
            this.importRegFileToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.importRegFileToolStripMenuItem.Text = "Import RegFile";
            // 
            // importFolderToolStripMenuItem
            // 
            this.importFolderToolStripMenuItem.Name = "importFolderToolStripMenuItem";
            this.importFolderToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.importFolderToolStripMenuItem.Text = "Import Folder";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fixDirectoriesToolStripMenuItem,
            this.deleteFoldersToolStripMenuItem,
            this.ignoreListToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // fixDirectoriesToolStripMenuItem
            // 
            this.fixDirectoriesToolStripMenuItem.Name = "fixDirectoriesToolStripMenuItem";
            this.fixDirectoriesToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.fixDirectoriesToolStripMenuItem.Text = "Fix Directories";
            // 
            // deleteFoldersToolStripMenuItem
            // 
            this.deleteFoldersToolStripMenuItem.Name = "deleteFoldersToolStripMenuItem";
            this.deleteFoldersToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.deleteFoldersToolStripMenuItem.Text = "Delete Folders";
            // 
            // ignoreListToolStripMenuItem
            // 
            this.ignoreListToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ignoreFoldersToolStripMenuItem,
            this.ignoreFilesToolStripMenuItem,
            this.ignoreRegistryToolStripMenuItem});
            this.ignoreListToolStripMenuItem.Name = "ignoreListToolStripMenuItem";
            this.ignoreListToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.ignoreListToolStripMenuItem.Text = "IgnoreList";
            // 
            // ignoreFoldersToolStripMenuItem
            // 
            this.ignoreFoldersToolStripMenuItem.Name = "ignoreFoldersToolStripMenuItem";
            this.ignoreFoldersToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.ignoreFoldersToolStripMenuItem.Text = "Folders";
            // 
            // ignoreFilesToolStripMenuItem
            // 
            this.ignoreFilesToolStripMenuItem.Name = "ignoreFilesToolStripMenuItem";
            this.ignoreFilesToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.ignoreFilesToolStripMenuItem.Text = "Files";
            // 
            // ignoreRegistryToolStripMenuItem
            // 
            this.ignoreRegistryToolStripMenuItem.Name = "ignoreRegistryToolStripMenuItem";
            this.ignoreRegistryToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.ignoreRegistryToolStripMenuItem.Text = "Registry";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // mainTab
            // 
            this.mainTab.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTab.Controls.Add(this.filesPage);
            this.mainTab.Controls.Add(this.registryTab);
            this.mainTab.Controls.Add(this.appInfoPage);
            this.mainTab.Controls.Add(this.launchPage);
            this.mainTab.Controls.Add(this.optionsPage);
            this.mainTab.Location = new System.Drawing.Point(12, 61);
            this.mainTab.Name = "mainTab";
            this.mainTab.SelectedIndex = 0;
            this.mainTab.Size = new System.Drawing.Size(1206, 619);
            this.mainTab.TabIndex = 1;
            // 
            // filesPage
            // 
            this.filesPage.Controls.Add(this.filesTableLayoutPanel);
            this.filesPage.Location = new System.Drawing.Point(4, 22);
            this.filesPage.Name = "filesPage";
            this.filesPage.Padding = new System.Windows.Forms.Padding(3);
            this.filesPage.Size = new System.Drawing.Size(1198, 593);
            this.filesPage.TabIndex = 0;
            this.filesPage.Text = "Files";
            this.filesPage.UseVisualStyleBackColor = true;
            // 
            // filesTableLayoutPanel
            // 
            this.filesTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filesTableLayoutPanel.ColumnCount = 2;
            this.filesTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.filesTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.filesTableLayoutPanel.Controls.Add(this.sourceFilesTree, 0, 0);
            this.filesTableLayoutPanel.Controls.Add(this.appFilesTree, 1, 0);
            this.filesTableLayoutPanel.Location = new System.Drawing.Point(6, 6);
            this.filesTableLayoutPanel.Name = "filesTableLayoutPanel";
            this.filesTableLayoutPanel.RowCount = 1;
            this.filesTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.filesTableLayoutPanel.Size = new System.Drawing.Size(1186, 581);
            this.filesTableLayoutPanel.TabIndex = 7;
            // 
            // sourceFilesTree
            // 
            this.sourceFilesTree.AllowDrop = true;
            this.sourceFilesTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sourceFilesTree.Location = new System.Drawing.Point(3, 3);
            this.sourceFilesTree.Name = "sourceFilesTree";
            this.sourceFilesTree.Size = new System.Drawing.Size(587, 575);
            this.sourceFilesTree.TabIndex = 5;
            // 
            // appFilesTree
            // 
            this.appFilesTree.AllowDrop = true;
            this.appFilesTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.appFilesTree.Location = new System.Drawing.Point(596, 3);
            this.appFilesTree.Name = "appFilesTree";
            this.appFilesTree.Size = new System.Drawing.Size(587, 575);
            this.appFilesTree.TabIndex = 6;
            // 
            // registryTab
            // 
            this.registryTab.Controls.Add(this.registryTableLayoutPanel);
            this.registryTab.Location = new System.Drawing.Point(4, 22);
            this.registryTab.Name = "registryTab";
            this.registryTab.Padding = new System.Windows.Forms.Padding(3);
            this.registryTab.Size = new System.Drawing.Size(1198, 593);
            this.registryTab.TabIndex = 2;
            this.registryTab.Text = "Registry";
            this.registryTab.UseVisualStyleBackColor = true;
            // 
            // registryTableLayoutPanel
            // 
            this.registryTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.registryTableLayoutPanel.ColumnCount = 2;
            this.registryTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.registryTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.registryTableLayoutPanel.Controls.Add(this.appRegTree, 1, 0);
            this.registryTableLayoutPanel.Controls.Add(this.sourceRegTree, 0, 0);
            this.registryTableLayoutPanel.Location = new System.Drawing.Point(6, 6);
            this.registryTableLayoutPanel.Name = "registryTableLayoutPanel";
            this.registryTableLayoutPanel.RowCount = 1;
            this.registryTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.registryTableLayoutPanel.Size = new System.Drawing.Size(1186, 594);
            this.registryTableLayoutPanel.TabIndex = 8;
            // 
            // appRegTree
            // 
            this.appRegTree.AllowDrop = true;
            this.appRegTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.appRegTree.Location = new System.Drawing.Point(596, 3);
            this.appRegTree.Name = "appRegTree";
            this.appRegTree.Size = new System.Drawing.Size(587, 588);
            this.appRegTree.TabIndex = 7;
            // 
            // sourceRegTree
            // 
            this.sourceRegTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sourceRegTree.Location = new System.Drawing.Point(3, 3);
            this.sourceRegTree.Name = "sourceRegTree";
            this.sourceRegTree.Size = new System.Drawing.Size(587, 588);
            this.sourceRegTree.TabIndex = 6;
            // 
            // appInfoPage
            // 
            this.appInfoPage.Controls.Add(this.appInfoEditor);
            this.appInfoPage.Controls.Add(this.appInfoTree);
            this.appInfoPage.Location = new System.Drawing.Point(4, 22);
            this.appInfoPage.Name = "appInfoPage";
            this.appInfoPage.Padding = new System.Windows.Forms.Padding(3);
            this.appInfoPage.Size = new System.Drawing.Size(1198, 593);
            this.appInfoPage.TabIndex = 4;
            this.appInfoPage.Text = "AppInfo";
            this.appInfoPage.UseVisualStyleBackColor = true;
            // 
            // appInfoEditor
            // 
            this.appInfoEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.appInfoEditor.Location = new System.Drawing.Point(459, 1);
            this.appInfoEditor.Name = "appInfoEditor";
            this.appInfoEditor.Size = new System.Drawing.Size(733, 599);
            this.appInfoEditor.TabIndex = 9;
            // 
            // appInfoTree
            // 
            this.appInfoTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.appInfoTree.LabelEdit = true;
            this.appInfoTree.Location = new System.Drawing.Point(6, 12);
            this.appInfoTree.Name = "appInfoTree";
            this.appInfoTree.Size = new System.Drawing.Size(447, 588);
            this.appInfoTree.TabIndex = 8;
            // 
            // launchPage
            // 
            this.launchPage.Controls.Add(this.launchEditor);
            this.launchPage.Controls.Add(this.launchTree);
            this.launchPage.Location = new System.Drawing.Point(4, 22);
            this.launchPage.Name = "launchPage";
            this.launchPage.Padding = new System.Windows.Forms.Padding(3);
            this.launchPage.Size = new System.Drawing.Size(1198, 593);
            this.launchPage.TabIndex = 1;
            this.launchPage.Text = "Launch";
            this.launchPage.UseVisualStyleBackColor = true;
            // 
            // launchEditor
            // 
            this.launchEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.launchEditor.Location = new System.Drawing.Point(591, 0);
            this.launchEditor.Name = "launchEditor";
            this.launchEditor.Size = new System.Drawing.Size(601, 600);
            this.launchEditor.TabIndex = 8;
            // 
            // launchTree
            // 
            this.launchTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.launchTree.Location = new System.Drawing.Point(6, 12);
            this.launchTree.Name = "launchTree";
            this.launchTree.Size = new System.Drawing.Size(579, 588);
            this.launchTree.TabIndex = 7;
            // 
            // optionsPage
            // 
            this.optionsPage.AllowDrop = true;
            this.optionsPage.Controls.Add(this.groupBoxFileConvert);
            this.optionsPage.Controls.Add(this.registryGroupBox);
            this.optionsPage.Controls.Add(this.debugGroupBox);
            this.optionsPage.Location = new System.Drawing.Point(4, 22);
            this.optionsPage.Name = "optionsPage";
            this.optionsPage.Padding = new System.Windows.Forms.Padding(3);
            this.optionsPage.Size = new System.Drawing.Size(1198, 593);
            this.optionsPage.TabIndex = 5;
            this.optionsPage.Text = "Options / Tools";
            this.optionsPage.UseVisualStyleBackColor = true;
            // 
            // groupBoxFileConvert
            // 
            this.groupBoxFileConvert.Controls.Add(this.labelFileConvert);
            this.groupBoxFileConvert.Location = new System.Drawing.Point(390, 119);
            this.groupBoxFileConvert.Name = "groupBoxFileConvert";
            this.groupBoxFileConvert.Size = new System.Drawing.Size(378, 213);
            this.groupBoxFileConvert.TabIndex = 2;
            this.groupBoxFileConvert.TabStop = false;
            this.groupBoxFileConvert.Text = "Folder / File Convert";
            // 
            // registryGroupBox
            // 
            this.registryGroupBox.Controls.Add(this.generateManifestChk);
            this.registryGroupBox.Controls.Add(this.generateRegFilesCheck);
            this.registryGroupBox.Location = new System.Drawing.Point(390, 16);
            this.registryGroupBox.Name = "registryGroupBox";
            this.registryGroupBox.Size = new System.Drawing.Size(378, 97);
            this.registryGroupBox.TabIndex = 1;
            this.registryGroupBox.TabStop = false;
            this.registryGroupBox.Text = "Registry";
            // 
            // generateManifestChk
            // 
            this.generateManifestChk.AutoSize = true;
            this.generateManifestChk.Checked = true;
            this.generateManifestChk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.generateManifestChk.Location = new System.Drawing.Point(23, 59);
            this.generateManifestChk.Name = "generateManifestChk";
            this.generateManifestChk.Size = new System.Drawing.Size(113, 17);
            this.generateManifestChk.TabIndex = 1;
            this.generateManifestChk.Text = "Generate Manifest";
            this.generateManifestChk.UseVisualStyleBackColor = true;
            // 
            // generateRegFilesCheck
            // 
            this.generateRegFilesCheck.AutoSize = true;
            this.generateRegFilesCheck.Checked = true;
            this.generateRegFilesCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.generateRegFilesCheck.Location = new System.Drawing.Point(23, 34);
            this.generateRegFilesCheck.Name = "generateRegFilesCheck";
            this.generateRegFilesCheck.Size = new System.Drawing.Size(117, 17);
            this.generateRegFilesCheck.TabIndex = 0;
            this.generateRegFilesCheck.Text = "Generate Reg Files";
            this.generateRegFilesCheck.UseVisualStyleBackColor = true;
            // 
            // debugGroupBox
            // 
            this.debugGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.debugGroupBox.Controls.Add(this.DEBUG_SEGMENT_List);
            this.debugGroupBox.Controls.Add(this.label2);
            this.debugGroupBox.Controls.Add(this.label1);
            this.debugGroupBox.Controls.Add(this.DEBUG_GLOBAL_Check);
            this.debugGroupBox.Controls.Add(this.DEBUG_OUTPUT_Combo);
            this.debugGroupBox.Controls.Add(this.DEBUG_SEGWRAP_Check);
            this.debugGroupBox.Controls.Add(this.DEBUG_ALL_Check);
            this.debugGroupBox.Location = new System.Drawing.Point(6, 16);
            this.debugGroupBox.Name = "debugGroupBox";
            this.debugGroupBox.Size = new System.Drawing.Size(378, 557);
            this.debugGroupBox.TabIndex = 0;
            this.debugGroupBox.TabStop = false;
            this.debugGroupBox.Text = "Debug";
            // 
            // DEBUG_SEGMENT_List
            // 
            this.DEBUG_SEGMENT_List.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DEBUG_SEGMENT_List.CheckBoxes = true;
            listViewItem2.StateImageIndex = 0;
            this.DEBUG_SEGMENT_List.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem2});
            this.DEBUG_SEGMENT_List.Location = new System.Drawing.Point(15, 116);
            this.DEBUG_SEGMENT_List.Name = "DEBUG_SEGMENT_List";
            this.DEBUG_SEGMENT_List.Size = new System.Drawing.Size(346, 423);
            this.DEBUG_SEGMENT_List.TabIndex = 7;
            this.DEBUG_SEGMENT_List.UseCompatibleStateImageBehavior = false;
            this.DEBUG_SEGMENT_List.View = System.Windows.Forms.View.List;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Debug Segment:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(160, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Debug Output :";
            // 
            // DEBUG_GLOBAL_Check
            // 
            this.DEBUG_GLOBAL_Check.AutoSize = true;
            this.DEBUG_GLOBAL_Check.Location = new System.Drawing.Point(10, 80);
            this.DEBUG_GLOBAL_Check.Name = "DEBUG_GLOBAL_Check";
            this.DEBUG_GLOBAL_Check.Size = new System.Drawing.Size(112, 17);
            this.DEBUG_GLOBAL_Check.TabIndex = 3;
            this.DEBUG_GLOBAL_Check.Text = "DEBUG_GLOBAL";
            this.DEBUG_GLOBAL_Check.UseVisualStyleBackColor = true;
            // 
            // DEBUG_OUTPUT_Combo
            // 
            this.DEBUG_OUTPUT_Combo.FormattingEnabled = true;
            this.DEBUG_OUTPUT_Combo.Items.AddRange(new object[] {
            "nothing",
            "file",
            "messagebox"});
            this.DEBUG_OUTPUT_Combo.Location = new System.Drawing.Point(163, 55);
            this.DEBUG_OUTPUT_Combo.Name = "DEBUG_OUTPUT_Combo";
            this.DEBUG_OUTPUT_Combo.Size = new System.Drawing.Size(186, 21);
            this.DEBUG_OUTPUT_Combo.TabIndex = 2;
            // 
            // DEBUG_SEGWRAP_Check
            // 
            this.DEBUG_SEGWRAP_Check.AutoSize = true;
            this.DEBUG_SEGWRAP_Check.Location = new System.Drawing.Point(10, 57);
            this.DEBUG_SEGWRAP_Check.Name = "DEBUG_SEGWRAP_Check";
            this.DEBUG_SEGWRAP_Check.Size = new System.Drawing.Size(125, 17);
            this.DEBUG_SEGWRAP_Check.TabIndex = 1;
            this.DEBUG_SEGWRAP_Check.Text = "DEBUG_SEGWRAP";
            this.DEBUG_SEGWRAP_Check.UseVisualStyleBackColor = true;
            // 
            // DEBUG_ALL_Check
            // 
            this.DEBUG_ALL_Check.AutoSize = true;
            this.DEBUG_ALL_Check.Checked = true;
            this.DEBUG_ALL_Check.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DEBUG_ALL_Check.Location = new System.Drawing.Point(10, 34);
            this.DEBUG_ALL_Check.Name = "DEBUG_ALL_Check";
            this.DEBUG_ALL_Check.Size = new System.Drawing.Size(89, 17);
            this.DEBUG_ALL_Check.TabIndex = 0;
            this.DEBUG_ALL_Check.Text = "DEBUG_ALL";
            this.DEBUG_ALL_Check.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonCreate,
            this.toolStripButtonOpen,
            this.toolStripButtonSave,
            this.toolStripButtonClear,
            this.toolStripButtonRefresh,
            this.toolStripButtonImportAppv,
            this.toolStripButtonImportThinApp,
            this.toolStripButtonImportX_RegShot,
            this.toolStripButtonImportRegShot,
            this.toolStripButtonImportRegFile,
            this.toolStripButtonImportFolder});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1230, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonCreate
            // 
            this.toolStripButtonCreate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonCreate.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCreate.Image")));
            this.toolStripButtonCreate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCreate.Name = "toolStripButtonCreate";
            this.toolStripButtonCreate.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonCreate.Text = "Create Portable App";
            // 
            // toolStripButtonOpen
            // 
            this.toolStripButtonOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOpen.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOpen.Image")));
            this.toolStripButtonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpen.Name = "toolStripButtonOpen";
            this.toolStripButtonOpen.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonOpen.Text = "Open Portable App";
            // 
            // toolStripButtonSave
            // 
            this.toolStripButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSave.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSave.Image")));
            this.toolStripButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSave.Name = "toolStripButtonSave";
            this.toolStripButtonSave.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonSave.Text = "Save Portable App";
            // 
            // toolStripButtonClear
            // 
            this.toolStripButtonClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonClear.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonClear.Image")));
            this.toolStripButtonClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonClear.Name = "toolStripButtonClear";
            this.toolStripButtonClear.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonClear.Text = "Clear Source Files and Registry";
            // 
            // toolStripButtonRefresh
            // 
            this.toolStripButtonRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRefresh.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRefresh.Image")));
            this.toolStripButtonRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRefresh.Name = "toolStripButtonRefresh";
            this.toolStripButtonRefresh.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonRefresh.Text = "Refresh";
            // 
            // toolStripButtonImportAppv
            // 
            this.toolStripButtonImportAppv.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonImportAppv.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonImportAppv.Image")));
            this.toolStripButtonImportAppv.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonImportAppv.Name = "toolStripButtonImportAppv";
            this.toolStripButtonImportAppv.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonImportAppv.Text = "Import App-V";
            // 
            // toolStripButtonImportThinApp
            // 
            this.toolStripButtonImportThinApp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonImportThinApp.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonImportThinApp.Image")));
            this.toolStripButtonImportThinApp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonImportThinApp.Name = "toolStripButtonImportThinApp";
            this.toolStripButtonImportThinApp.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonImportThinApp.Text = "Import ThinApp";
            // 
            // toolStripButtonImportX_RegShot
            // 
            this.toolStripButtonImportX_RegShot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonImportX_RegShot.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonImportX_RegShot.Image")));
            this.toolStripButtonImportX_RegShot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonImportX_RegShot.Name = "toolStripButtonImportX_RegShot";
            this.toolStripButtonImportX_RegShot.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonImportX_RegShot.Text = "Import X-Regshot";
            // 
            // toolStripButtonImportRegShot
            // 
            this.toolStripButtonImportRegShot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonImportRegShot.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonImportRegShot.Image")));
            this.toolStripButtonImportRegShot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonImportRegShot.Name = "toolStripButtonImportRegShot";
            this.toolStripButtonImportRegShot.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonImportRegShot.Text = "Import Regshot";
            // 
            // toolStripButtonImportRegFile
            // 
            this.toolStripButtonImportRegFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonImportRegFile.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonImportRegFile.Image")));
            this.toolStripButtonImportRegFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonImportRegFile.Name = "toolStripButtonImportRegFile";
            this.toolStripButtonImportRegFile.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonImportRegFile.Text = "Import RegFile";
            // 
            // toolStripButtonImportFolder
            // 
            this.toolStripButtonImportFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonImportFolder.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonImportFolder.Image")));
            this.toolStripButtonImportFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonImportFolder.Name = "toolStripButtonImportFolder";
            this.toolStripButtonImportFolder.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonImportFolder.Text = "Import Folder";
            // 
            // mainStatus
            // 
            this.mainStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabelMain,
            this.statusLabelKeyPress});
            this.mainStatus.Location = new System.Drawing.Point(0, 683);
            this.mainStatus.Name = "mainStatus";
            this.mainStatus.Size = new System.Drawing.Size(1230, 22);
            this.mainStatus.TabIndex = 3;
            // 
            // statusLabelMain
            // 
            this.statusLabelMain.Name = "statusLabelMain";
            this.statusLabelMain.Size = new System.Drawing.Size(1057, 17);
            this.statusLabelMain.Spring = true;
            this.statusLabelMain.Text = "statusLabelMain";
            this.statusLabelMain.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusLabelKeyPress
            // 
            this.statusLabelKeyPress.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusLabelKeyPress.Name = "statusLabelKeyPress";
            this.statusLabelKeyPress.Size = new System.Drawing.Size(158, 17);
            this.statusLabelKeyPress.Text = "toolStripStatusLabelKeyPress";
            this.statusLabelKeyPress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelFileConvert
            // 
            this.labelFileConvert.AllowDrop = true;
            this.labelFileConvert.Location = new System.Drawing.Point(6, 16);
            this.labelFileConvert.Name = "labelFileConvert";
            this.labelFileConvert.Size = new System.Drawing.Size(366, 194);
            this.labelFileConvert.TabIndex = 0;
            this.labelFileConvert.Text = "Folder / File Convert";
            this.labelFileConvert.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Studio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1230, 705);
            this.Controls.Add(this.mainStatus);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.mainTab);
            this.Controls.Add(this.mainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.Name = "Studio";
            this.Text = "Portable App Studio";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Studio_FormClosing);
            this.Load += new System.EventHandler(this.Studio_Load);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.mainTab.ResumeLayout(false);
            this.filesPage.ResumeLayout(false);
            this.filesTableLayoutPanel.ResumeLayout(false);
            this.registryTab.ResumeLayout(false);
            this.registryTableLayoutPanel.ResumeLayout(false);
            this.appInfoPage.ResumeLayout(false);
            this.launchPage.ResumeLayout(false);
            this.optionsPage.ResumeLayout(false);
            this.groupBoxFileConvert.ResumeLayout(false);
            this.registryGroupBox.ResumeLayout(false);
            this.registryGroupBox.PerformLayout();
            this.debugGroupBox.ResumeLayout(false);
            this.debugGroupBox.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.mainStatus.ResumeLayout(false);
            this.mainStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.TabControl mainTab;
        private System.Windows.Forms.TabPage filesPage;
        private System.Windows.Forms.TabPage launchPage;
        private System.Windows.Forms.ToolStripMenuItem importStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importThinAppCaptureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importRegshotCaptureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importAppVCaptureToolStripMenuItem;
        private PortableAppStudio.Controls.TreeViewEx sourceFilesTree;
        private PortableAppStudio.Controls.TreeViewEx launchTree;
        private System.Windows.Forms.TabPage registryTab;
        private PortableAppStudio.Controls.TreeViewEx sourceRegTree;
        private System.Windows.Forms.ToolStripMenuItem importXRegshotCaptureToolStripMenuItem;
        private System.Windows.Forms.TabPage appInfoPage;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDlg;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDlg;
        private System.Windows.Forms.ToolStripMenuItem importRegFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importFolderToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDlg;
        private PortableAppStudio.Controls.TreeViewEx appFilesTree;
        private PortableAppStudio.Controls.TreeViewEx appRegTree;
        private PortableAppStudio.Controls.TreeViewEx appInfoTree;
        private System.Windows.Forms.ToolStripMenuItem createToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel filesTableLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel registryTableLayoutPanel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonCreate;
        private System.Windows.Forms.ToolStripButton toolStripButtonOpen;
        private System.Windows.Forms.ToolStripButton toolStripButtonSave;
        private System.Windows.Forms.ToolStripButton toolStripButtonClear;
        private Controls.PropertyEditorUserControl launchEditor;
        private Controls.PropertyEditorUserControl appInfoEditor;
        private System.Windows.Forms.ToolStripButton toolStripButtonRefresh;
        private System.Windows.Forms.StatusStrip mainStatus;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelMain;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelKeyPress;
        private System.Windows.Forms.TabPage optionsPage;
        private System.Windows.Forms.GroupBox debugGroupBox;
        private System.Windows.Forms.GroupBox registryGroupBox;
        private System.Windows.Forms.CheckBox generateRegFilesCheck;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox DEBUG_GLOBAL_Check;
        private System.Windows.Forms.ComboBox DEBUG_OUTPUT_Combo;
        private System.Windows.Forms.CheckBox DEBUG_SEGWRAP_Check;
        private System.Windows.Forms.CheckBox DEBUG_ALL_Check;
        private System.Windows.Forms.ListView DEBUG_SEGMENT_List;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fixDirectoriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonImportAppv;
        private System.Windows.Forms.ToolStripButton toolStripButtonImportThinApp;
        private System.Windows.Forms.ToolStripButton toolStripButtonImportX_RegShot;
        private System.Windows.Forms.ToolStripButton toolStripButtonImportRegShot;
        private System.Windows.Forms.ToolStripButton toolStripButtonImportRegFile;
        private System.Windows.Forms.ToolStripButton toolStripButtonImportFolder;
        private System.Windows.Forms.ToolStripMenuItem ignoreListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ignoreFoldersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ignoreFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ignoreRegistryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteFoldersToolStripMenuItem;
        private System.Windows.Forms.CheckBox generateManifestChk;
        private System.Windows.Forms.GroupBox groupBoxFileConvert;
        private System.Windows.Forms.Label labelFileConvert;
    }
}

