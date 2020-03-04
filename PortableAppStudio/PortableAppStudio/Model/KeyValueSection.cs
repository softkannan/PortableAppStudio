using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PortableAppStudio.INI;

namespace PortableAppStudio.Model
{
    public class KeyValueSection : INIKeyValuePairBase
    {
        public KeyValueSection()
        {
            this.IniValue = "<New Value>";
        }
    }
}
