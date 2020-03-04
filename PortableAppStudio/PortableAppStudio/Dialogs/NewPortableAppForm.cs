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
        private void bttnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            SelectedPath = string.Format("{0}\\{1}Portable", baseFolderTextBox.Text, appNameTextBox.Text.Replace(" ","_"));

            try
            {
                string fullPath = Path.GetFullPath(SelectedPath);

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
            appNameTextBox.Text = "<Enter New App Name>";
            if(string.IsNullOrWhiteSpace(SelectedPath))
            {
                SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            }
            baseFolderTextBox.Text = SelectedPath;
        }
    }
}
