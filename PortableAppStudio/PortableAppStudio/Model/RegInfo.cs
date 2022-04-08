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
        public RegInfo(SortedDictionary<string, string> searchReplaceList, string parserType,string key, string valueName, string regKind, string regValue)
        {
            this.Value = regValue;
            this.Key = key;
            this.ValueName = valueName;
            this.Kind = regKind;
            this.ParserType = parserType;
            this.SearchReplaceList = searchReplaceList;
        }

        public string Key { get; private set; }
        public string Kind { get; private set; }
        public string ValueName { get; private set; }
        public string Value { get; private set; }
        public string ParserType { get; private set; }

        public SortedDictionary<string, string> SearchReplaceList { get; private set; }

        public string RegWriteValue
        {
            get
            {
                return GetValue();
            }
        }

        public string RegStrValue
        {
            get
            {
                return GetStrValue();
            }
        }

        public void SearchAndReplace(string search,string replace)
        {
            if(Kind == "REG_SZ")
            {
                string tempVal = Value;
                Value = tempVal.Replace(search, replace,StringComparison.OrdinalIgnoreCase);
            }
        }

        private string GetStrValue()
        {
            string regValue = "";
            switch (Kind)
            {
                case "REG_EXPAND_SZ":
                    regValue = Value.HexToStr();
                    break;
                case "REG_SZ":
                    regValue = Value;
                    break;
                case "REG_DWORD":
                    uint dwordValue = 0;
                    uint.TryParse(Value, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out dwordValue);
                    regValue = string.Format("{0}", dwordValue);
                    break;
                case "REG_QWORD":
                case "REG_BINARY":
                    regValue = string.Format("{0}", Value.Replace(",", ""));
                    break;
                default:
                    regValue = "";
                    break;
            }

            return regValue;
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
