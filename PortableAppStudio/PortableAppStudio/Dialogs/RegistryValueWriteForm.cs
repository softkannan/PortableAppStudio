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
    public partial class RegistryValueWriteForm : INIValueEditForm
    {
        public RegistryValueWriteForm() : base()
        {
            InitializeComponent();
            this.Load += RegistryValueWriteForm_Load;

            bttnOk.Click += BttnOk_Click;

            helpTextBox.Rtf = PathManager.Init.GetHelpData("RegistryValueWrite");
        }

        private void BttnOk_Click(object sender, EventArgs e)
        {
            FullValue = string.Format("{0}={1}", regKeyTextBox.Text, regValueTextBox.Text);
            DialogResult = DialogResult.OK;

            this.Close();
        }

        private void RegistryValueWriteForm_Load(object sender, EventArgs e)
        {
            this.Text = string.Format("Registry Value Write - {0}", this.Title);
            if (!string.IsNullOrWhiteSpace(FullValue))
            {
                int startIndex = FullValue.IndexOf("=");
                if (startIndex != -1)
                {
                    regKeyTextBox.Text = FullValue.Substring(0, startIndex);
                    regValueTextBox.Text = FullValue.Substring(startIndex + 1);
                }
                else
                {
                    regKeyTextBox.Text = FullValue;
                    regValueTextBox.Text = "<Reg Type:New Reg Value>";
                }
            }
            else
            {
                regKeyTextBox.Text = "<New Reg Key\\Reg Value Name>";
                regValueTextBox.Text = "<Reg Type:New Reg Value>";
            }

        }
    }
}
