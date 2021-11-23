using PortableAppStudio.INI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppStudio.Model
{
    public interface ISectionINI
    {
        string SectionName { get; set; }
        void LoadSection(string section, INIFile file);
        string Validate();
        void SaveSection(string section, INIFile file);
    }
}
