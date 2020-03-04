using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Model
{
    public class EnvironmentInfo
    {
        public string VarName { get; set; }
        public string Tips { get; set; }
        public string RelativePath { get; set; }
        public string ShortPath { get; set; }
        public string LongPath { get; set; }
        public string DisplayName { get; set; }
    }
}
