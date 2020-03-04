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
    public partial class DirectoriesLinkForm : INIValueEditForm
    {
        public DirectoriesLinkForm()
        {
            InitializeComponent();
            this.Load += DirectoriesLinkForm_Load;

            bttnOk.Click += BttnOk_Click;

            helpTextBox.Rtf = PathManager.Init.GetHelpData("DirectoriesLink");
            targetDirTextBox.AutoCompleteCustomSource = Model.InteliSense.Inst.EnvVaraibles;
            srcDirTextBox.AutoCompleteCustomSource = Model.InteliSense.Inst.EnvVaraibles;
        }

        private void BttnOk_Click(object sender, EventArgs e)
        {
            FullValue = string.Format("{0}={1}", srcDirTextBox.Text, targetDirTextBox.Text);
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void DirectoriesLinkForm_Load(object sender, EventArgs e)
        {
            this.Text = string.Format("Directories Link - {0}", this.Title);
            if (!string.IsNullOrWhiteSpace(FullValue))
            {
                int startIndex = FullValue.IndexOf("=");
                if (startIndex != -1)
                {
                    targetDirTextBox.Text = FullValue.Substring(startIndex + 1);
                    srcDirTextBox.Text = FullValue.Substring(0, startIndex);
                }
                else
                {
                    srcDirTextBox.Text = FullValue;
                    targetDirTextBox.Text = "<Target folder Path>";
                }
            }
            else
            {
                targetDirTextBox.Text = "<Target folder Path>";
                srcDirTextBox.Text = "<Folder Name from Data Folder";
            }
        }
    }
}