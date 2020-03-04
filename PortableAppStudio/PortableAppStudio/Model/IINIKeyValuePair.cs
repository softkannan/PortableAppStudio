using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppStudio.Model
{
    public interface IINIKeyValuePair
    {
        string Validate();

        void UpdateIndex(int index);

        string KeyDisplayName { get; }

        string IniKey { get; set; }

        string ValueDisplayName { get; }
        string IniValue { get; set; }

        bool IsRemoved { get; set; }
        string FullValue { get; set; }
    }
}
