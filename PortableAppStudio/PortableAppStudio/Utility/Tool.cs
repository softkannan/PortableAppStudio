using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Utility
{
    public class Tool
    {
        private string _path = string.Empty;
        public string Path { get => _path; set => _path = value.ResolveFullPath(); }
        public string Args { get; set; }
    }
}
