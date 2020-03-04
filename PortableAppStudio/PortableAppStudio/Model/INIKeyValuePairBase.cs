using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppStudio.Model
{
    public class INIKeyValuePairBase : IINIKeyValuePair
    {
        public INIKeyValuePairBase()
        {
            IniValue = "<NewValue>";
        }
        [Browsable(false)]
        public virtual string IniKey { get; set; }
        [Browsable(false)]
        public virtual string IniValue { get; set; }

        [Browsable(false)]
        public virtual bool IsRemoved { get; set; }

        [Browsable(false)]
        public virtual string KeyDisplayName => throw new NotImplementedException();

        [Browsable(false)]
        public virtual string ValueDisplayName => throw new NotImplementedException();

        [Browsable(false)]
        public string FullValue
        {
            get
            {
                return string.Format("{0}={1}", this.IniKey, this.IniValue);
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    IniKey = "";
                    IniValue = "";
                    return;
                }

                int startIndex = value.IndexOf('=');

                if (startIndex != -1)
                {
                    IniKey = value.Substring(0, startIndex);
                    IniValue = value.Substring(startIndex + 1);
                }
                else
                {
                    IniKey = value;
                    IniValue = "";
                }

            }
        }

        public virtual void UpdateIndex(int index)
        {
            IniKey = index.ToString();
        }

        public string Validate()
        {
            return "";
        }

        public override string ToString()
        {
            return FullValue;
        }
    }
}
