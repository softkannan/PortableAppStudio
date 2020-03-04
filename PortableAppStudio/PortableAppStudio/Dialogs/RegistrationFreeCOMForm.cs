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
    public partial class RegistrationFreeCOMForm : INIValueEditForm
    {
        public RegistrationFreeCOMForm()
        {
            InitializeComponent();
            this.Load += RegistrationFreeCOMForm_Load;
            bttnOk.Click += BttnOk_Click;

            helpTextBox.Rtf = PathManager.Init.GetHelpData("RegistrationFreeCOM");

            comFileName.AutoCompleteCustomSource = Model.InteliSense.Inst.EnvVaraibles;
        }
        private void BttnOk_Click(object sender, EventArgs e)
        {
            FullValue = string.Format("{0}={1}", seqTextBox.Text, comFileName.Text);
            DialogResult = DialogResult.OK;
            this.Close();
        }
        private void RegistrationFreeCOMForm_Load(object sender, EventArgs e)
        {
            this.Text = string.Format("RegistrationFreeCOM - {0}", this.Title);
            seqTextBox.Enabled = false;
            if (!string.IsNullOrWhiteSpace(FullValue))
            {
                int startIndex = FullValue.IndexOf("=");
                if (startIndex != -1)
                {
                    comFileName.Text = FullValue.Substring(startIndex + 1);
                    seqTextBox.Text = FullValue.Substring(0, startIndex);
                }
                else
                {
                    seqTextBox.Text = FullValue;
                    comFileName.Text = "<COM File Name>";
                }
            }
            else
            {
                comFileName.Text = "<COM File Name>";
                seqTextBox.Enabled = false;
                seqTextBox.Text = Index.ToString();
            }
        }
    }
}
