using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PortableAppStudio.Model.Manifest
{
    /// <remarks/>
    [System.Serializable]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlType(AnonymousType = true, Namespace = "urn:schemas-microsoft-com:asm.v1")]
    public partial class assemblyFile
    {

        private List<assemblyFileComClass> comClassField;

        private List<assemblyFileTypelib> typelibField;

        private string nameField;

        /// <remarks/>
        [XmlElement("comClass")]
        public List<assemblyFileComClass> comClass
        {
            get
            {
                return this.comClassField;
            }
            set
            {
                this.comClassField = value;
            }
        }

        /// <remarks/>
         [XmlElementAttribute("typelib")]
        public List<assemblyFileTypelib> typelib
        {
            get
            {
                return this.typelibField;
            }
            set
            {
                this.typelibField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute]
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }
}
