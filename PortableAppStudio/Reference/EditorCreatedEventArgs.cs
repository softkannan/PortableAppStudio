using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppStudio.Model
{
    public class EditorCreatedEventArgs : EventArgs
    {
        public Form EditorForm { get; set; }

        public EditorCreatedEventArgs(Form frm)
        {
            this.EditorForm = frm;
        }
    }
}
