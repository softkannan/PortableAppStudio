// Decompiled with JetBrains decompiler
// Type: PafLauncherEditor.MainForm
// Assembly: PAFLauncherEditor, Version=5.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5FA988B0-28E9-47EB-9AB7-80FDA0B74460
// Assembly location: G:\csharp\PortableAppGen\Reference\%DEFAULT FOLDER%\PAFLauncherEditor.exe

using INI;
using PafLauncherEditor.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PafLauncherEditor
{
  public class MainForm : Form
  {
    internal readonly Buttons _buttons;
    private State _state;
    internal static bool _isNew;
    private IContainer components;
    private Panel ButtonsPanel;
    public Panel ControlsPanel;
    private FolderBrowserDialog folderBrowserDialog1;
    private StatusStrip statusStrip1;
    internal ToolStripStatusLabel StatusLabel;
    private ToolStripStatusLabel SpacerStatusLabel;
    private ToolTip toolTip1;
    private MenuStrip menuStrip1;
    private ToolStripMenuItem fileToolStripMenuItem;
    private ToolStripMenuItem newToolStripMenuItem;
    private ToolStripMenuItem openToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator;
    private ToolStripMenuItem saveToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripMenuItem exitToolStripMenuItem;
    private ToolStripMenuItem toolsToolStripMenuItem;
    private ToolStripMenuItem BuildToolStripMenuItem;
    private ToolStripMenuItem OptionsToolStripMenuItem;
    private ToolStripMenuItem helpToolStripMenuItem;
    private ToolStripMenuItem WebsiteToolStripMenuItem;
    private ToolStripMenuItem FacebookToolStripMenuItem;
    private ToolStripMenuItem UpdateToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator5;
    private ToolStripMenuItem AboutToolStripMenuItem;
    private ToolStrip toolStrip1;
    private ToolStripButton BuildLauncherToolStripButton;
    private ToolStripButton BuildInstallerToolStripButton;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripLabel AppIdToolStripLabel;
    private ToolStripTextBox AppIdToolStripTextBox;
    private ToolStripSeparator toolStripSeparator6;
    internal ToolStripComboBox EnvironmentsToolStripComboBox;
    private ToolStripMenuItem launcherToolStripMenuItem;
    private ToolStripMenuItem installerToolStripMenuItem;
    private ToolStripButton CopyToolStripButton;
    private ToolStripMenuItem DonateToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator7;
    private ToolStripButton ExploreToolStripButton;
    private ToolStripButton BackupToolStripButton;
    private ToolStripButton ResetToolStripButton;
    private ToolStripMenuItem FeedbackToolStripMenuItem;
    private ToolStripButton NewToolStripButton;
    private ToolStripButton OpenToolStripButton;
    private ToolStripButton SaveToolStripButton;
    private ToolStripSeparator toolStripSeparator8;
    private ToolStripSeparator toolStripSeparator9;
    private ToolStripButton PaypalToolStripButton;
    private ToolStripButton FacebookToolStripButton;
    private ToolStripButton ReloadToolStripButton;
    private ToolStripStatusLabel PathStatusLabel;

    public MainForm()
    {
      this.InitializeComponent();
      this._buttons = new Buttons(this);
      this.EnvironmentsToolStripComboBox.ComboBox.DataSource = (object) new BindingSource((object) AppEnvironments.Instance.Environments, (string) null);
      this.EnvironmentsToolStripComboBox.ComboBox.DisplayMember = "Key";
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
      try
      {
        Settings.Instance.Load();
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.Message, "Error in Settings logic!", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }

    internal void AddButtonControlAndSetAllSectionsToTheirDefaults()
    {
      this.ButtonsPanel.Controls.Clear();
      this.ButtonsPanel.Controls.Add((Control) this._buttons);
      this._buttons.Dock = DockStyle.Fill;
      this._buttons.LaunchButton.PerformClick();
      this._buttons.Defaults();
    }

    internal void LauncherConfigBackedUpState()
    {
      if (File.Exists(ProjectManager.Instance.ThisProject.LauncherIni) && File.Exists(ProjectManager.Instance.ThisProject.LauncherBak) && new FileInfo(ProjectManager.Instance.ThisProject.LauncherBak).Length != 0L)
      {
        this.StatusLabel.ForeColor = Color.Green;
        this.StatusLabel.Text = "Launcher.ini is safely backed up. Edit away!";
        this.BackupToolStripButton.Enabled = true;
      }
      else if (File.Exists(ProjectManager.Instance.ThisProject.LauncherIni) && !File.Exists(ProjectManager.Instance.ThisProject.LauncherBak))
      {
        this.StatusLabel.ForeColor = Color.Red;
        this.StatusLabel.Text = "Launcher.ini is not backed up. It's a good idea to backup the original before you edit.";
        this.BackupToolStripButton.Enabled = true;
      }
      else if (!File.Exists(ProjectManager.Instance.ThisProject.LauncherIni) && File.Exists(ProjectManager.Instance.ThisProject.LauncherBak) && new FileInfo(ProjectManager.Instance.ThisProject.LauncherBak).Length != 0L)
      {
        this.StatusLabel.ForeColor = Color.Green;
        this.StatusLabel.Text = "Launcher.ini was not found, but there is a backup. Click restore from backup button and reload, then you can edit away.";
        this.BackupToolStripButton.Enabled = true;
      }
      else
      {
        this.StatusLabel.ForeColor = Color.Gray;
        this.StatusLabel.Text = "Launcher.ini not Found!";
        this.BackupToolStripButton.Enabled = false;
      }
    }

    private static void Donate()
    {
      Process.Start("" + "https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=FA3S3LU2N7G2C&lc=US&item_name=PAF%20Launcher%20Editor&currency_code=USD&bn=PP%2dDonationsBF%3abtn_donateCC_LG%2egif%3aNonHosted");
    }

    private void NewToolStripButton_Click(object sender, EventArgs e)
    {
      if (new NewForm().ShowDialog() != DialogResult.OK)
        return;
      this.LoadApplicationDefaults();
      this.LoadProjectToEdit();
      this.EnableButtons();
      int num = (int) MessageBox.Show("New project created successfully.", "Done!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }

    private void LoadProjectToEdit()
    {
      this.AppIdToolStripTextBox.Text = ProjectManager.Instance.ThisProject.Id;
      this.Text = ProjectManager.Instance.ThisProject.Id + " - " + this.Tag;
      this._state = File.Exists(ProjectManager.Instance.ThisProject.LauncherIni) ? (File.Exists(ProjectManager.Instance.ThisProject.AppInfoIni) ? State.Full : State.Launcher) : (File.Exists(ProjectManager.Instance.ThisProject.AppInfoIni) ? State.AppInfo : State.New);
      this.StatusLabel.Text = "Path: " + ProjectManager.Instance.ThisProject.Path;
    }

    private void LoadApplicationDefaults()
    {
      this.ButtonsPanel.Controls.Clear();
      this.ButtonsPanel.Controls.Add((Control) this._buttons);
      this._buttons.Dock = DockStyle.Fill;
      this._buttons.Defaults();
      this._buttons.LaunchButton.PerformClick();
    }

    private void CopyToolStripButton_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrWhiteSpace(this.EnvironmentsToolStripComboBox.Text))
        return;
      Clipboard.SetText(this.EnvironmentsToolStripComboBox.Text);
    }

    private void EnvironmentsToolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.CopyToolStripButton.Enabled = true;
      foreach (KeyValuePair<string, string> keyValuePair in AppEnvironments.Instance.Environments.Where<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (item => item.Key == this.EnvironmentsToolStripComboBox.Text)))
        this.toolTip1.SetToolTip(this.EnvironmentsToolStripComboBox.Control, keyValuePair.Value);
    }

    private void donateToolStripMenuItem_Click(object sender, EventArgs e)
    {
      MainForm.Donate();
    }

    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
      using (About about = new About())
      {
        int num = (int) about.ShowDialog((IWin32Window) this);
      }
    }

    private void ExploreToolStripButton_Click(object sender, EventArgs e)
    {
      if (ProjectManager.Instance.ThisProject == null)
        return;
      if (!string.IsNullOrWhiteSpace(ProjectManager.Instance.ThisProject.Path) && Directory.Exists(ProjectManager.Instance.ThisProject.Path))
      {
        Process.Start(ProjectManager.Instance.ThisProject.Path);
      }
      else
      {
        int num = (int) MessageBox.Show("Directory don't exist.");
      }
    }

    private void ResetToolStripButton_Click(object sender, EventArgs e)
    {
      if (!Directory.Exists(ProjectManager.Instance.ThisProject.Path))
        return;
      this._buttons.Reset();
    }

    private void BackupToolStripButton_MouseDown(object sender, MouseEventArgs e)
    {
      if (ProjectManager.Instance.ThisProject == null || !Directory.Exists(ProjectManager.Instance.ThisProject.Path))
        return;
      switch (e.Button)
      {
        case MouseButtons.Left:
          if (File.Exists(ProjectManager.Instance.ThisProject.LauncherIni))
          {
            File.Copy(ProjectManager.Instance.ThisProject.LauncherIni, ProjectManager.Instance.ThisProject.LauncherBak, true);
            int num = (int) MessageBox.Show("Backup successful.", "Done.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            break;
          }
          break;
        case MouseButtons.Right:
          if (File.Exists(ProjectManager.Instance.ThisProject.LauncherBak))
          {
            File.Copy(ProjectManager.Instance.ThisProject.LauncherBak, ProjectManager.Instance.ThisProject.LauncherIni, true);
            int num = (int) MessageBox.Show("Restore successful.", "Done.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            break;
          }
          int num1 = (int) MessageBox.Show("No backup exist.");
          break;
      }
      this.LauncherConfigBackedUpState();
    }

    private void BuildLauncherToolStripButton_Click(object sender, EventArgs e)
    {
      if (ProjectManager.Instance.ThisProject == null)
        return;
      ProjectManager.Instance.Generate(Generator.Launcher, ProjectManager.Instance.ThisProject.Path);
    }

    private void BuildInstallerToolStripButton_Click(object sender, EventArgs e)
    {
      if (ProjectManager.Instance.ThisProject == null)
        return;
      ProjectManager.Instance.Generate(Generator.Installer, ProjectManager.Instance.ThisProject.Path);
    }

    private void OpenToolStripButton_Click(object sender, EventArgs e)
    {
      if (new OpenForm().ShowDialog() != DialogResult.OK)
        return;
      this.LoadApplicationDefaults();
      this.LoadProjectToEdit();
      this.LoadLauncherConfig();
      this.EnableButtons();
    }

    private void LoadLauncherConfig()
    {
      if (File.Exists(ProjectManager.Instance.ThisProject.LauncherIni) && new FileInfo(ProjectManager.Instance.ThisProject.LauncherIni).Length > 0L)
      {
        foreach (Section section in IniDocument.Load(File.ReadLines(ProjectManager.Instance.ThisProject.LauncherIni)).get_Sections())
          this._buttons.PopulateUserControls(section);
        int num = (int) MessageBox.Show("Project loading completed.", "Success.");
      }
      else
      {
        int num1 = (int) MessageBox.Show("The editor was set to defaults.", "This app is not in PAL format.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
      }
    }

    private void AppIdToolStripTextBox_TextChanged(object sender, EventArgs e)
    {
    }

    private void SaveToolStripButton_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Are you sure you want to save your changes?", "Confirmation!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
        return;
      ProjectManager.Instance.CreateDirectoryStructure(ProjectManager.Instance.ThisProject.Path);
      ProjectManager.Instance.Add(ProjectManager.Instance.ThisProject);
      this.WriteLauncher(ProjectManager.Instance.ThisProject.LauncherIni);
      Settings.Instance.Save(ProjectManager.Instance.ThisProject);
      this.EnableButtons();
      int num = (int) MessageBox.Show("Save successful!", "Done!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }

    private void EnableButtons()
    {
      this.SaveToolStripButton.Enabled = true;
      this.ResetToolStripButton.Enabled = true;
      this.EnvironmentsToolStripComboBox.Enabled = true;
    }

    private void WriteLauncher(string launcherFile)
    {
      IniDocument iniDocument = new IniDocument("iniDocument", "", "");
      iniDocument.Add(this._buttons.Launch.Write2File(), true);
      iniDocument.Add(this._buttons.Activate.Write2File(), true);
      iniDocument.Add(this._buttons.LiveMode.Write2File(), true);
      iniDocument.Add(this._buttons.Environment.Write2File(), true);
      iniDocument.Add(this._buttons.RegistryKeys.Write2File(), true);
      iniDocument.Add(this._buttons.RegistryValueWrite.Write2File(), true);
      iniDocument.Add(this._buttons.RegistryCleanup.Write2File(), true);
      iniDocument.Add(this._buttons.FileWriteN.Write2File(), true);
      iniDocument.Add(this._buttons.FilesMove.Write2File(), true);
      iniDocument.Add(this._buttons.DirectoriesMove.Write2File(), true);
      iniDocument.Add(this._buttons.DirectoriesCleanup.Write2File(), true);
      iniDocument.Add(this._buttons.Language.Write2File(), true);
      iniDocument.Add(this._buttons.LanguageStrings.Write2File(), true);
      iniDocument.Add(this._buttons.LanguageFile.Write2File(), true);
      iniDocument.Add(this._buttons.Custom.Write2File(), true);
      iniDocument.Remove(Descendants.Empty);
      foreach (Section section in iniDocument.get_Sections())
        section.Remove(Descendants.Empty);
      iniDocument.Footer = "Generated with PAF Launcher Editor 5";
      iniDocument.SpaceOutSections = true;
      File.WriteAllLines(launcherFile, iniDocument.Lines(), Encoding.Default);
    }

    private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      int num = (int) new OptionsForm().ShowDialog();
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void ReloadToolStripButton_Click(object sender, EventArgs e)
    {
      if (ProjectManager.Instance.ThisProject == null || !Directory.Exists(ProjectManager.Instance.ThisProject.Path))
        return;
      this.LoadApplicationDefaults();
      this.LoadProjectToEdit();
      this.LoadLauncherConfig();
      this.EnableButtons();
    }

    private void contentsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Process.Start("http://compucode.blogspot.com/");
    }

    private void feedbackToolStripMenuItem_Click(object sender, EventArgs e)
    {
      new FeedbackForm().Show();
    }

    private void UpdateToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        new Updater().CheckForUpdates();
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.Message);
      }
    }

    private void FacebookToolStripButton_Click(object sender, EventArgs e)
    {
      Process.Start("https://www.facebook.com/pages/CompuCode/751592058256641");
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (MainForm));
      this.ControlsPanel = new Panel();
      this.ButtonsPanel = new Panel();
      this.folderBrowserDialog1 = new FolderBrowserDialog();
      this.statusStrip1 = new StatusStrip();
      this.StatusLabel = new ToolStripStatusLabel();
      this.SpacerStatusLabel = new ToolStripStatusLabel();
      this.PathStatusLabel = new ToolStripStatusLabel();
      this.toolTip1 = new ToolTip(this.components);
      this.menuStrip1 = new MenuStrip();
      this.fileToolStripMenuItem = new ToolStripMenuItem();
      this.newToolStripMenuItem = new ToolStripMenuItem();
      this.openToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator = new ToolStripSeparator();
      this.saveToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator1 = new ToolStripSeparator();
      this.exitToolStripMenuItem = new ToolStripMenuItem();
      this.toolsToolStripMenuItem = new ToolStripMenuItem();
      this.BuildToolStripMenuItem = new ToolStripMenuItem();
      this.launcherToolStripMenuItem = new ToolStripMenuItem();
      this.installerToolStripMenuItem = new ToolStripMenuItem();
      this.OptionsToolStripMenuItem = new ToolStripMenuItem();
      this.helpToolStripMenuItem = new ToolStripMenuItem();
      this.WebsiteToolStripMenuItem = new ToolStripMenuItem();
      this.FacebookToolStripMenuItem = new ToolStripMenuItem();
      this.DonateToolStripMenuItem = new ToolStripMenuItem();
      this.FeedbackToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator5 = new ToolStripSeparator();
      this.UpdateToolStripMenuItem = new ToolStripMenuItem();
      this.AboutToolStripMenuItem = new ToolStripMenuItem();
      this.toolStrip1 = new ToolStrip();
      this.NewToolStripButton = new ToolStripButton();
      this.OpenToolStripButton = new ToolStripButton();
      this.ReloadToolStripButton = new ToolStripButton();
      this.SaveToolStripButton = new ToolStripButton();
      this.ResetToolStripButton = new ToolStripButton();
      this.toolStripSeparator8 = new ToolStripSeparator();
      this.AppIdToolStripLabel = new ToolStripLabel();
      this.AppIdToolStripTextBox = new ToolStripTextBox();
      this.toolStripSeparator6 = new ToolStripSeparator();
      this.EnvironmentsToolStripComboBox = new ToolStripComboBox();
      this.CopyToolStripButton = new ToolStripButton();
      this.toolStripSeparator2 = new ToolStripSeparator();
      this.BuildLauncherToolStripButton = new ToolStripButton();
      this.BuildInstallerToolStripButton = new ToolStripButton();
      this.toolStripSeparator7 = new ToolStripSeparator();
      this.ExploreToolStripButton = new ToolStripButton();
      this.BackupToolStripButton = new ToolStripButton();
      this.toolStripSeparator9 = new ToolStripSeparator();
      this.FacebookToolStripButton = new ToolStripButton();
      this.PaypalToolStripButton = new ToolStripButton();
      this.statusStrip1.SuspendLayout();
      this.menuStrip1.SuspendLayout();
      this.toolStrip1.SuspendLayout();
      this.SuspendLayout();
      this.ControlsPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.ControlsPanel.Location = new Point(171, 52);
      this.ControlsPanel.Name = "ControlsPanel";
      this.ControlsPanel.Size = new Size(621, 492);
      this.ControlsPanel.TabIndex = 10;
      this.ControlsPanel.Visible = false;
      this.ButtonsPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
      this.ButtonsPanel.Location = new Point(0, 52);
      this.ButtonsPanel.Name = "ButtonsPanel";
      this.ButtonsPanel.Size = new Size(165, 492);
      this.ButtonsPanel.TabIndex = 9;
      this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer;
      this.folderBrowserDialog1.ShowNewFolderButton = false;
      this.statusStrip1.Items.AddRange(new ToolStripItem[3]
      {
        (ToolStripItem) this.StatusLabel,
        (ToolStripItem) this.SpacerStatusLabel,
        (ToolStripItem) this.PathStatusLabel
      });
      this.statusStrip1.Location = new Point(0, 547);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.ShowItemToolTips = true;
      this.statusStrip1.Size = new Size(792, 22);
      this.statusStrip1.SizingGrip = false;
      this.statusStrip1.TabIndex = 11;
      this.statusStrip1.Text = "statusStrip1";
      this.StatusLabel.Name = "StatusLabel";
      this.StatusLabel.Size = new Size(168, 17);
      this.StatusLabel.Text = "Smile because Jesus loves you.";
      this.SpacerStatusLabel.DisplayStyle = ToolStripItemDisplayStyle.None;
      this.SpacerStatusLabel.Name = "SpacerStatusLabel";
      this.SpacerStatusLabel.Size = new Size(609, 17);
      this.SpacerStatusLabel.Spring = true;
      this.SpacerStatusLabel.Text = "toolStripStatusLabel2";
      this.PathStatusLabel.DisplayStyle = ToolStripItemDisplayStyle.Text;
      this.PathStatusLabel.Name = "PathStatusLabel";
      this.PathStatusLabel.Size = new Size(34, 17);
      this.PathStatusLabel.Text = "Path:";
      this.PathStatusLabel.Visible = false;
      this.toolTip1.AutoPopDelay = 8000;
      this.toolTip1.InitialDelay = 500;
      this.toolTip1.ReshowDelay = 100;
      this.toolTip1.ToolTipIcon = ToolTipIcon.Info;
      this.toolTip1.ToolTipTitle = "PAF Launcher Editor";
      this.toolTip1.UseAnimation = false;
      this.toolTip1.UseFading = false;
      this.menuStrip1.Items.AddRange(new ToolStripItem[3]
      {
        (ToolStripItem) this.fileToolStripMenuItem,
        (ToolStripItem) this.toolsToolStripMenuItem,
        (ToolStripItem) this.helpToolStripMenuItem
      });
      this.menuStrip1.Location = new Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.RenderMode = ToolStripRenderMode.Professional;
      this.menuStrip1.Size = new Size(792, 24);
      this.menuStrip1.TabIndex = 0;
      this.menuStrip1.Text = "menuStrip1";
      this.fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[6]
      {
        (ToolStripItem) this.newToolStripMenuItem,
        (ToolStripItem) this.openToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator,
        (ToolStripItem) this.saveToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator1,
        (ToolStripItem) this.exitToolStripMenuItem
      });
      this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      this.fileToolStripMenuItem.Size = new Size(37, 20);
      this.fileToolStripMenuItem.Text = "&File";
      this.newToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("newToolStripMenuItem.Image");
      this.newToolStripMenuItem.ImageTransparentColor = Color.Magenta;
      this.newToolStripMenuItem.Name = "newToolStripMenuItem";
      this.newToolStripMenuItem.ShortcutKeys = Keys.N | Keys.Control;
      this.newToolStripMenuItem.Size = new Size(146, 22);
      this.newToolStripMenuItem.Text = "&New";
      this.newToolStripMenuItem.Click += new EventHandler(this.NewToolStripButton_Click);
      this.openToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("openToolStripMenuItem.Image");
      this.openToolStripMenuItem.ImageTransparentColor = Color.Magenta;
      this.openToolStripMenuItem.Name = "openToolStripMenuItem";
      this.openToolStripMenuItem.ShortcutKeys = Keys.O | Keys.Control;
      this.openToolStripMenuItem.Size = new Size(146, 22);
      this.openToolStripMenuItem.Text = "&Open";
      this.openToolStripMenuItem.Click += new EventHandler(this.OpenToolStripButton_Click);
      this.toolStripSeparator.Name = "toolStripSeparator";
      this.toolStripSeparator.Size = new Size(143, 6);
      this.saveToolStripMenuItem.Image = (Image) componentResourceManager.GetObject("saveToolStripMenuItem.Image");
      this.saveToolStripMenuItem.ImageTransparentColor = Color.Magenta;
      this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
      this.saveToolStripMenuItem.ShortcutKeys = Keys.S | Keys.Control;
      this.saveToolStripMenuItem.Size = new Size(146, 22);
      this.saveToolStripMenuItem.Text = "&Save";
      this.saveToolStripMenuItem.Click += new EventHandler(this.SaveToolStripButton_Click);
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new Size(143, 6);
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.Size = new Size(146, 22);
      this.exitToolStripMenuItem.Text = "E&xit";
      this.exitToolStripMenuItem.Click += new EventHandler(this.exitToolStripMenuItem_Click);
      this.toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.BuildToolStripMenuItem,
        (ToolStripItem) this.OptionsToolStripMenuItem
      });
      this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
      this.toolsToolStripMenuItem.Size = new Size(48, 20);
      this.toolsToolStripMenuItem.Text = "&Tools";
      this.BuildToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.launcherToolStripMenuItem,
        (ToolStripItem) this.installerToolStripMenuItem
      });
      this.BuildToolStripMenuItem.Image = (Image) Resources.build;
      this.BuildToolStripMenuItem.Name = "BuildToolStripMenuItem";
      this.BuildToolStripMenuItem.Size = new Size(116, 22);
      this.BuildToolStripMenuItem.Text = "&Build";
      this.launcherToolStripMenuItem.Image = (Image) Resources.launcher;
      this.launcherToolStripMenuItem.Name = "launcherToolStripMenuItem";
      this.launcherToolStripMenuItem.Size = new Size(123, 22);
      this.launcherToolStripMenuItem.Text = "&Launcher";
      this.launcherToolStripMenuItem.Click += new EventHandler(this.BuildLauncherToolStripButton_Click);
      this.installerToolStripMenuItem.Image = (Image) Resources.installer;
      this.installerToolStripMenuItem.Name = "installerToolStripMenuItem";
      this.installerToolStripMenuItem.Size = new Size(123, 22);
      this.installerToolStripMenuItem.Text = "&Installer";
      this.installerToolStripMenuItem.Click += new EventHandler(this.BuildInstallerToolStripButton_Click);
      this.OptionsToolStripMenuItem.Image = (Image) Resources.options;
      this.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem";
      this.OptionsToolStripMenuItem.Size = new Size(116, 22);
      this.OptionsToolStripMenuItem.Text = "&Options";
      this.OptionsToolStripMenuItem.Click += new EventHandler(this.optionsToolStripMenuItem_Click);
      this.helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[7]
      {
        (ToolStripItem) this.WebsiteToolStripMenuItem,
        (ToolStripItem) this.FacebookToolStripMenuItem,
        (ToolStripItem) this.DonateToolStripMenuItem,
        (ToolStripItem) this.FeedbackToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator5,
        (ToolStripItem) this.UpdateToolStripMenuItem,
        (ToolStripItem) this.AboutToolStripMenuItem
      });
      this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
      this.helpToolStripMenuItem.Size = new Size(44, 20);
      this.helpToolStripMenuItem.Text = "&Help";
      this.WebsiteToolStripMenuItem.Image = (Image) Resources.web;
      this.WebsiteToolStripMenuItem.Name = "WebsiteToolStripMenuItem";
      this.WebsiteToolStripMenuItem.Size = new Size(171, 22);
      this.WebsiteToolStripMenuItem.Text = "&Website";
      this.WebsiteToolStripMenuItem.Click += new EventHandler(this.contentsToolStripMenuItem_Click);
      this.FacebookToolStripMenuItem.Image = (Image) Resources.facebook;
      this.FacebookToolStripMenuItem.Name = "FacebookToolStripMenuItem";
      this.FacebookToolStripMenuItem.Size = new Size(171, 22);
      this.FacebookToolStripMenuItem.Text = "&Facebook";
      this.FacebookToolStripMenuItem.Click += new EventHandler(this.FacebookToolStripButton_Click);
      this.DonateToolStripMenuItem.Image = (Image) Resources.paypal;
      this.DonateToolStripMenuItem.Name = "DonateToolStripMenuItem";
      this.DonateToolStripMenuItem.Size = new Size(171, 22);
      this.DonateToolStripMenuItem.Text = "&Donate";
      this.DonateToolStripMenuItem.Click += new EventHandler(this.donateToolStripMenuItem_Click);
      this.FeedbackToolStripMenuItem.Image = (Image) Resources.feedback;
      this.FeedbackToolStripMenuItem.Name = "FeedbackToolStripMenuItem";
      this.FeedbackToolStripMenuItem.Size = new Size(171, 22);
      this.FeedbackToolStripMenuItem.Text = "Feed&back";
      this.FeedbackToolStripMenuItem.Click += new EventHandler(this.feedbackToolStripMenuItem_Click);
      this.toolStripSeparator5.Name = "toolStripSeparator5";
      this.toolStripSeparator5.Size = new Size(168, 6);
      this.UpdateToolStripMenuItem.Image = (Image) Resources.update;
      this.UpdateToolStripMenuItem.Name = "UpdateToolStripMenuItem";
      this.UpdateToolStripMenuItem.Size = new Size(171, 22);
      this.UpdateToolStripMenuItem.Text = "&Check for Updates";
      this.UpdateToolStripMenuItem.Click += new EventHandler(this.UpdateToolStripMenuItem_Click);
      this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
      this.AboutToolStripMenuItem.Size = new Size(171, 22);
      this.AboutToolStripMenuItem.Text = "&About...";
      this.AboutToolStripMenuItem.Click += new EventHandler(this.aboutToolStripMenuItem_Click);
      this.toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
      this.toolStrip1.Items.AddRange(new ToolStripItem[20]
      {
        (ToolStripItem) this.NewToolStripButton,
        (ToolStripItem) this.OpenToolStripButton,
        (ToolStripItem) this.ReloadToolStripButton,
        (ToolStripItem) this.SaveToolStripButton,
        (ToolStripItem) this.ResetToolStripButton,
        (ToolStripItem) this.toolStripSeparator8,
        (ToolStripItem) this.AppIdToolStripLabel,
        (ToolStripItem) this.AppIdToolStripTextBox,
        (ToolStripItem) this.toolStripSeparator6,
        (ToolStripItem) this.EnvironmentsToolStripComboBox,
        (ToolStripItem) this.CopyToolStripButton,
        (ToolStripItem) this.toolStripSeparator2,
        (ToolStripItem) this.BuildLauncherToolStripButton,
        (ToolStripItem) this.BuildInstallerToolStripButton,
        (ToolStripItem) this.toolStripSeparator7,
        (ToolStripItem) this.ExploreToolStripButton,
        (ToolStripItem) this.BackupToolStripButton,
        (ToolStripItem) this.toolStripSeparator9,
        (ToolStripItem) this.FacebookToolStripButton,
        (ToolStripItem) this.PaypalToolStripButton
      });
      this.toolStrip1.Location = new Point(0, 24);
      this.toolStrip1.Name = "toolStrip1";
      this.toolStrip1.RenderMode = ToolStripRenderMode.Professional;
      this.toolStrip1.Size = new Size(792, 25);
      this.toolStrip1.TabIndex = 12;
      this.toolStrip1.Text = "toolStrip1";
      this.NewToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.NewToolStripButton.Image = (Image) Resources.create;
      this.NewToolStripButton.ImageTransparentColor = Color.Magenta;
      this.NewToolStripButton.Name = "NewToolStripButton";
      this.NewToolStripButton.Size = new Size(23, 22);
      this.NewToolStripButton.Text = "New";
      this.NewToolStripButton.ToolTipText = "Create a new project.";
      this.NewToolStripButton.Click += new EventHandler(this.NewToolStripButton_Click);
      this.OpenToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.OpenToolStripButton.Image = (Image) Resources.open;
      this.OpenToolStripButton.ImageTransparentColor = Color.Magenta;
      this.OpenToolStripButton.Name = "OpenToolStripButton";
      this.OpenToolStripButton.Size = new Size(23, 22);
      this.OpenToolStripButton.Text = "Open";
      this.OpenToolStripButton.ToolTipText = "Open a project to work with.";
      this.OpenToolStripButton.Click += new EventHandler(this.OpenToolStripButton_Click);
      this.ReloadToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.ReloadToolStripButton.Image = (Image) Resources.reload;
      this.ReloadToolStripButton.ImageTransparentColor = Color.Magenta;
      this.ReloadToolStripButton.Name = "ReloadToolStripButton";
      this.ReloadToolStripButton.Size = new Size(23, 22);
      this.ReloadToolStripButton.Text = "Reload";
      this.ReloadToolStripButton.ToolTipText = "Reload the project.";
      this.ReloadToolStripButton.Click += new EventHandler(this.ReloadToolStripButton_Click);
      this.SaveToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.SaveToolStripButton.Enabled = false;
      this.SaveToolStripButton.Image = (Image) Resources.save;
      this.SaveToolStripButton.ImageTransparentColor = Color.Magenta;
      this.SaveToolStripButton.Name = "SaveToolStripButton";
      this.SaveToolStripButton.Size = new Size(23, 22);
      this.SaveToolStripButton.Text = "Save";
      this.SaveToolStripButton.ToolTipText = "Save the project.";
      this.SaveToolStripButton.Click += new EventHandler(this.SaveToolStripButton_Click);
      this.ResetToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.ResetToolStripButton.Enabled = false;
      this.ResetToolStripButton.Image = (Image) Resources.reset;
      this.ResetToolStripButton.ImageTransparentColor = Color.Magenta;
      this.ResetToolStripButton.Name = "ResetToolStripButton";
      this.ResetToolStripButton.Size = new Size(23, 22);
      this.ResetToolStripButton.Text = "Reset";
      this.ResetToolStripButton.ToolTipText = "Reset the current tab to defaults.";
      this.ResetToolStripButton.Click += new EventHandler(this.ResetToolStripButton_Click);
      this.toolStripSeparator8.Name = "toolStripSeparator8";
      this.toolStripSeparator8.Size = new Size(6, 25);
      this.AppIdToolStripLabel.Name = "AppIdToolStripLabel";
      this.AppIdToolStripLabel.Size = new Size(43, 22);
      this.AppIdToolStripLabel.Text = "AppID:";
      this.AppIdToolStripTextBox.BorderStyle = BorderStyle.FixedSingle;
      this.AppIdToolStripTextBox.Name = "AppIdToolStripTextBox";
      this.AppIdToolStripTextBox.ReadOnly = true;
      this.AppIdToolStripTextBox.Size = new Size(150, 25);
      this.AppIdToolStripTextBox.TextChanged += new EventHandler(this.AppIdToolStripTextBox_TextChanged);
      this.toolStripSeparator6.Name = "toolStripSeparator6";
      this.toolStripSeparator6.Size = new Size(6, 25);
      this.EnvironmentsToolStripComboBox.Enabled = false;
      this.EnvironmentsToolStripComboBox.FlatStyle = FlatStyle.System;
      this.EnvironmentsToolStripComboBox.Name = "EnvironmentsToolStripComboBox";
      this.EnvironmentsToolStripComboBox.Size = new Size(245, 25);
      this.EnvironmentsToolStripComboBox.SelectedIndexChanged += new EventHandler(this.EnvironmentsToolStripComboBox_SelectedIndexChanged);
      this.CopyToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.CopyToolStripButton.Enabled = false;
      this.CopyToolStripButton.Image = (Image) Resources.copy;
      this.CopyToolStripButton.ImageTransparentColor = Color.Magenta;
      this.CopyToolStripButton.Name = "CopyToolStripButton";
      this.CopyToolStripButton.Size = new Size(23, 22);
      this.CopyToolStripButton.Text = "&Copy";
      this.CopyToolStripButton.ToolTipText = "Copy the environment to the clipboard.";
      this.CopyToolStripButton.Click += new EventHandler(this.CopyToolStripButton_Click);
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new Size(6, 25);
      this.BuildLauncherToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.BuildLauncherToolStripButton.Image = (Image) Resources.launcher;
      this.BuildLauncherToolStripButton.ImageTransparentColor = Color.Magenta;
      this.BuildLauncherToolStripButton.Name = "BuildLauncherToolStripButton";
      this.BuildLauncherToolStripButton.Size = new Size(23, 22);
      this.BuildLauncherToolStripButton.Text = "Build the Launcher.";
      this.BuildLauncherToolStripButton.Click += new EventHandler(this.BuildLauncherToolStripButton_Click);
      this.BuildInstallerToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.BuildInstallerToolStripButton.Image = (Image) Resources.installer;
      this.BuildInstallerToolStripButton.ImageTransparentColor = Color.Magenta;
      this.BuildInstallerToolStripButton.Name = "BuildInstallerToolStripButton";
      this.BuildInstallerToolStripButton.Size = new Size(23, 22);
      this.BuildInstallerToolStripButton.Text = "Build the Installer.";
      this.BuildInstallerToolStripButton.Click += new EventHandler(this.BuildInstallerToolStripButton_Click);
      this.toolStripSeparator7.Name = "toolStripSeparator7";
      this.toolStripSeparator7.Size = new Size(6, 25);
      this.ExploreToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.ExploreToolStripButton.Image = (Image) Resources.explore;
      this.ExploreToolStripButton.ImageTransparentColor = Color.Magenta;
      this.ExploreToolStripButton.Name = "ExploreToolStripButton";
      this.ExploreToolStripButton.Size = new Size(23, 22);
      this.ExploreToolStripButton.Text = "Explore";
      this.ExploreToolStripButton.ToolTipText = "Explore the project folder.";
      this.ExploreToolStripButton.Click += new EventHandler(this.ExploreToolStripButton_Click);
      this.BackupToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.BackupToolStripButton.Image = (Image) Resources.backup;
      this.BackupToolStripButton.ImageTransparentColor = Color.Magenta;
      this.BackupToolStripButton.Name = "BackupToolStripButton";
      this.BackupToolStripButton.Size = new Size(23, 22);
      this.BackupToolStripButton.Text = "Backup";
      this.BackupToolStripButton.ToolTipText = "Backup project settings. (Recomended before edit)";
      this.BackupToolStripButton.MouseDown += new MouseEventHandler(this.BackupToolStripButton_MouseDown);
      this.toolStripSeparator9.Name = "toolStripSeparator9";
      this.toolStripSeparator9.Size = new Size(6, 25);
      this.FacebookToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.FacebookToolStripButton.Image = (Image) Resources.facebook;
      this.FacebookToolStripButton.ImageTransparentColor = Color.Magenta;
      this.FacebookToolStripButton.Name = "FacebookToolStripButton";
      this.FacebookToolStripButton.Size = new Size(23, 22);
      this.FacebookToolStripButton.Text = "Facebook";
      this.FacebookToolStripButton.ToolTipText = "Like on Facebook.";
      this.FacebookToolStripButton.Click += new EventHandler(this.FacebookToolStripButton_Click);
      this.PaypalToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
      this.PaypalToolStripButton.Image = (Image) Resources.paypal;
      this.PaypalToolStripButton.ImageTransparentColor = Color.Magenta;
      this.PaypalToolStripButton.Name = "PaypalToolStripButton";
      this.PaypalToolStripButton.Size = new Size(23, 22);
      this.PaypalToolStripButton.Text = "Paypal";
      this.PaypalToolStripButton.ToolTipText = "Thanks for your generous donation.";
      this.PaypalToolStripButton.Click += new EventHandler(this.donateToolStripMenuItem_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(792, 569);
      this.Controls.Add((Control) this.toolStrip1);
      this.Controls.Add((Control) this.ButtonsPanel);
      this.Controls.Add((Control) this.ControlsPanel);
      this.Controls.Add((Control) this.statusStrip1);
      this.Controls.Add((Control) this.menuStrip1);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.MainMenuStrip = this.menuStrip1;
      this.MinimumSize = new Size(800, 600);
      this.Name = nameof (MainForm);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Tag = (object) "PAF Launcher Editor";
      this.Text = "PAF Launcher Editor";
      this.Load += new EventHandler(this.MainForm_Load);
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.toolStrip1.ResumeLayout(false);
      this.toolStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
