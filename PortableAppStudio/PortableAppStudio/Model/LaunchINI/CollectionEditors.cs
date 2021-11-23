using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Reflection;
using System.Drawing.Design;
using System.Windows.Forms;

namespace PortableAppStudio.Model.LaunchINI
{
    public class WaitForExeListEditor : CollectionEditor
    {
        public WaitForExeListEditor(Type type) : base(type) 
        {
        }

        //protected override Type CreateCollectionItemType()
        //{
        //    return typeof(WaitForEXENValue);
        //}

        //protected override string GetDisplayText(object value)
        //{
        //    string retVal = "";
        //    WaitForEXENValue listValue = value as WaitForEXENValue;
        //    if(listValue != null)
        //    {
        //        retVal = string.Format(" WaitForEXE={0}", listValue.ExeName);
        //    }
        //    return retVal;
        //}

        //protected override object CreateInstance(Type itemType)
        //{
        //    return new WaitForEXENValue(null);
        //}

        protected override CollectionEditor.CollectionForm CreateCollectionForm()
        {
            CollectionEditor.CollectionForm collectionForm = base.CreateCollectionForm();

            collectionForm.Size = new System.Drawing.Size(800, 600);

            var topLayout = collectionForm.Controls["overArchingTableLayoutPanel"];

            if (topLayout != null)
            {
                var addRemoveLayout = topLayout.Controls["addRemoveTableLayoutPanel"];

                if (addRemoveLayout != null)
                {
                    addRemoveLayout.Width = 300;
                }
            }

            //(System.Windows.Forms.ContainerControl)collectionForm.Controls[0].Controls[5]
            collectionForm.Text = "WaitForExe List Editor";
            return collectionForm;
        }


        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            object result = base.EditValue(context, provider, value);

            // assign the temporary collection from the UI to the property
            ((LaunchSection)context.Instance).WaitForEXEN = (ExeProcessList<WaitForEXENValue>)result;

            return result;
        }
    }

    public class KillProcListEditor : CollectionEditor
    {
        public KillProcListEditor(Type type) : base(type) 
        { 
        }

        //protected override Type CreateCollectionItemType()
        //{
        //    return typeof(WaitForEXENValue);
        //}

        //protected override string GetDisplayText(object value)
        //{
        //    string retVal = "";
        //    WaitForEXENValue listValue = value as WaitForEXENValue;
        //    if(listValue != null)
        //    {
        //        retVal = string.Format(" WaitForEXE={0}", listValue.ExeName);
        //    }
        //    return retVal;
        //}

        //protected override object CreateInstance(Type itemType)
        //{
        //    return new WaitForEXENValue(null);
        //}

        protected override CollectionEditor.CollectionForm CreateCollectionForm()
        {
            CollectionEditor.CollectionForm collectionForm = base.CreateCollectionForm();

            collectionForm.Size = new System.Drawing.Size(800, 600);

            var topLayout = collectionForm.Controls["overArchingTableLayoutPanel"];

            if (topLayout != null)
            {
                var addRemoveLayout = topLayout.Controls["addRemoveTableLayoutPanel"];

                if (addRemoveLayout != null)
                {
                    addRemoveLayout.Width = 300;
                }
            }

            //(System.Windows.Forms.ContainerControl)collectionForm.Controls[0].Controls[5]
            collectionForm.Text = "KillProc List Editor";
            return collectionForm;
        }


        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            object result = base.EditValue(context, provider, value);

            // assign the temporary collection from the UI to the property
            ((LaunchSection)context.Instance).KillProcN = (ExeProcessList<KillProcNValue>)result;

            return result;
        }
    }
}
