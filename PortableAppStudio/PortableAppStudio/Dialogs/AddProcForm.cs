using PortableAppStudio.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppStudio.Dialogs
{
    public partial class AddProcForm : Form
    {
        public AddProcForm()
        {
            InitializeComponent();

            this.Load += AddProcForm_Load;

            AutoCompleteList = null;
            SelectedItems = new List<string>();

            bttnOk.Click += BttnOk_Click;
        }

        public List<string> AutoCompleteList { get; set; }

        private void AddProcForm_Load(object sender, EventArgs e)
        {
            if (AutoCompleteList == null)
            {
                Process[] runningProcesses = Process.GetProcesses();
                var currentSessionID = Process.GetCurrentProcess().SessionId;
                AutoCompleteList = new List<string>();
                foreach (var item in runningProcesses)
                {
                    string procName = item.GetProcessFileName();
                    if (!string.IsNullOrWhiteSpace(procName))
                    {
                        AutoCompleteList.Add(procName);
                    }
                }
            }

            if (AutoCompleteList != null)
            {
                procListBox.Items.AddRange(AutoCompleteList.ToArray());
            }
        }

        private void BttnOk_Click(object sender, EventArgs e)
        {
            if (procListBox.SelectedItems != null && procListBox.SelectedItems.Count > 0)
            {
                foreach (var item in procListBox.SelectedItems)
                {
                    SelectedItems.Add(item as string);
                }
            }

            DialogResult = DialogResult.OK;
            this.Close();
        }

        public List<string> SelectedItems { get; private set; }
    }
}
