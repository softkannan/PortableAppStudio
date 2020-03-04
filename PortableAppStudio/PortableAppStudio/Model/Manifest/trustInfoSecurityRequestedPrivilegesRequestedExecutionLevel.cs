using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Model.Manifest
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "urn:schemas-microsoft-com:asm.v3")]
    public partial class trustInfoSecurityRequestedPrivilegesRequestedExecutionLevel
    {

        private string levelField;

        private bool uiAccessField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string level
        {
            get
            {
                return this.levelField;
            }
            set
            {
                this.levelField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool uiAccess
        {
            get
            {
                return this.uiAccessField;
            }
            set
            {
                this.uiAccessField = value;
            }
        }
    }
}
