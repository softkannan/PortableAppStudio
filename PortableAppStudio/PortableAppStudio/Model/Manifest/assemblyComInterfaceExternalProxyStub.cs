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
    public partial class assemblyComInterfaceExternalProxyStub
    {

        private string nameField;

        private string iidField;

        private string tlbidField;

        private string proxyStubClsid32Field;

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

        /// <remarks/>
        [XmlAttribute]
        public string iid
        {
            get
            {
                return this.iidField;
            }
            set
            {
                this.iidField = value;
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
        public string proxyStubClsid32
        {
            get
            {
                return this.proxyStubClsid32Field;
            }
            set
            {
                this.proxyStubClsid32Field = value;
            }
        }
    }

}
