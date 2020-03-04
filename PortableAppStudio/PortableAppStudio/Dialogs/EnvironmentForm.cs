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
    public partial class EnvironmentForm : INIValueEditForm
    {
        public EnvironmentForm()
        {
            InitializeComponent();
            this.Load += EnvironmentForm_Load;

            bttnOk.Click += BttnOk_Click;

            helpTextBox.Rtf = PathManager.Init.GetHelpData("Environment");

            envVarTextBox.AutoCompleteCustomSource = Model.InteliSense.Inst.EnvVaraibles;
            envValueTextBox.AutoCompleteCustomSource = Model.InteliSense.Inst.EnvVaraibles;
        }

        private void BttnOk_Click(object sender, EventArgs e)
        {
            FullValue = string.Format("{0}={1}", envVarTextBox.Text, envValueTextBox.Text);
            DialogResult = DialogResult.OK;

            this.Close();
        }

        private void EnvironmentForm_Load(object sender, EventArgs e)
        {
            this.Text = string.Format("Environment - {0}", this.Title);
            if (!string.IsNullOrWhiteSpace(FullValue))
            {
                int startIndex = FullValue.IndexOf("=");
                if (startIndex != -1)
                {
                    envVarTextBox.Text = FullValue.Substring(0, startIndex);
                    envValueTextBox.Text = FullValue.Substring(startIndex + 1);
                }
                else
                {
                    envVarTextBox.Text = FullValue;
                    envValueTextBox.Text = "<New Env Value>";
                }
            }
            else
            {
                envValueTextBox.Text = "<New Env Var Name>";
                envVarTextBox.Text = "<New Env Value>";
            }
        }
    }
}
