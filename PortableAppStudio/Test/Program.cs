using PortableAppStudio.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        public enum CodingType
        {
            Unicode,
            ASCII
        }
        static void Main(string[] args)
        {

            string reallyLongDirectory = @"\\?\C:\Test\abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ\abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ\abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            reallyLongDirectory = reallyLongDirectory + @"\abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ\abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ\abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            reallyLongDirectory = reallyLongDirectory + @"\abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ\abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ\abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ\";

            //Console.WriteLine($"Creating a directory that is {reallyLongDirectory.Length} characters long");
            Directory.CreateDirectory(reallyLongDirectory);

            //var temp = new ProgressDialog();

            //temp.ShowDialog();

            //string byteCodedString1 = "4a,00,69,00,6c,00,6c,00,20,00,4b,00,61,00,6e,00,6e,00,61,00,6e,00,00,00,00,00";
            //string byteCodedString2 = "47,00,3a,00,5c,00,63,00,73,00,68,00,61,00,72,00,70,00,5c,00,50,00,6f,00,72,00,74,00,61,00,62,00,6c,00,65,00,41,00,70,00,70,00,53,00,74,00,75,00,64,00,69,00,6f,00,5c,00,54,00,65,00,73,00,74,00,69,00,6e,00,67,00,44,00,61,00,74,00,61,00,5c,00,53,00,75,00,6d,00,61,00,74,00,72,00,61,00,50,00,44,00,46,00,50,00,6f,00,72,00,74,00,61,00,62,00,6c,00,65,00,5c,00,41,00,70,00,70,00,5c,00,72,00,65,00,67,00,65,00,78,00,70,00,61,00,6e,00,64,00,5c,00,72,00,65,00,67,00,65,00,78,00,70,00,61,00,6e,00,64,00,65,00,78,00,2e,00,74,00,78,00,74,00,00,00";

            //var tempVal = HexCodedStringToString(byteCodedString1);
            //Console.WriteLine(tempVal);

            //tempVal = HexCodedStringToString(byteCodedString2);
            //Console.WriteLine(tempVal);

            //tempVal = HexToDec("ffffffff");
            //Console.WriteLine(tempVal);
        }

        public static string HexToDec(string hexString)
        {
            return uint.Parse(hexString,System.Globalization.NumberStyles.HexNumber).ToString();
        }

        public static string HexCodedStringToString(string hexCodedString, char seperator = ',', CodingType encodingType = CodingType.Unicode)
        {
            string retVal = "";
            List<byte> byteStream = new List<byte>();
            var tempByteVals = hexCodedString.Split(seperator);

            if(tempByteVals != null && tempByteVals.Length > 0)
            {
                for(int index=0;index < tempByteVals.Length;index++)
                {
                    byteStream.Add(Convert.ToByte(tempByteVals[index], 16));
                }
            }

            if(encodingType == CodingType.Unicode)
            {
                retVal = Encoding.Unicode.GetString(byteStream.ToArray());
            }
            else
            {
                retVal = Encoding.ASCII.GetString(byteStream.ToArray());
            }

            return retVal.Replace("\0", string.Empty);
        }

    }
}
