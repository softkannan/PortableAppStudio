using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppStudio.Utility
{
    public static class MiscExtensions
    {
        public static bool HasMethod(this object pThis, string methodName)
        {
            var type = pThis.GetType();
            return type.GetMethod(methodName) != null;
        }

        public static bool HasProperty(this object pThis, string propertyName)
        {
            var type = pThis.GetType();
            return type.GetProperty(propertyName) != null;
        }

        public static object InvokeMethod(this object pThis, string methodName, params object[] args)
        {
            object retVal = null;
            Type thisType = pThis.GetType();
            MethodInfo theMethod = thisType.GetMethod(methodName);
            if (theMethod != null)
            {
                retVal = theMethod.Invoke(pThis, args);
            }
            return retVal;
        }

        public static void AddList(this ComboBox pThis, AutoCompleteStringCollection listOfItems)
        {

            if (listOfItems != null && pThis != null)
            {
                for (int index = 0; index < listOfItems.Count; index++)
                {
                    pThis.Items.Add(listOfItems[index]);
                }
            }
        }

        public static void AddList(this ComboBox pThis, List<string> listOfItems)
        {

            if (listOfItems != null && pThis != null)
            {
                for (int index = 0; index < listOfItems.Count; index++)
                {
                    pThis.Items.Add(listOfItems[index]);
                }
            }
        }

        public static AutoCompleteStringCollection ToAutoCompleteCustomSource(this List<string> pThis)
        {
            AutoCompleteStringCollection retVal = new AutoCompleteStringCollection();

            if(pThis != null)
            {
                for(int index =0; index < pThis.Count; index++)
                {
                    retVal.Add(pThis[index]);
                }
            }

            return retVal;
        }
        public static string GetFilePath(this List<string> allFiles, string fileName)
        {
            string retVal = "";
            foreach (var item in allFiles)
            {
                if (item.EndsWith(fileName,StringComparison.InvariantCultureIgnoreCase))
                {
                    retVal = item;
                    break;
                }
            }
            return retVal;
        }

        public static Model.EnvironmentInfo TryGetEnvironment(this Dictionary<string, Model.EnvironmentInfo> pThis, string displayName)
        {
            Model.EnvironmentInfo retVal = null;

            foreach(var item in pThis)
            {
                if(string.Equals(item.Value.DisplayName,displayName,StringComparison.OrdinalIgnoreCase))
                {
                    retVal = item.Value;
                    break;
                }
            }

            return retVal;
        }
    }
}
