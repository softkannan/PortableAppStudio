using PortableAppStudio.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppStudio.Model
{
    public class InteliSense
    {
        private static InteliSense _inst = new InteliSense();

        public static InteliSense Inst
        {
            get
            {
                return _inst;
            }
        }

        InteliSense()
        {
            EnvVaraibles = new AutoCompleteStringCollection();

            string filename = PathManager.Init.GetResourcePath("EnvIntellisense.txt");
            if (File.Exists(filename))
            {
                EnvVaraibles.AddRange(File.ReadLines(filename).ToArray());
            }

        }

        public void UpdateEnvironmentList()
        {
            string filename = PathManager.Init.GetResourcePath("EnvIntellisense.txt");
            if (File.Exists(filename))
            {
                var tempVal = File.ReadLines(filename).ToArray();
                SearchList.AddRange(tempVal);
                ReplaceList.AddRange(tempVal);
            }
        }

        public SortedDictionary<string, string> UpdateSearchReplaceList(string resourceName)
        {
            SearchList = new AutoCompleteStringCollection();
            ReplaceList = new AutoCompleteStringCollection();
            var retVal = new SortedDictionary<string, string>(new DescendingComparer<string>());

            string filename = PathManager.Init.GetResourcePath(resourceName);
            if (File.Exists(filename))
            {
                var mapList = File.ReadLines(filename).ToArray();
                foreach(var item in mapList)
                {
                    var tempVal = item.Trim();
                    if(!string.IsNullOrWhiteSpace(tempVal))
                    {
                        var keyValue = tempVal.Split('=');
                        if(keyValue.Length > 0)
                        {
                            SearchList.Add(keyValue[0].Trim());
                        }
                        if(keyValue.Length > 1)
                        {
                            ReplaceList.Add(keyValue[1].Trim());
                            retVal[keyValue[0].Trim()] = keyValue[1].Trim();
                        }
                    }
                }
            }
            
            SearchReplaceMap = retVal;
            return retVal;
        }

        public SortedDictionary<string,string> SearchReplaceMap { get; private set; }

        public AutoCompleteStringCollection SearchList { get; private set; }

        public AutoCompleteStringCollection ReplaceList { get; private set; }

        public AutoCompleteStringCollection EnvVaraibles { get; private set; }
    }
}
