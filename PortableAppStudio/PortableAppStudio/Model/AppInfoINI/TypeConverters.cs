using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Model.AppInfoINI
{
    public class CategoryStringConverter : StringConverter
    {
        public override Boolean GetStandardValuesSupported(ITypeDescriptorContext context) { return true; }
        public override Boolean GetStandardValuesExclusive(ITypeDescriptorContext context) { return true; }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            List<String> list = new List<String>();
            list.Add("Accessibility");
            list.Add("Development");
            list.Add("Education");
            list.Add("Games");
            list.Add("Graphics & Pictures");
            list.Add("Internet");
            list.Add("Music & Video");
            list.Add("Office");
            list.Add("Security");
            list.Add("Utilities");
            return new StandardValuesCollection(list);
        }
    }

    public class LanguageStringConverter : StringConverter
    {
        public override Boolean GetStandardValuesSupported(ITypeDescriptorContext context) { return true; }
        public override Boolean GetStandardValuesExclusive(ITypeDescriptorContext context) { return true; }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {

            List<String> list = new List<String> { "Multilingual",
                "Afrikaans", "Albanian", "Arabic", "Armenian", "Basque", "Belarusian", "Bosnian",
                "Breton", "Bulgarian", "Catalan", "Cibemba", "Croatian", "Czech", "Danish", "Dutch", "Efik", "English",
                "Estonian", "Farsi", "Finnish", "French", "Galician", "Georgian", "German", "Greek", "Hebrew",
                "Hungarian", "Icelandic", "Igbo", "Indonesian", "Irish", "Italian", "Japanese", "Khmer", "Korean", "Kurdish",
                "Latvian", "Lithuanian", "Luxembourgish", "Macedonian", "Malagasy", "Malay", "Mongolian", "Norwegian",
                "NorwegianNynorsk", "Pashto", "Polish","Portuguese", "PortugueseBR", "Romanian", "Russian",
                "Serbian", "SerbianLatin", "SimpChinese", "Slovak", "Slovenian", "Spanish", "SpanishInternational",
                "Swahili", "Swedish", "Thai", "TradChinese", "Turkish", "Ukranian", "Uzbek", "Valencian", "Vietnamese","Welsh", "Yoruba"
            };
            return new StandardValuesCollection(list);
        }
    }
}
