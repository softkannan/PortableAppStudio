namespace PortableAppStudio
{
    partial class MainStudio
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("DEBUG_SEGMENT_[Launch]");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainStudio));
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
            this.importRegistryCliboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importFilesClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fixDirectoriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteFoldersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ignoreListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ignoreFoldersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ignoreFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ignoreRegistryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchReplaceInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.appVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thinAppToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.fileConvertLabel = new System.Windows.Forms.Label();
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
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.createToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.clearToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.refreshToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.createLauncherToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.runPortableToolStripButton = new System.Windows.Forms.ToolStripSplitButton();
            this.runWithProcMonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runWithRegMonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runWithTracerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewDebugLogToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.mergeListtoolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.editToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.mergeCustomNSIToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.importAppVToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.importFolderToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.importMiscToolStripButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.importXRegshotToolStripButtonDropMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importThinAppToolStripButtonDropMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importRegFileToolStripButtonDropMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importRegshotToolStripButtonDropMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importRegClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolsToolStripSplitButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.whatChangedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.regFromAppToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registryChangesViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileActivityWatchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exeInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processMonitorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resourceHackerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tcpviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.smartSniffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iconsExtractToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.foldersToolStripSplitButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.aPPDATAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aPPDATALocalLowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lOCALAPPDATAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pROGRAMDATAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uSERPROFILEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pUBLICToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tEMPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.systemRootToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cOMMONPROGRAMFILESx86ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commonProgramW6432ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pROGRAMFILESX86ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.programW6432ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainStatus = new System.Windows.Forms.StatusStrip();
            this.statusLabelMain = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelKeyPress = new System.Windows.Forms.ToolStripStatusLabel();
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
            this.mainToolStrip.SuspendLayout();
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
            this.mainMenu.Size = new System.Drawing.Size(1318, 24);
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
            this.importFolderToolStripMenuItem,
            this.importRegistryCliboardToolStripMenuItem,
            this.importFilesClipboardToolStripMenuItem});
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
            // importRegistryCliboardToolStripMenuItem
            // 
            this.importRegistryCliboardToolStripMenuItem.Name = "importRegistryCliboardToolStripMenuItem";
            this.importRegistryCliboardToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.importRegistryCliboardToolStripMenuItem.Text = "Import Registry Clipboard";
            // 
            // importFilesClipboardToolStripMenuItem
            // 
            this.importFilesClipboardToolStripMenuItem.Name = "importFilesClipboardToolStripMenuItem";
            this.importFilesClipboardToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
            this.importFilesClipboardToolStripMenuItem.Text = "Import Files Clipboard";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fixDirectoriesToolStripMenuItem,
            this.deleteFoldersToolStripMenuItem,
            this.ignoreListToolStripMenuItem,
            this.searchReplaceInfoToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // fixDirectoriesToolStripMenuItem
            // 
            this.fixDirectoriesToolStripMenuItem.Name = "fixDirectoriesToolStripMenuItem";
            this.fixDirectoriesToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.fixDirectoriesToolStripMenuItem.Text = "Fix Directories";
            // 
            // deleteFoldersToolStripMenuItem
            // 
            this.deleteFoldersToolStripMenuItem.Name = "deleteFoldersToolStripMenuItem";
            this.deleteFoldersToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.deleteFoldersToolStripMenuItem.Text = "Delete Folders";
            // 
            // ignoreListToolStripMenuItem
            // 
            this.ignoreListToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ignoreFoldersToolStripMenuItem,
            this.ignoreFilesToolStripMenuItem,
            this.ignoreRegistryToolStripMenuItem});
            this.ignoreListToolStripMenuItem.Name = "ignoreListToolStripMenuItem";
            this.ignoreListToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
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
            // searchReplaceInfoToolStripMenuItem
            // 
            this.searchReplaceInfoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.appVToolStripMenuItem,
            this.thinAppToolStripMenuItem});
            this.searchReplaceInfoToolStripMenuItem.Name = "searchReplaceInfoToolStripMenuItem";
            this.searchReplaceInfoToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.searchReplaceInfoToolStripMenuItem.Text = "SearchReplaceInfo";
            // 
            // appVToolStripMenuItem
            // 
            this.appVToolStripMenuItem.Name = "appVToolStripMenuItem";
            this.appVToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.appVToolStripMenuItem.Text = "App-V";
            this.appVToolStripMenuItem.Click += new System.EventHandler(this.appVToolStripMenuItem_Click);
            // 
            // thinAppToolStripMenuItem
            // 
            this.thinAppToolStripMenuItem.Name = "thinAppToolStripMenuItem";
            this.thinAppToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.thinAppToolStripMenuItem.Text = "ThinApp";
            this.thinAppToolStripMenuItem.Click += new System.EventHandler(this.thinAppToolStripMenuItem_Click);
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
            this.mainTab.Location = new System.Drawing.Point(12, 66);
            this.mainTab.Name = "mainTab";
            this.mainTab.SelectedIndex = 0;
            this.mainTab.Size = new System.Drawing.Size(1294, 695);
            this.mainTab.TabIndex = 1;
            // 
            // filesPage
            // 
            this.filesPage.Controls.Add(this.filesTableLayoutPanel);
            this.filesPage.Location = new System.Drawing.Point(4, 22);
            this.filesPage.Name = "filesPage";
            this.filesPage.Padding = new System.Windows.Forms.Padding(3);
            this.filesPage.Size = new System.Drawing.Size(1286, 669);
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
            this.filesTableLayoutPanel.Size = new System.Drawing.Size(1274, 657);
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
            this.sourceFilesTree.Size = new System.Drawing.Size(631, 651);
            this.sourceFilesTree.TabIndex = 5;
            // 
            // appFilesTree
            // 
            this.appFilesTree.AllowDrop = true;
            this.appFilesTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.appFilesTree.Location = new System.Drawing.Point(640, 3);
            this.appFilesTree.Name = "appFilesTree";
            this.appFilesTree.Size = new System.Drawing.Size(631, 651);
            this.appFilesTree.TabIndex = 6;
            // 
            // registryTab
            // 
            this.registryTab.Controls.Add(this.registryTableLayoutPanel);
            this.registryTab.Location = new System.Drawing.Point(4, 22);
            this.registryTab.Name = "registryTab";
            this.registryTab.Padding = new System.Windows.Forms.Padding(3);
            this.registryTab.Size = new System.Drawing.Size(1286, 669);
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
            this.appRegTree.ShowNodeToolTips = true;
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
            this.appInfoPage.Size = new System.Drawing.Size(1286, 669);
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
            this.launchPage.Size = new System.Drawing.Size(1286, 669);
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
            this.optionsPage.Size = new System.Drawing.Size(1286, 669);
            this.optionsPage.TabIndex = 5;
            this.optionsPage.Text = "Options / Tools";
            this.optionsPage.UseVisualStyleBackColor = true;
            // 
            // groupBoxFileConvert
            // 
            this.groupBoxFileConvert.Controls.Add(this.fileConvertLabel);
            this.groupBoxFileConvert.Location = new System.Drawing.Point(390, 119);
            this.groupBoxFileConvert.Name = "groupBoxFileConvert";
            this.groupBoxFileConvert.Size = new System.Drawing.Size(378, 213);
            this.groupBoxFileConvert.TabIndex = 2;
            this.groupBoxFileConvert.TabStop = false;
            this.groupBoxFileConvert.Text = "Folder / File Convert";
            // 
            // fileConvertLabel
            // 
            this.fileConvertLabel.AllowDrop = true;
            this.fileConvertLabel.Location = new System.Drawing.Point(6, 16);
            this.fileConvertLabel.Name = "fileConvertLabel";
            this.fileConvertLabel.Size = new System.Drawing.Size(366, 194);
            this.fileConvertLabel.TabIndex = 0;
            this.fileConvertLabel.Text = "Folder / File Convert";
            this.fileConvertLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.DEBUG_SEGMENT_List.HideSelection = false;
            listViewItem1.StateImageIndex = 0;
            this.DEBUG_SEGMENT_List.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
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
            // mainToolStrip
            // 
            this.mainToolStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator1,
            this.clearToolStripButton,
            this.refreshToolStripButton,
            this.toolStripSeparator2,
            this.createLauncherToolStripButton,
            this.runPortableToolStripButton,
            this.viewDebugLogToolStripButton,
            this.mergeListtoolStripComboBox,
            this.editToolStripButton,
            this.mergeCustomNSIToolStripButton,
            this.toolStripSeparator3,
            this.importAppVToolStripButton,
            this.importFolderToolStripButton,
            this.importMiscToolStripButton,
            this.toolStripSeparator4,
            this.toolsToolStripSplitButton,
            this.foldersToolStripSplitButton});
            this.mainToolStrip.Location = new System.Drawing.Point(0, 24);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Size = new System.Drawing.Size(1318, 39);
            this.mainToolStrip.TabIndex = 2;
            this.mainToolStrip.Text = "Main Tools";
            // 
            // createToolStripButton
            // 
            this.createToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.createToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("createToolStripButton.Image")));
            this.createToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.createToolStripButton.Name = "createToolStripButton";
            this.createToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.createToolStripButton.Text = "Create Portable App";
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.openToolStripButton.Text = "Open Portable App";
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.saveToolStripButton.Text = "Save Portable App";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // clearToolStripButton
            // 
            this.clearToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.clearToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("clearToolStripButton.Image")));
            this.clearToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.clearToolStripButton.Name = "clearToolStripButton";
            this.clearToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.clearToolStripButton.Text = "Clear Source Files and Registry";
            // 
            // refreshToolStripButton
            // 
            this.refreshToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.refreshToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("refreshToolStripButton.Image")));
            this.refreshToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshToolStripButton.Name = "refreshToolStripButton";
            this.refreshToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.refreshToolStripButton.Text = "Refresh";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // createLauncherToolStripButton
            // 
            this.createLauncherToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.createLauncherToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("createLauncherToolStripButton.Image")));
            this.createLauncherToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.createLauncherToolStripButton.Name = "createLauncherToolStripButton";
            this.createLauncherToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.createLauncherToolStripButton.Text = "Create Launcher";
            // 
            // runPortableToolStripButton
            // 
            this.runPortableToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.runPortableToolStripButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runWithProcMonToolStripMenuItem,
            this.runWithRegMonToolStripMenuItem,
            this.runWithTracerToolStripMenuItem});
            this.runPortableToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("runPortableToolStripButton.Image")));
            this.runPortableToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.runPortableToolStripButton.Name = "runPortableToolStripButton";
            this.runPortableToolStripButton.Size = new System.Drawing.Size(48, 36);
            this.runPortableToolStripButton.Text = "Run Portable";
            // 
            // runWithProcMonToolStripMenuItem
            // 
            this.runWithProcMonToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("runWithProcMonToolStripMenuItem.Image")));
            this.runWithProcMonToolStripMenuItem.Name = "runWithProcMonToolStripMenuItem";
            this.runWithProcMonToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.runWithProcMonToolStripMenuItem.Text = "Run With ProcMon";
            // 
            // runWithRegMonToolStripMenuItem
            // 
            this.runWithRegMonToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("runWithRegMonToolStripMenuItem.Image")));
            this.runWithRegMonToolStripMenuItem.Name = "runWithRegMonToolStripMenuItem";
            this.runWithRegMonToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.runWithRegMonToolStripMenuItem.Text = "Run With RegMon";
            this.runWithRegMonToolStripMenuItem.Visible = false;
            // 
            // runWithTracerToolStripMenuItem
            // 
            this.runWithTracerToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("runWithTracerToolStripMenuItem.Image")));
            this.runWithTracerToolStripMenuItem.Name = "runWithTracerToolStripMenuItem";
            this.runWithTracerToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.runWithTracerToolStripMenuItem.Text = "Run With Tracer";
            // 
            // viewDebugLogToolStripButton
            // 
            this.viewDebugLogToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.viewDebugLogToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("viewDebugLogToolStripButton.Image")));
            this.viewDebugLogToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.viewDebugLogToolStripButton.Name = "viewDebugLogToolStripButton";
            this.viewDebugLogToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.viewDebugLogToolStripButton.Text = "View Debug Log Files";
            // 
            // mergeListtoolStripComboBox
            // 
            this.mergeListtoolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mergeListtoolStripComboBox.Name = "mergeListtoolStripComboBox";
            this.mergeListtoolStripComboBox.Size = new System.Drawing.Size(200, 39);
            this.mergeListtoolStripComboBox.ToolTipText = "Importan files that can be editable";
            // 
            // editToolStripButton
            // 
            this.editToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.editToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("editToolStripButton.Image")));
            this.editToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editToolStripButton.Name = "editToolStripButton";
            this.editToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.editToolStripButton.Text = "Edit Selected File";
            // 
            // mergeCustomNSIToolStripButton
            // 
            this.mergeCustomNSIToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mergeCustomNSIToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("mergeCustomNSIToolStripButton.Image")));
            this.mergeCustomNSIToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mergeCustomNSIToolStripButton.Name = "mergeCustomNSIToolStripButton";
            this.mergeCustomNSIToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.mergeCustomNSIToolStripButton.Text = "Merge Custom.nsi";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
            // 
            // importAppVToolStripButton
            // 
            this.importAppVToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.importAppVToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("importAppVToolStripButton.Image")));
            this.importAppVToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.importAppVToolStripButton.Name = "importAppVToolStripButton";
            this.importAppVToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.importAppVToolStripButton.Text = "Import App-V";
            // 
            // importFolderToolStripButton
            // 
            this.importFolderToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.importFolderToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("importFolderToolStripButton.Image")));
            this.importFolderToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.importFolderToolStripButton.Name = "importFolderToolStripButton";
            this.importFolderToolStripButton.Size = new System.Drawing.Size(36, 36);
            this.importFolderToolStripButton.Text = "Import Folder";
            // 
            // importMiscToolStripButton
            // 
            this.importMiscToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.importMiscToolStripButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importXRegshotToolStripButtonDropMenuItem,
            this.importThinAppToolStripButtonDropMenuItem,
            this.importRegFileToolStripButtonDropMenuItem,
            this.importRegshotToolStripButtonDropMenuItem,
            this.importRegClipboardToolStripMenuItem});
            this.importMiscToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("importMiscToolStripButton.Image")));
            this.importMiscToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.importMiscToolStripButton.Name = "importMiscToolStripButton";
            this.importMiscToolStripButton.Size = new System.Drawing.Size(45, 36);
            this.importMiscToolStripButton.Text = "Import Captures";
            // 
            // importXRegshotToolStripButtonDropMenuItem
            // 
            this.importXRegshotToolStripButtonDropMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("importXRegshotToolStripButtonDropMenuItem.Image")));
            this.importXRegshotToolStripButtonDropMenuItem.Name = "importXRegshotToolStripButtonDropMenuItem";
            this.importXRegshotToolStripButtonDropMenuItem.Size = new System.Drawing.Size(188, 22);
            this.importXRegshotToolStripButtonDropMenuItem.Text = "Import X-Regshot";
            // 
            // importThinAppToolStripButtonDropMenuItem
            // 
            this.importThinAppToolStripButtonDropMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("importThinAppToolStripButtonDropMenuItem.Image")));
            this.importThinAppToolStripButtonDropMenuItem.Name = "importThinAppToolStripButtonDropMenuItem";
            this.importThinAppToolStripButtonDropMenuItem.Size = new System.Drawing.Size(188, 22);
            this.importThinAppToolStripButtonDropMenuItem.Text = "Import ThinApp";
            // 
            // importRegFileToolStripButtonDropMenuItem
            // 
            this.importRegFileToolStripButtonDropMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("importRegFileToolStripButtonDropMenuItem.Image")));
            this.importRegFileToolStripButtonDropMenuItem.Name = "importRegFileToolStripButtonDropMenuItem";
            this.importRegFileToolStripButtonDropMenuItem.Size = new System.Drawing.Size(188, 22);
            this.importRegFileToolStripButtonDropMenuItem.Text = "Import RegFile";
            // 
            // importRegshotToolStripButtonDropMenuItem
            // 
            this.importRegshotToolStripButtonDropMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("importRegshotToolStripButtonDropMenuItem.Image")));
            this.importRegshotToolStripButtonDropMenuItem.Name = "importRegshotToolStripButtonDropMenuItem";
            this.importRegshotToolStripButtonDropMenuItem.Size = new System.Drawing.Size(188, 22);
            this.importRegshotToolStripButtonDropMenuItem.Text = "Import Regshot";
            // 
            // importRegClipboardToolStripMenuItem
            // 
            this.importRegClipboardToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("importRegClipboardToolStripMenuItem.Image")));
            this.importRegClipboardToolStripMenuItem.Name = "importRegClipboardToolStripMenuItem";
            this.importRegClipboardToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.importRegClipboardToolStripMenuItem.Text = "Import Reg Clipboard";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 39);
            // 
            // toolsToolStripSplitButton
            // 
            this.toolsToolStripSplitButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolsToolStripSplitButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.whatChangedToolStripMenuItem,
            this.regFromAppToolStripMenuItem,
            this.registryChangesViewToolStripMenuItem,
            this.fileActivityWatchToolStripMenuItem,
            this.exeInfoToolStripMenuItem,
            this.processExplorerToolStripMenuItem,
            this.processMonitorToolStripMenuItem,
            this.resourceHackerToolStripMenuItem,
            this.tcpviewToolStripMenuItem,
            this.smartSniffToolStripMenuItem,
            this.iconsExtractToolStripMenuItem});
            this.toolsToolStripSplitButton.Image = ((System.Drawing.Image)(resources.GetObject("toolsToolStripSplitButton.Image")));
            this.toolsToolStripSplitButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolsToolStripSplitButton.Name = "toolsToolStripSplitButton";
            this.toolsToolStripSplitButton.Size = new System.Drawing.Size(45, 36);
            this.toolsToolStripSplitButton.Text = "Tools";
            // 
            // whatChangedToolStripMenuItem
            // 
            this.whatChangedToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("whatChangedToolStripMenuItem.Image")));
            this.whatChangedToolStripMenuItem.Name = "whatChangedToolStripMenuItem";
            this.whatChangedToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.whatChangedToolStripMenuItem.Text = "What Changed";
            // 
            // regFromAppToolStripMenuItem
            // 
            this.regFromAppToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("regFromAppToolStripMenuItem.Image")));
            this.regFromAppToolStripMenuItem.Name = "regFromAppToolStripMenuItem";
            this.regFromAppToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.regFromAppToolStripMenuItem.Text = "RegFromApp";
            // 
            // registryChangesViewToolStripMenuItem
            // 
            this.registryChangesViewToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("registryChangesViewToolStripMenuItem.Image")));
            this.registryChangesViewToolStripMenuItem.Name = "registryChangesViewToolStripMenuItem";
            this.registryChangesViewToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.registryChangesViewToolStripMenuItem.Text = "RegistryChangesView";
            // 
            // fileActivityWatchToolStripMenuItem
            // 
            this.fileActivityWatchToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("fileActivityWatchToolStripMenuItem.Image")));
            this.fileActivityWatchToolStripMenuItem.Name = "fileActivityWatchToolStripMenuItem";
            this.fileActivityWatchToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.fileActivityWatchToolStripMenuItem.Text = "FileActivityWatch";
            // 
            // exeInfoToolStripMenuItem
            // 
            this.exeInfoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exeInfoToolStripMenuItem.Image")));
            this.exeInfoToolStripMenuItem.Name = "exeInfoToolStripMenuItem";
            this.exeInfoToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.exeInfoToolStripMenuItem.Text = "ExeInfo";
            // 
            // processExplorerToolStripMenuItem
            // 
            this.processExplorerToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("processExplorerToolStripMenuItem.Image")));
            this.processExplorerToolStripMenuItem.Name = "processExplorerToolStripMenuItem";
            this.processExplorerToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.processExplorerToolStripMenuItem.Text = "Process Explorer";
            // 
            // processMonitorToolStripMenuItem
            // 
            this.processMonitorToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("processMonitorToolStripMenuItem.Image")));
            this.processMonitorToolStripMenuItem.Name = "processMonitorToolStripMenuItem";
            this.processMonitorToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.processMonitorToolStripMenuItem.Text = "Process Monitor";
            // 
            // resourceHackerToolStripMenuItem
            // 
            this.resourceHackerToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("resourceHackerToolStripMenuItem.Image")));
            this.resourceHackerToolStripMenuItem.Name = "resourceHackerToolStripMenuItem";
            this.resourceHackerToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.resourceHackerToolStripMenuItem.Text = "Resource Hacker";
            // 
            // tcpviewToolStripMenuItem
            // 
            this.tcpviewToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("tcpviewToolStripMenuItem.Image")));
            this.tcpviewToolStripMenuItem.Name = "tcpviewToolStripMenuItem";
            this.tcpviewToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.tcpviewToolStripMenuItem.Text = "Tcpview";
            // 
            // smartSniffToolStripMenuItem
            // 
            this.smartSniffToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("smartSniffToolStripMenuItem.Image")));
            this.smartSniffToolStripMenuItem.Name = "smartSniffToolStripMenuItem";
            this.smartSniffToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.smartSniffToolStripMenuItem.Text = "SmartSniff";
            // 
            // iconsExtractToolStripMenuItem
            // 
            this.iconsExtractToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("iconsExtractToolStripMenuItem.Image")));
            this.iconsExtractToolStripMenuItem.Name = "iconsExtractToolStripMenuItem";
            this.iconsExtractToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.iconsExtractToolStripMenuItem.Text = "IconsExtract";
            // 
            // foldersToolStripSplitButton
            // 
            this.foldersToolStripSplitButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.foldersToolStripSplitButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aPPDATAToolStripMenuItem,
            this.aPPDATALocalLowToolStripMenuItem,
            this.lOCALAPPDATAToolStripMenuItem,
            this.pROGRAMDATAToolStripMenuItem,
            this.uSERPROFILEToolStripMenuItem,
            this.pUBLICToolStripMenuItem,
            this.tEMPToolStripMenuItem,
            this.systemRootToolStripMenuItem,
            this.cOMMONPROGRAMFILESx86ToolStripMenuItem,
            this.commonProgramW6432ToolStripMenuItem,
            this.pROGRAMFILESX86ToolStripMenuItem,
            this.programW6432ToolStripMenuItem});
            this.foldersToolStripSplitButton.Image = ((System.Drawing.Image)(resources.GetObject("foldersToolStripSplitButton.Image")));
            this.foldersToolStripSplitButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.foldersToolStripSplitButton.Name = "foldersToolStripSplitButton";
            this.foldersToolStripSplitButton.Size = new System.Drawing.Size(45, 36);
            this.foldersToolStripSplitButton.Text = "Common Folders";
            // 
            // aPPDATAToolStripMenuItem
            // 
            this.aPPDATAToolStripMenuItem.Name = "aPPDATAToolStripMenuItem";
            this.aPPDATAToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
            this.aPPDATAToolStripMenuItem.Text = "%APPDATA%";
            // 
            // aPPDATALocalLowToolStripMenuItem
            // 
            this.aPPDATALocalLowToolStripMenuItem.Name = "aPPDATALocalLowToolStripMenuItem";
            this.aPPDATALocalLowToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
            this.aPPDATALocalLowToolStripMenuItem.Text = "%APPDATA%\\..\\LocalLow";
            // 
            // lOCALAPPDATAToolStripMenuItem
            // 
            this.lOCALAPPDATAToolStripMenuItem.Name = "lOCALAPPDATAToolStripMenuItem";
            this.lOCALAPPDATAToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
            this.lOCALAPPDATAToolStripMenuItem.Text = "%LOCALAPPDATA%";
            // 
            // pROGRAMDATAToolStripMenuItem
            // 
            this.pROGRAMDATAToolStripMenuItem.Name = "pROGRAMDATAToolStripMenuItem";
            this.pROGRAMDATAToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
            this.pROGRAMDATAToolStripMenuItem.Text = "%PROGRAMDATA%";
            // 
            // uSERPROFILEToolStripMenuItem
            // 
            this.uSERPROFILEToolStripMenuItem.Name = "uSERPROFILEToolStripMenuItem";
            this.uSERPROFILEToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
            this.uSERPROFILEToolStripMenuItem.Text = "%USERPROFILE%";
            // 
            // pUBLICToolStripMenuItem
            // 
            this.pUBLICToolStripMenuItem.Name = "pUBLICToolStripMenuItem";
            this.pUBLICToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
            this.pUBLICToolStripMenuItem.Text = "%PUBLIC%";
            // 
            // tEMPToolStripMenuItem
            // 
            this.tEMPToolStripMenuItem.Name = "tEMPToolStripMenuItem";
            this.tEMPToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
            this.tEMPToolStripMenuItem.Text = "%TEMP%";
            // 
            // systemRootToolStripMenuItem
            // 
            this.systemRootToolStripMenuItem.Name = "systemRootToolStripMenuItem";
            this.systemRootToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
            this.systemRootToolStripMenuItem.Text = "%SystemRoot%";
            // 
            // cOMMONPROGRAMFILESx86ToolStripMenuItem
            // 
            this.cOMMONPROGRAMFILESx86ToolStripMenuItem.Name = "cOMMONPROGRAMFILESx86ToolStripMenuItem";
            this.cOMMONPROGRAMFILESx86ToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
            this.cOMMONPROGRAMFILESx86ToolStripMenuItem.Text = "%COMMONPROGRAMFILES(x86)%";
            // 
            // commonProgramW6432ToolStripMenuItem
            // 
            this.commonProgramW6432ToolStripMenuItem.Name = "commonProgramW6432ToolStripMenuItem";
            this.commonProgramW6432ToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
            this.commonProgramW6432ToolStripMenuItem.Text = "%CommonProgramW6432%";
            // 
            // pROGRAMFILESX86ToolStripMenuItem
            // 
            this.pROGRAMFILESX86ToolStripMenuItem.Name = "pROGRAMFILESX86ToolStripMenuItem";
            this.pROGRAMFILESX86ToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
            this.pROGRAMFILESX86ToolStripMenuItem.Text = "%PROGRAMFILES(X86)%";
            // 
            // programW6432ToolStripMenuItem
            // 
            this.programW6432ToolStripMenuItem.Name = "programW6432ToolStripMenuItem";
            this.programW6432ToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
            this.programW6432ToolStripMenuItem.Text = "%ProgramW6432%";
            // 
            // mainStatus
            // 
            this.mainStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabelMain,
            this.statusLabelKeyPress});
            this.mainStatus.Location = new System.Drawing.Point(0, 764);
            this.mainStatus.Name = "mainStatus";
            this.mainStatus.Size = new System.Drawing.Size(1318, 22);
            this.mainStatus.TabIndex = 3;
            // 
            // statusLabelMain
            // 
            this.statusLabelMain.Name = "statusLabelMain";
            this.statusLabelMain.Size = new System.Drawing.Size(1145, 17);
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
            // MainStudio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1318, 786);
            this.Controls.Add(this.mainStatus);
            this.Controls.Add(this.mainToolStrip);
            this.Controls.Add(this.mainTab);
            this.Controls.Add(this.mainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.Name = "MainStudio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
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
        private System.Windows.Forms.ToolStrip mainToolStrip;
        private System.Windows.Forms.ToolStripButton createToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripButton clearToolStripButton;
        private Controls.PropertyEditorUserControl launchEditor;
        private Controls.PropertyEditorUserControl appInfoEditor;
        private System.Windows.Forms.ToolStripButton refreshToolStripButton;
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
        private System.Windows.Forms.ToolStripButton importAppVToolStripButton;
        private System.Windows.Forms.ToolStripButton importFolderToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem ignoreListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ignoreFoldersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ignoreFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ignoreRegistryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteFoldersToolStripMenuItem;
        private System.Windows.Forms.CheckBox generateManifestChk;
        private System.Windows.Forms.GroupBox groupBoxFileConvert;
        private System.Windows.Forms.Label fileConvertLabel;
        private System.Windows.Forms.ToolStripMenuItem searchReplaceInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem appVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thinAppToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton createLauncherToolStripButton;
        private System.Windows.Forms.ToolStripButton mergeCustomNSIToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton viewDebugLogToolStripButton;
        private System.Windows.Forms.ToolStripComboBox mergeListtoolStripComboBox;
        private System.Windows.Forms.ToolStripButton editToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem importRegistryCliboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importFilesClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runWithProcMonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runWithRegMonToolStripMenuItem;
        private System.Windows.Forms.ToolStripSplitButton runPortableToolStripButton;
        private System.Windows.Forms.ToolStripDropDownButton toolsToolStripSplitButton;
        private System.Windows.Forms.ToolStripMenuItem whatChangedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem regFromAppToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem registryChangesViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileActivityWatchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exeInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem processExplorerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem processMonitorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resourceHackerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tcpviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem smartSniffToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iconsExtractToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton foldersToolStripSplitButton;
        private System.Windows.Forms.ToolStripMenuItem aPPDATAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aPPDATALocalLowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lOCALAPPDATAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pROGRAMDATAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uSERPROFILEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pUBLICToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tEMPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem systemRootToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cOMMONPROGRAMFILESx86ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem commonProgramW6432ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pROGRAMFILESX86ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem programW6432ToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton importMiscToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem importXRegshotToolStripButtonDropMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importThinAppToolStripButtonDropMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importRegFileToolStripButtonDropMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importRegshotToolStripButtonDropMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem runWithTracerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importRegClipboardToolStripMenuItem;
    }
}

