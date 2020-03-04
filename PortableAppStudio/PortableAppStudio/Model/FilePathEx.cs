using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PortableAppStudio.Model
{
    public class FilePathEx
    {
        public string RelativePath { get; set; }
        public Regex ShortPath { get; set; }
        public Regex LongPath { get; set; }
    }
}
