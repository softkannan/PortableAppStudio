using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Model.LaunchINI
{
    [DisplayName("WaitForEXE")]
    public class WaitForEXENValue : INIKeyValuePairBase
    {
        [Browsable(true)]
        [TypeConverter(typeof(ExeFileNameListStringConverter))]
        [Category("WaitForEXEN")]
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

        public WaitForEXENValue(int index, string exeName)
        {
            ExeName = exeName;
            UpdateIndex(index);
        }

        public override void UpdateIndex(int index)
        {
            IniKey = string.Format("WaitForEXE{0}", index);
        }

        public WaitForEXENValue()
        {
        }
    }
}
