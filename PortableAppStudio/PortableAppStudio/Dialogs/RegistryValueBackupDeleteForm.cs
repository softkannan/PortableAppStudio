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
    public partial class RegistryValueBackupDeleteForm : INIValueEditForm
    {
        public RegistryValueBackupDeleteForm()
        {
            InitializeComponent();
            this.Load += RegistryValueBackupDeleteForm_Load;
            bttnOk.Click += BttnOk_Click;

            helpTextBox.Rtf = PathManager.Init.GetHelpData("RegistryValueBackupDelete");
        }
        private void BttnOk_Click(object sender, EventArgs e)
        {
            FullValue = string.Format("{0}={1}", seqTextBox.Text, regValueTextBox.Text);
            DialogResult = DialogResult.OK;
            this.Close();
        }
        private void RegistryValueBackupDeleteForm_Load(object sender, EventArgs e)
        {
            this.Text = string.Format("Registry Value Backup Delete - {0}", this.Title);
            seqTextBox.Enabled = false;
            if (!string.IsNullOrWhiteSpace(FullValue))
            {
                int startIndex = FullValue.IndexOf("=");
                if (startIndex != -1)
                {
                    regValueTextBox.Text = FullValue.Substring(startIndex + 1);
                    seqTextBox.Text = FullValue.Substring(0, startIndex);
                }
                else
                {
                    seqTextBox.Text = FullValue;
                    regValueTextBox.Text = "<New Reg Key\\Reg Value Name>";
                }
            }
            else
            {
                regValueTextBox.Text = "<New Reg Key\\Reg Value Name>";
                seqTextBox.Enabled = false;
                seqTextBox.Text = Index.ToString();
            }
        }
    }
}
