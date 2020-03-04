using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Model.LaunchINI
{
    public class FilesMoveSection : KeyValueSection
    {
        [Browsable(true)]
        [Category("[FilesMove]")]
        public string FileName
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
        [Category("[FilesMove]")]
        public string FilePath
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
        public override string KeyDisplayName { get { return "FileName"; } }
        [Browsable(false)]
        public override string ValueDisplayName { get { return "FilePath"; } }
    }
}
