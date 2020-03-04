using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Model.COM
{
    public class ComTypeLibRegInfo
    {
        public string tlbid { get; set; }
        public string version { get; set; }
        public string helpdir { get; set; }
        public string flags { get; set; }
        public string name { get; set; }

        public ComTypeLibRegInfo()
        {
            version = "0";
            helpdir = "";
            flags = "0";
            name = "";
        }
    }
}
