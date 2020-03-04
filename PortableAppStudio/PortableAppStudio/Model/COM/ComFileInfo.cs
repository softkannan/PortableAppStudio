using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Model.COM
{
    public class ComFileInfo
    {
        public string name { get; set; }

        public Dictionary<string,ComTypeLibRegInfo> TypeLibInfo { get; set; }

        public Dictionary<string,ComClassRegInfo> ComClassInfo { get; set; }

        public Dictionary<string, ComInterfaceExternalProxyStubRegInfo> InterfaceInfo { get; set; }
       

        public ComFileInfo()
        {
            TypeLibInfo = new Dictionary<string, ComTypeLibRegInfo>(StringComparer.InvariantCultureIgnoreCase);
            ComClassInfo = new Dictionary<string, ComClassRegInfo>(StringComparer.InvariantCultureIgnoreCase);
            InterfaceInfo = new Dictionary<string, ComInterfaceExternalProxyStubRegInfo>(StringComparer.InvariantCultureIgnoreCase);
        }
    }
}
