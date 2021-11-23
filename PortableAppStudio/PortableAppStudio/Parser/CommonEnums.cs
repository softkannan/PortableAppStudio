using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Parser
{
    public enum Action
    {
        None,
        Added,
        Deleted,
        Modified
    }

    public enum SectionType
    {
        None,
        RegKeys,
        RegValues,
        Files,
        Folders
    }

    public class RegSourceType
    {
        public const string RegFile = "RegFile";
        public const string AppV = "AppV";
        public const string RegShot = "RegShot";
        public const string ThinApp = "ThinApp";
        public const string X_RegShot = "X_RegShot";
    }
}
