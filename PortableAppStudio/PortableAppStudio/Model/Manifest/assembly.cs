using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PortableAppStudio.Model.Manifest
{
    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "urn:schemas-microsoft-com:asm.v1")]
    [XmlRoot(Namespace = "urn:schemas-microsoft-com:asm.v1", IsNullable = false)]
    public partial class Assembly
    {

        private List<assemblyFile> fileField;

        private trustInfo trustInfoField;

        private List<assemblyComInterfaceExternalProxyStub> comInterfaceExternalProxyStubField;

        private decimal manifestVersionField;

        /// <remarks/>
        [XmlElement("file")]
        public List<assemblyFile> file
        {
            get
            {
                return this.fileField;
            }
            set
            {
                this.fileField = value;
            }
        }

        /// <remarks/>
        [XmlElement(Namespace = "urn:schemas-microsoft-com:asm.v3")]
        public trustInfo trustInfo
        {
            get
            {
                return this.trustInfoField;
            }
            set
            {
                this.trustInfoField = value;
            }
        }

        /// <remarks/>
        [XmlElement("comInterfaceExternalProxyStub")]
        public List<assemblyComInterfaceExternalProxyStub> comInterfaceExternalProxyStub
        {
            get
            {
                return this.comInterfaceExternalProxyStubField;
            }
            set
            {
                this.comInterfaceExternalProxyStubField = value;
            }
        }

        /// <remarks/>
        [XmlAttribute]
        public decimal manifestVersion
        {
            get
            {
                return this.manifestVersionField;
            }
            set
            {
                this.manifestVersionField = value;
            }
        }
    }
}
