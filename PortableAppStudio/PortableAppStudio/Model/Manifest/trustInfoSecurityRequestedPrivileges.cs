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
    public partial class trustInfoSecurityRequestedPrivileges
    {

        private trustInfoSecurityRequestedPrivilegesRequestedExecutionLevel requestedExecutionLevelField;

        /// <remarks/>
        public trustInfoSecurityRequestedPrivilegesRequestedExecutionLevel requestedExecutionLevel
        {
            get
            {
                return this.requestedExecutionLevelField;
            }
            set
            {
                this.requestedExecutionLevelField = value;
            }
        }
    }
}
