using PortableAppStudio.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppStudio.Dialogs
{
    public partial class DirectoriesCleanupForceForm : INIValueEditForm
    {
        public DirectoriesCleanupForceForm()
        {
            InitializeComponent();
            this.Load += DirectoriesCleanupForceForm_Load;
            bttnOk.Click += BttnOk_Click;

            helpTextBox.Rtf = PathManager.Init.GetHelpData("DirectoriesCleanupForce");
            folderPathTextBox.AutoCompleteCustomSource = Model.InteliSense.Inst.EnvVaraibles;
        }
        private void BttnOk_Click(object sender, EventArgs e)
        {
            FullValue = string.Format("{0}={1}", seqTextBox.Text, folderPathTextBox.Text);
            DialogResult = DialogResult.OK;
            this.Close();
        }
        private void DirectoriesCleanupForceForm_Load(object sender, EventArgs e)
        {
            this.Text = string.Format("Directories Cleanup Force - {0}", this.Title);
            seqTextBox.Enabled = false;
            if (!string.IsNullOrWhiteSpace(FullValue))
            {
                int startIndex = FullValue.IndexOf("=");
                if (startIndex != -1)
                {
                    folderPathTextBox.Text = FullValue.Substring(startIndex + 1);
                    seqTextBox.Text = FullValue.Substring(0, startIndex);
                }
                else
                {
                    seqTextBox.Text = FullValue;
                    folderPathTextBox.Text = "<Target folder Path>";
                }
            }
            else
            {
                folderPathTextBox.Text = "<Target folder Path>"; 
                seqTextBox.Text = Index.ToString();
            }
        }
    }
}
