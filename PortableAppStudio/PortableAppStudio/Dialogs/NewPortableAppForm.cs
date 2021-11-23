using PortableAppStudio.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppStudio.Dialogs
{
    public partial class NewPortableAppForm : Form
    {
        public NewPortableAppForm()
        {
            InitializeComponent();
            this.SelectedPath = string.Empty;
        }
        public string SelectedPath { get; set; }
        public string ImportFolderName { get; set; }
        private void bttnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            var appName = "";
            if(string.IsNullOrWhiteSpace(appNameTextBox.Text))
            {
                ErrorLog.Inst.ShowError("Invalid AppName, Portable App did not created");
                return;
            }

            appName = appNameTextBox.Text.Trim();
            if(string.IsNullOrWhiteSpace(appName))
            {
                ErrorLog.Inst.ShowError("Invalid AppName, Portable App did not created");
                return;
            }

            SelectedPath = string.Format("{0}\\{1}", baseFolderTextBox.Text, appName.Replace(" ","_"));

            try
            {
                string fullPath = Path.GetFullPath(SelectedPath);
                if(Directory.Exists(fullPath))
                {
                    SelectedPath = string.Format("{0}\\{1}Portable", baseFolderTextBox.Text, appName.Replace(" ", "_"));
                    fullPath = Path.GetFullPath(SelectedPath);
                }

                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(fullPath);
                }

                this.DialogResult = DialogResult.OK;
            }
            catch (Exception)
            {
                ErrorLog.Inst.LogError("Invalid folder name : {0}", SelectedPath);
            }

            this.Close();
        }

        private void bttnBrowse_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = baseFolderTextBox.Text;
            if (folderBrowserDialog.ShowFolderBrowser(this) == DialogResult.OK)
            {
                baseFolderTextBox.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void NewPortableApp_Load(object sender, EventArgs e)
        {
            var folderName = ImportFolderName?.Trim();
            if (string.IsNullOrWhiteSpace(folderName))
            {
                appNameTextBox.Text = "<Enter New App Name>";
            }
            else
            {
                appNameTextBox.Text = string.Format("{0}Portable", Path.GetFileName(folderName).Replace("_Fixed", "",StringComparison.OrdinalIgnoreCase));
            }
            if(string.IsNullOrWhiteSpace(SelectedPath))
            {
                SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            }
            baseFolderTextBox.Text = SelectedPath;
        }
    }
}
