using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Model.COM
{
    public class ComClassRegInfo
    {
        public string description { get; set; }
        public string clsid { get; set; }
        public string threadingModel { get; set; }
        public string tlbid { get; set; }
        public string progid { get; set; }
        public string name { get; set; }

        public ComClassRegInfo()
        {
            clsid = string.Empty;
            threadingModel = string.Empty;
            progid = string.Empty;
            tlbid = string.Empty;
            name = string.Empty;
        }

        public void AddProgId(string progID)
        {
            if (!string.IsNullOrWhiteSpace(progID))
            {
                if(string.IsNullOrWhiteSpace(progid))
                {
                    progid = progID;
                }
                else if(string.Compare(progid,progID, true) > 0)
                {
                    progid = progID;
                }
            }
        }
    }
}
