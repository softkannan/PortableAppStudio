using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Utility
{
    public static class TypeInfoExtensionMethods
    {
        public static bool ContainsType(this CustomAttributeData pThis,Type typeInfo)
        {
            bool retVal = false;
            //foreach(var item in pThis.A)
            //{
            //    if(item.AttributeType == typeInfo)
            //    {
            //        retVal = true;
            //        break;
            //    }
            //}
            return retVal;
        }
    }
}
