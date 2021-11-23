using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Model.LaunchINI
{
    [DisplayName("KillProc")]
    public class KillProcNValue : INIKeyValuePairBase
    {
        [Browsable(true)]
        [TypeConverter(typeof(ExeFileNameListStringConverter))]
        [Category("KillProcN")]
        public string ExeName
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
        public override string KeyDisplayName { get { return "Key"; } }
        [Browsable(false)]
        public override string ValueDisplayName { get { return "ExeName"; } }

        public KillProcNValue(int index, string exeName)
        {
            ExeName = exeName;
            UpdateIndex(index);
        }

        public override void UpdateIndex(int index)
        {
            IniKey = string.Format("KillProc{0}", index);
        }

        public KillProcNValue()
        {
        }
    }
}
