using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Model.LaunchINI
{
    public class EnvironmentSection : KeyValueSection
    {
        [Browsable(true)]
        [Category("[Environment]")]
        public string VarName
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

        [TypeConverter(typeof(EnvVariableListStringConverter))]
        [Browsable(true)]
        [Category("[Environment]")]
        public string VarValue
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
        public override string KeyDisplayName { get { return "EnvVar Name"; } }
        [Browsable(false)]
        public override string ValueDisplayName { get { return "Value"; } }

        public override void UpdateIndex(int index)
        {
            if(string.IsNullOrWhiteSpace(IniKey))
            {
                IniKey = string.Format("EnvVar{0}", index);
            }
        }
    }
}
