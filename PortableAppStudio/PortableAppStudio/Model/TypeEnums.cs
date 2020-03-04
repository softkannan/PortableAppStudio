using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Model
{
    public delegate void IniValueChangedEventHandler(object s, EventArgs e);
    public enum NameServices
    {
        None,
        Automatic,
        NameProvider,
    }
}
