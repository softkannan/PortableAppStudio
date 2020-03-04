using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Utility
{
    public class TypeLib
    {
        [DllImport("oleaut32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        static extern int LoadTypeLib(string fileName, out ITypeLib typeLib);

        public static ITypeLib GetTypeLib(string fileName)
        {
            ITypeLib retVal = null;
            LoadTypeLib(fileName, out retVal);
            return retVal;
        }
    }
}
