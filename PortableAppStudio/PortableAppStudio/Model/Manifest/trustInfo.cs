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
    [XmlType(AnonymousType = true, Namespace = "urn:schemas-microsoft-com:asm.v3")]
    [XmlRoot(Namespace = "urn:schemas-microsoft-com:asm.v3", IsNullable = false)]
    public partial class trustInfo
    {

        private trustInfoSecurity securityField;

        /// <remarks/>
        public trustInfoSecurity security
        {
            get
            {
                return this.securityField;
            }
            set
            {
                this.securityField = value;
            }
        }
    }

}
