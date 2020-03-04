using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Model
{
    public class NewItemCreatedEventArgs : EventArgs
    {
        public string ItemBaseName { get; set; }

        public NewItemCreatedEventArgs(string name)
        {
            this.ItemBaseName = name;
        }
    }
}
