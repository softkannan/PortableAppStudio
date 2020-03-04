using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Model.LaunchINI
{
    public class LanguageStringsSection : KeyValueSection
    {
        [Browsable(true)]
        [Category("[LanguageStrings]")]
        public string KeyName
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
        [Category("[LanguageStrings]")]
        [TypeConverter(typeof(EnvVariableListStringConverter))]
        public string LangValue
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
        public override string KeyDisplayName { get { return "LangBase"; } }
        [Browsable(false)]
        public override string ValueDisplayName { get { return "LangName"; } }
    }
}
