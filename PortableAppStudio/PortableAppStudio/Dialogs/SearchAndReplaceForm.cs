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
    public partial class SearchAndReplaceForm : Form
    {
        public SearchAndReplaceForm()
        {
            InitializeComponent();

            txtSearch.AutoCompleteCustomSource = Model.InteliSense.Inst.SearchList;
            txtReplace.AutoCompleteCustomSource = Model.InteliSense.Inst.ReplaceList;
        }

        public string Search
        {
            get => txtSearch.Text; 
            set => txtSearch.Text = value;
        }

        public string Replace
        {
            get => txtReplace.Text;
            set => txtReplace.Text = value;
        }

        private void bttnReplace_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }

}
