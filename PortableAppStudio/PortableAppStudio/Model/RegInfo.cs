using Microsoft.Win32;
using PortableAppStudio.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Model
{
    public class RegInfo
    {
        public RegInfo(string key, string valueName, string regKind, string regValue)
        {
            this.Value = regValue;
            this.Key = key;
            this.ValueName = valueName;
            this.Kind = regKind;
        }

        public string Key { get; private set; }
        public string Kind { get; private set; }
        public string ValueName { get; private set; }
        public string Value { get; private set; }

        public string RegWriteValue
        {
            get
            {
                return GetValue();
            }
        }

        private string GetValue()
        {
            string regValue = "";
            switch (Kind)
            {
                case "REG_EXPAND_SZ":
                    regValue = string.Format("{0}:{1}", Kind, Value.HexToStr());
                    break;
                case "REG_SZ":
                    regValue = string.Format("{0}", Value);
                    break;
                case "REG_DWORD":
                    uint dwordValue = 0;
                    uint.TryParse(Value, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out dwordValue);
                    regValue = string.Format("{0}:{1}", Kind, dwordValue);
                    break;
                case "REG_QWORD":
                case "REG_BINARY":
                    regValue = string.Format("{0}:{1}", Kind, Value.Replace(",", ""));
                    break;
                default:
                    regValue = "";
                    break;
            }

            return regValue;
        }
    }
}
