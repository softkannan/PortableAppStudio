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
}
