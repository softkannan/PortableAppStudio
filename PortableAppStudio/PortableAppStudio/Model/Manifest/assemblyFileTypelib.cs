using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PortableAppStudio.Model.Manifest
{
    /// <remarks/>
    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "urn:schemas-microsoft-com:asm.v1")]
    public partial class assemblyFileTypelib
    {

        private string tlbidField;

        private decimal versionField;

        private string helpdirField;

        private string flagsField;

        /// <remarks/>
        [XmlAttribute]
        public string tlbid
        {
            get
            {
                return this.tlbidField;
            }
            set
            {
                this.tlbidField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute]
        public decimal version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute]
        public string helpdir
        {
            get
            {
                return this.helpdirField;
            }
            set
            {
                this.helpdirField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute]
        public string flags
        {
            get
            {
                return this.flagsField;
            }
            set
            {
                this.flagsField = value;
            }
        }
    }
}
