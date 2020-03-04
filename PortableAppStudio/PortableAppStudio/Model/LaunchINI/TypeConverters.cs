using PortableAppStudio.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Model.LaunchINI
{
    public class FileWriteEnvStringConverter : StringConverter
    {
        public override Boolean GetStandardValuesSupported(ITypeDescriptorContext context) { return true; }
        public override Boolean GetStandardValuesExclusive(ITypeDescriptorContext context) { return false; }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(_list);
        }

        static FileWriteEnvStringConverter()
        {
            _list.Add("");
            string filename = PathManager.Init.GetResourcePath("EnvIntellisense.txt");
            if(File.Exists(filename))
            {
                _list.AddRange(File.ReadLines(filename));
            }
        }

        private static List<string> _list = new List<string>();
    }
    public class FileWriteNTypeConverter : StringConverter
    {
        public override Boolean GetStandardValuesSupported(ITypeDescriptorContext context) { return true; }
        public override Boolean GetStandardValuesExclusive(ITypeDescriptorContext context) { return true; }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            List<String> list = new List<String>();
            list.Add("");
            list.Add("ConfigWrite");
            list.Add("INI");
            list.Add("Replace");
            list.Add("XML attribute");
            list.Add("XML text");
            return new StandardValuesCollection(list);
        }
    }
    public class EncodingConverter : StringConverter
    {
        public override Boolean GetStandardValuesSupported(ITypeDescriptorContext context) { return true; }
        public override Boolean GetStandardValuesExclusive(ITypeDescriptorContext context) { return true; }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            List<String> list = new List<String>();
            list.Add("");
            list.Add("auto");
            list.Add("ANSI");
            list.Add("UTF - 16LE");
            return new StandardValuesCollection(list);
        }
    }
    public class YeNoWarnConverter : StringConverter
    {
        public override Boolean GetStandardValuesSupported(ITypeDescriptorContext context) { return true; }
        public override Boolean GetStandardValuesExclusive(ITypeDescriptorContext context) { return true; }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            List<String> list = new List<String>();
            list.Add("");
            list.Add("yes");
            list.Add("no");
            list.Add("warn");
            return new StandardValuesCollection(list);
        }
    }

    public class JavaStringConverter : StringConverter
    {
        public override Boolean GetStandardValuesSupported(ITypeDescriptorContext context) { return true; }
        public override Boolean GetStandardValuesExclusive(ITypeDescriptorContext context) { return true; }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            List<String> list = new List<String>();
            list.Add("");
            list.Add("none");
            list.Add("find");
            list.Add("require");
            return new StandardValuesCollection(list);
        }
    }
    public class RefreshShellIconsStringConverter : StringConverter
    {
        public override Boolean GetStandardValuesSupported(ITypeDescriptorContext context) { return true; }
        public override Boolean GetStandardValuesExclusive(ITypeDescriptorContext context) { return true; }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            List<String> list = new List<String>();
            list.Add("");
            list.Add("none");
            list.Add("before");
            list.Add("after");
            list.Add("both");
            return new StandardValuesCollection(list);
        }
    }

    public class OSSelectionStringConverter : StringConverter
    {
        public override Boolean GetStandardValuesSupported(ITypeDescriptorContext context) { return true; }
        public override Boolean GetStandardValuesExclusive(ITypeDescriptorContext context) { return true; }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            List<String> list = new List<String>();
            list.Add("");
            list.Add("none");
            list.Add("2000");
            list.Add("XP");
            list.Add("2003");
            list.Add("Vista");
            list.Add("2008");
            list.Add("7");
            list.Add("2008 R2");
            list.Add("10");
            return new StandardValuesCollection(list);
        }
    }

    public class RunAsAdminStringConverter : StringConverter
    {
        public override Boolean GetStandardValuesSupported(ITypeDescriptorContext context) { return true; }
        public override Boolean GetStandardValuesExclusive(ITypeDescriptorContext context) { return true; }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            List<String> list = new List<String>();
            list.Add("");
            list.Add("none");
            list.Add("force");
            list.Add("try");
            list.Add("compile-force");
            return new StandardValuesCollection(list);
        }
    }
    //public class OSSelectionTypeConverter : TypeConverter
    //{
    //    //public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    //    //{
    //    //    if (destinationType != typeof(string))
    //    //        return base.ConvertTo(context, culture, value, destinationType);

    //    //    List<Member> members = value as List<Member>;
    //    //    if (members == null)
    //    //        return "-";

    //    //    return string.Join(", ", members.Select(m => m.Name));
    //    //}

    //    //public override bool GetPropertiesSupported(ITypeDescriptorContext context)
    //    //{
    //    //    return true;
    //    //}

    //    //public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
    //    //{
    //    //    List<PropertyDescriptor> list = new List<PropertyDescriptor>();
    //    //    List<Member> members = value as List<Member>;
    //    //    if (members != null)
    //    //    {
    //    //        foreach (Member member in members)
    //    //        {
    //    //            if (member.Name != null)
    //    //            {
    //    //                list.Add(new MemberDescriptor(member, list.Count));
    //    //            }
    //    //        }
    //    //    }
    //    //    return new PropertyDescriptorCollection(list.ToArray());
    //    //}

    //    //private class MemberDescriptor : SimplePropertyDescriptor
    //    //{
    //    //    public MemberDescriptor(Member member, int index)
    //    //        : base(member.GetType(), index.ToString(), typeof(string))
    //    //    {
    //    //        Member = member;
    //    //    }

    //    //    public Member Member { get; private set; }

    //    //    public override object GetValue(object component)
    //    //    {
    //    //        return Member.Name;
    //    //    }

    //    //    public override void SetValue(object component, object value)
    //    //    {
    //    //        Member.Name = (string)value;
    //    //    }
    //    //}
    //}
}
