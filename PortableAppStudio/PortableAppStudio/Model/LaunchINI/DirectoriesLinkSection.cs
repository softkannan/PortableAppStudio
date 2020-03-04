using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Model.LaunchINI
{
    public class DirectoriesLinkSection : KeyValueSection
    {
        [Browsable(true)]
        [Category("[DirectoriesLink]")]
        public string FolderName
        {
            get
            {
                return this.IniKey;
            }
            set
            {
                this.IniKey = value;
            }
        }
        [Browsable(true)]
        [Category("[DirectoriesLink]")]
        public string FolderPath
        {
            get
            {
                return this.IniValue;
            }
            set
            {
                this.IniValue = value;
            }
        }

        [Browsable(false)]
        public override string KeyDisplayName { get { return "S.No"; } }
        [Browsable(false)]
        public override string ValueDisplayName { get { return "DirPath"; } }
    }
}
