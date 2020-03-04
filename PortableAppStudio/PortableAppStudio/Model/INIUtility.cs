using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Model
{
    public static class INIUtility
    {
        public static void Parse(this IINIKeyValuePair pThis, string fullValue)
        {
            if (string.IsNullOrWhiteSpace(fullValue))
            {
                pThis.IniKey = "";
                pThis.IniValue = "";
                return;
            }

            int startIndex = fullValue.IndexOf('=');

            if (startIndex != -1)
            {
                pThis.IniKey = fullValue.Substring(0, startIndex);
                pThis.IniValue = fullValue.Substring(startIndex + 1);
            }
            else
            {
                pThis.IniKey = fullValue;
                pThis.IniValue = "";
            }
        }

        public static string ToFullValue(this IINIKeyValuePair pThis)
        {
            return string.Format("{0}={1}", pThis.IniKey, pThis.IniValue);
        }
    }
}
