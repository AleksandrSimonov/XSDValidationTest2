using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XSDValidationTest2.Validator
{

    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://github.com/AleksandrSimonov/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "https://github.com/AleksandrSimonov/", IsNullable = false)]
    public partial class contacts
    {

        private contactsContact[] contactField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("contact")]
        public contactsContact[] contact
        {
            get
            {
                return this.contactField;
            }
            set
            {
                this.contactField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://github.com/AleksandrSimonov/")]
    public partial class contactsContact
    {

        private contactsContactFio fioField;

        private string genderField;

        private string phoneNumberField;

        private string telegramField;

        private bool isBlockedField;

        /// <remarks/>
        public contactsContactFio fio
        {
            get
            {
                return this.fioField;
            }
            set
            {
                this.fioField = value;
            }
        }

        /// <remarks/>
        public string gender
        {
            get
            {
                return this.genderField;
            }
            set
            {
                this.genderField = value;
            }
        }

        /// <remarks/>
        public string phoneNumber
        {
            get
            {
                return this.phoneNumberField;
            }
            set
            {
                this.phoneNumberField = value;
            }
        }

        /// <remarks/>
        public string telegram
        {
            get
            {
                return this.telegramField;
            }
            set
            {
                this.telegramField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool isBlocked
        {
            get
            {
                return this.isBlockedField;
            }
            set
            {
                this.isBlockedField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://github.com/AleksandrSimonov/")]
    public partial class contactsContactFio
    {

        private string lastnameField;

        private string firstnameField;

        private string nameField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("last-name")]
        public string lastname
        {
            get
            {
                return this.lastnameField;
            }
            set
            {
                this.lastnameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("first-name")]
        public string firstname
        {
            get
            {
                return this.firstnameField;
            }
            set
            {
                this.firstnameField = value;
            }
        }

        /// <remarks/>
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