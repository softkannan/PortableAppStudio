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
    public partial class assemblyFileComClass
    {

        private string progidField;

        private string clsidField;

        private string tlbidField;

        private string threadingModelField;


        /// <remarks/>
        public string progid
        {
            get
            {
                return this.progidField;
            }
            set
            {
                this.progidField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute]
        public string clsid
        {
            get
            {
                return this.clsidField;
            }
            set
            {
                this.clsidField = value;
            }
        }

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
        public string threadingModel
        {
            get
            {
                return this.threadingModelField;
            }
            set
            {
                this.threadingModelField = value;
            }
        }
    }

}
