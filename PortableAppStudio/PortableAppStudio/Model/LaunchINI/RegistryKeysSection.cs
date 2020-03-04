using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Model.LaunchINI
{
    public class RegistryKeysSection : KeyValueSection
    {
        [Browsable(true)]
        [Category("[RegistryKeys]")]
        public string RegFileName
        {
            get
            {
                return this.IniKey;
            }
            set
            {
                this.IniKey = value;
            }
        }
        [Browsable(true)]
        [Category("[RegistryKeys]")]
        public string RegKey
        {
            get
            {
                return this.IniValue;
            }
            set
            {
                this.IniValue = value;
            }
        }

        [Browsable(false)]
        public override string KeyDisplayName { get { return "Reg File"; } }
        [Browsable(false)]
        public override string ValueDisplayName { get { return "RegKey"; } }
    }
}
