using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Forms.Design;


namespace PortableAppStudio.Model
{
    public class RunTimeUIEdTools
    {
        public static string BaseNameFromType(Type ItemType)
        {
            return RunTimeUIEdTools.BaseNameFromTypeName(ItemType.ToString());
        }

        public static string BaseNameFromTypeName(string ItemTypeName)
        {
            return ItemTypeName.Remove(0, checked(ItemTypeName.LastIndexOf(".") + 1));
        }

        public sealed class RunTimeTypeEdit : IWindowsFormsEditorService, IServiceProvider, ITypeDescriptorContext
        {
            private readonly IWin32Window myOwner;
            private readonly object myComponent;
            private readonly PropertyDescriptor myProperty;

            /// <summary>
            /// </summary>
            /// <param name="owner">form owner for the UI Dialog</param>
            /// <param name="component">the class instance to edit</param>
            /// <param name="propertyName">property name to edit (must have the Editor attribute)</param>
            /// <remarks></remarks>
            public static void ShowEditor(IWin32Window owner, object component, string propertyName)
            {
                PropertyDescriptor property = TypeDescriptor.GetProperties(RuntimeHelpers.GetObjectValue(component))[propertyName];
                if (property == null)
                    throw new ArgumentException(nameof(propertyName));
                UITypeEditor editor = (UITypeEditor)property.GetEditor(typeof(UITypeEditor));
                if (editor == null)
                    throw new NotImplementedException("Unsupported UIEditor Type");
                if (!(editor.GetType().IsSubclassOf(typeof(EnhancedCollectionEditor)) | editor.GetType().IsSubclassOf(typeof(CollectionEditor))))
                    throw new NotImplementedException("Unsupported UIEditor Type");
                RunTimeUIEdTools.RunTimeTypeEdit runTimeTypeEdit = new RunTimeUIEdTools.RunTimeTypeEdit(owner, RuntimeHelpers.GetObjectValue(component), property);
                object objectValue1 = RuntimeHelpers.GetObjectValue(property.GetValue(RuntimeHelpers.GetObjectValue(component)));
                object objectValue2 = RuntimeHelpers.GetObjectValue(editor.EditValue((ITypeDescriptorContext)runTimeTypeEdit, (IServiceProvider)runTimeTypeEdit, RuntimeHelpers.GetObjectValue(objectValue1)));
                if (property.IsReadOnly)
                    return;
                property.SetValue(RuntimeHelpers.GetObjectValue(component), RuntimeHelpers.GetObjectValue(objectValue2));
            }

            private RunTimeTypeEdit(IWin32Window owner, object component, PropertyDescriptor prop)
            {
                this.myOwner = owner;
                this.myComponent = RuntimeHelpers.GetObjectValue(component);
                this.myProperty = prop;
            }

            public static UITypeEditor GetUIEditor(object instance, string propName)
            {
                return (UITypeEditor)TypeDescriptor.GetProperties(RuntimeHelpers.GetObjectValue(instance))[propName].GetEditor(typeof(UITypeEditor));
            }

            public object GetService(Type serviceType)
            {
                if (serviceType == typeof(IWindowsFormsEditorService))
                    return (object)this;
                return (object)null;
            }

            public DialogResult ShowDialog(Form dialog)
            {
                return dialog.ShowDialog(this.myOwner);
            }

            IContainer ITypeDescriptorContext.Container
            {
                get
                {
                    return (IContainer)null;
                }
            }

            object ITypeDescriptorContext.Instance
            {
                get
                {
                    return this.myComponent;
                }
            }

            void ITypeDescriptorContext.OnComponentChanged()
            {
            }

            bool ITypeDescriptorContext.OnComponentChanging()
            {
                return true;
            }

            PropertyDescriptor ITypeDescriptorContext.PropertyDescriptor
            {
                get
                {
                    return this.myProperty;
                }
            }

            public void CloseDropDown()
            {
                throw new NotImplementedException();
            }

            public void DropDownControl(Control control)
            {
                throw new NotImplementedException();
            }
        }
    }
}
