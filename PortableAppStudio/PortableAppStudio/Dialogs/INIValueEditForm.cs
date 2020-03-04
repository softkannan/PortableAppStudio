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
    public partial class INIValueEditForm : Form
    {
        public INIValueEditForm()
        {
            InitializeComponent();
            Index = -1;
        }

        public string Title { get; set; }

        public int Index { get; set; }

        public string ValueName { get; set; }
        public string Value { get; set; }
        public string FullValue { get; set; }
    }
}
