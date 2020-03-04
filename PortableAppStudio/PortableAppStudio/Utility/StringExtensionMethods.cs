using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace PortableAppStudio.Utility
{
    public static class StringExtensionMethods
    {
        [DllImport("kernel32.dll",SetLastError = true, CharSet = CharSet.Auto)]
        static extern uint GetFullPathName(string lpFileName, uint nBufferLength, [Out] StringBuilder lpBuffer, out StringBuilder lpFilePart);
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern uint GetFullPathName(string lpFileName, uint nBufferLength, [Out] StringBuilder lpBuffer, IntPtr lpFilePart);

        public enum CodingType
        {
            Unicode,
            ASCII
        }

        public static string GetAbsolutePath(this string pThis, bool shortPath)
        {
            var expandedValue = shortPath ? Environment.ExpandEnvironmentVariables(pThis) : string.Format(@"\\?\{0}", Environment.ExpandEnvironmentVariables(pThis));
            int bufsz = 1;
            string sRel = expandedValue;
            StringBuilder sbFull = new StringBuilder(bufsz);
            uint u = GetFullPathName(sRel, (uint)bufsz, sbFull, IntPtr.Zero);
            if (u > bufsz)
            {
                bufsz = (int)u + 10;
                sbFull = new StringBuilder(bufsz);
                u = GetFullPathName(sRel, (uint)bufsz, sbFull, IntPtr.Zero);    
            }
            return sbFull.ToString();
        }

        public static string RelativeToFileName(this string pThis)
        {
            int lastIndex = pThis.LastIndexOf("\\");

            if (lastIndex == -1)
            {
                return pThis;
            }
            else
            {
                return pThis.Substring(lastIndex + 1);
            }
        }
        public static string RemoveEnd(this string pThis, char endChar)
        {
            if (String.IsNullOrEmpty(pThis))
            {
                return pThis;
            }
            int length = pThis.Length;
            if (length > 0 && pThis[length - 1] == endChar)
            {
                pThis = pThis.Substring(0, length - 1);
            }
            return pThis;
        }
        public static string RemoveStartEnd(this string pThis, char trimBothEnd = '\"')
        {
            return RemoveStartEnd(pThis, trimBothEnd, trimBothEnd);
        }
        public static string RemoveStartEnd(this string pThis, char startChar, char endChar)
        {
            if (String.IsNullOrEmpty(pThis))
            {
                return pThis;
            }
            int length = pThis.Length;
            if (length > 1 && pThis[0] == startChar && pThis[length - 1] == endChar)
            {
                pThis = pThis.Substring(1, length - 2);
            }
            return pThis;
        }
        public static string EscapeCtrl(this string pThis)
        {
            if (string.IsNullOrWhiteSpace(pThis))
            {
                return "";
            }
            var literal = new StringBuilder(pThis.Length * 2);
            for (int index = 0; index < pThis.Length; index++)
            {
                switch (pThis[index])
                {
                    //case '\'': literal.Append(@"\'"); break;
                    //case '\"': literal.Append("\\\""); break;
                    //case '\\': literal.Append(@"\\"); break;
                    case '\0': literal.Append(@"\0"); break;
                    case '\a': literal.Append(@"\a"); break;
                    case '\b': literal.Append(@"\b"); break;
                    case '\f': literal.Append(@"\f"); break;
                    case '\n': literal.Append(@"\n"); break;
                    case '\r': literal.Append(@"\r"); break;
                    case '\t': literal.Append(@"\t"); break;
                    case '\v': literal.Append(@"\v"); break;
                    default:
                        literal.Append(pThis[index]);
                        break;
                }
            }
            return literal.ToString();
        }
        
        public static string ToLiteral(this string pThis)
        {
            if(string.IsNullOrWhiteSpace(pThis))
            {
                return "";
            }
            var literal = new StringBuilder(pThis.Length * 2);
            for(int index=0; index < pThis.Length;index++)
            {
                switch (pThis[index])
                {
                    case '\'': literal.Append(@"\'"); break;
                    case '\"': literal.Append("\\\""); break;
                    case '\\': literal.Append(@"\\"); break;
                    case '\0': literal.Append(@"\0"); break;
                    case '\a': literal.Append(@"\a"); break;
                    case '\b': literal.Append(@"\b"); break;
                    case '\f': literal.Append(@"\f"); break;
                    case '\n': literal.Append(@"\n"); break;
                    case '\r': literal.Append(@"\r"); break;
                    case '\t': literal.Append(@"\t"); break;
                    case '\v': literal.Append(@"\v"); break;
                    default:
                        literal.Append(pThis[index]);
                        break;
                }
            }
            return literal.ToString();
        }
        public static string HexToDec(this string hexString, string seperator = ",")
        {
            if (hexString == null || hexString.Length == 0)
            {
                return "";
            }

            uint retVal = 0;

            uint.TryParse(hexString.Replace(seperator, ""), System.Globalization.NumberStyles.HexNumber,
                System.Globalization.CultureInfo.InvariantCulture, out retVal);

            return retVal.ToString();
        }
        public static string StrToHex(this string normalString, char seperator = ',', CodingType encodingType = CodingType.Unicode)
        {
            string retVal = "";
            if (string.IsNullOrWhiteSpace(normalString))
            {
                return retVal;
            }

            byte[] bytes;
            if (encodingType == CodingType.Unicode)
            {
                bytes = Encoding.Unicode.GetBytes(normalString);
            }
            else
            {
                bytes = Encoding.ASCII.GetBytes(normalString);
            }

            retVal = BitConverter.ToString(bytes).Replace('-', seperator);

            return retVal;
        }
        public static string HexToStr(this string hexCodedString, char seperator = ',', CodingType encodingType = CodingType.Unicode)
        {
            string retVal = "";
            if (hexCodedString == null || hexCodedString.Length == 0)
            {
                return retVal;
            }
            List<byte> byteStream = new List<byte>();
            var tempByteVals = hexCodedString.Split(seperator);

            if (tempByteVals != null && tempByteVals.Length > 0)
            {
                for (int index = 0; index < tempByteVals.Length; index++)
                {
                    byteStream.Add(Convert.ToByte(tempByteVals[index], 16));
                }
            }

            if (encodingType == CodingType.Unicode)
            {
                retVal = Encoding.Unicode.GetString(byteStream.ToArray());
            }
            else
            {
                retVal = Encoding.UTF8.GetString(byteStream.ToArray());
            }
            return retVal;
        }
        public static bool HasLauncher(this string str)
        {
            string path = string.Format("{0}\\App\\AppInfo\\Launcher", (object)str);
            if (!Directory.Exists(path) || !Directory.EnumerateFiles(path).Any<string>())
                return false;
            IEnumerable<string> source = Directory.EnumerateFiles(path);
            Func<string, bool> func = (Func<string, bool>)(f => f.Contains("portable", StringComparison.OrdinalIgnoreCase));
            Func<string, bool> predicate = (item) => true;
            return File.Exists(source.First<string>(predicate));
        }

        public static bool HasAppInfo(this string str)
        {
            return File.Exists(string.Format("{0}\\App\\AppInfo\\AppInfo.ini", (object)str));
        }

        public static bool Contains(this string original, string value, StringComparison comparisionType = StringComparison.OrdinalIgnoreCase)
        {
            return original.IndexOf(value, comparisionType) >= 0;
        }

        public static bool ContainsCaseInsensitive(this string original, string value, StringComparison comparisionType = StringComparison.OrdinalIgnoreCase)
        {
            return original.IndexOf(value, comparisionType) >= 0;
        }

        public static string ReplaceCaseInsensitive(this string original, string pattern, string replacement)
        {
            return new Regex(pattern, RegexOptions.IgnoreCase).Replace(original, replacement);
        }

        public static string Replace(string originalString, string oldValue, string newValue)
        {
            return new Regex(oldValue, RegexOptions.IgnoreCase | RegexOptions.Multiline).Replace(originalString, newValue);
        }

        private static Dictionary<string, string> thinAppReplace = null;

        private static Dictionary<string,string> ThinApp
        {
            get
            {
                if(thinAppReplace == null)
                {
                    thinAppReplace = new Dictionary<string, string>();
                    thinAppReplace.Add("#2300", "");
                    thinAppReplace.Add("#230d", "\r");
                    thinAppReplace.Add("#230a", "\n");
                    thinAppReplace.Add("#2309", "\t");
                    thinAppReplace.Add("#2307", "\a");
                    thinAppReplace.Add("#2308", "\b");
                    thinAppReplace.Add("#230c", "\f");
                    thinAppReplace.Add("#230b", "\v");
                }
                return thinAppReplace;
            }
        }

        public static string ThinAppToLiteral(this string pThis)
        {
            if(string.IsNullOrWhiteSpace(pThis))
            {
                return "";
            }
            if(pThis.Contains("#"))
            {
                var tempResult = pThis;
                foreach(var item in ThinApp)
                {
                    var tempStr = tempResult.Replace(item.Key, item.Value);
                    tempResult = tempStr;
                }
                return tempResult;
            }
            else
            {
                return pThis;
            }
        }

        public static string Replace(this string str, string oldValue, string newValue, StringComparison comparison)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int startIndex1 = 0;
            int startIndex2;
            for (int index = str.IndexOf(oldValue, comparison); index != -1; index = str.IndexOf(oldValue, startIndex2, comparison))
            {
                stringBuilder.Append(str.Substring(startIndex1, index - startIndex1));
                stringBuilder.Append(newValue);
                startIndex2 = index + oldValue.Length;
                startIndex1 = startIndex2;
            }
            stringBuilder.Append(str.Substring(startIndex1));
            return stringBuilder.ToString();
        }
    }
}
