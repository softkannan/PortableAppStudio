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
    public partial class trustInfoSecurity
    {

        private trustInfoSecurityRequestedPrivileges requestedPrivilegesField;

        /// <remarks/>
        public trustInfoSecurityRequestedPrivileges requestedPrivileges
        {
            get
            {
                return this.requestedPrivilegesField;
            }
            set
            {
                this.requestedPrivilegesField = value;
            }
        }
    }

}
