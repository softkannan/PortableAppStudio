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
    public partial class SearchReplaceHelperForm : Form
    {
        public SearchReplaceHelperForm()
        {
            InitializeComponent();
        }

        public void Initialize(string resourceName)
        {
            List<string> multiLineData = new List<string>();

            string filename = PathManager.Init.GetResourcePath(resourceName);
            if (File.Exists(filename))
            {
                var mapList = File.ReadLines(filename).ToArray();
                foreach (var item in mapList)
                {
                    var tempVal = item.Trim();
                    if (!string.IsNullOrWhiteSpace(tempVal))
                    {
                        StringBuilder fullVal = new StringBuilder();
                        var keyValue = tempVal.Split('=');
                        if (keyValue.Length > 0)
                        {
                            fullVal.Append(keyValue[0].Trim());
                        }
                        if (keyValue.Length > 1)
                        {
                            fullVal.Append(" ");
                            fullVal.Append(keyValue[1].Trim());
                        }
                        var tempFullVal = fullVal.ToString();
                        if (!string.IsNullOrWhiteSpace(tempFullVal))
                        {
                            multiLineData.Add(tempFullVal);
                        }

                    }
                }
            }

            filename = PathManager.Init.GetResourcePath("EnvIntellisense.txt");
            if (File.Exists(filename))
            {
                multiLineData.AddRange(File.ReadLines(filename).ToArray());
            }

            txtSearchReplace.Lines = multiLineData.ToArray();
        }

        private void bttnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }
    }
}
