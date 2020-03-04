using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppStudio.Model
{
    public abstract class EnhancedCollectionEditor : CollectionEditor
    {
        private Type myType;
        private Type mycolBaseType;
        protected internal List<Type> ExcludedTypes;
        private PropertyGrid _propG;
        private ListBox itemListBox;
        private object collection;
        private const string Caption = "CollectionEditor";

        protected internal string FormCaption { get; set; }

        protected internal bool ShowPropGridHelp { get; set; }

        protected internal bool AllowMultipleSelect { get; set; }

        protected internal bool UsePropGridChangeEvent { get; set; }

        protected internal NameServices NameService { get; set; }

        protected internal event EnhancedCollectionEditor.PropertyValueChangedEventHandler PropertyValueChanged;

        protected internal event EnhancedCollectionEditor.EditorFormCreatedEventHandler EditorFormCreated;

        protected internal event EnhancedCollectionEditor.NewItemCreatedEventHandler NewItemCreated;

        protected internal Type BaseCollectionType
        {
            get
            {
                return this.myType;
            }
        }

        protected internal Type BaseItemType
        {
            get
            {
                return this.mycolBaseType;
            }
        }

        protected internal string GetVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        /// <summary>Creates a new collection editor</summary>
        /// <param name="t">The type of the collection for this editor to edit.</param>
        public EnhancedCollectionEditor(Type t)
          : base(t)
        {
            this.myType = (Type)null;
            this.mycolBaseType = (Type)null;
            this.ExcludedTypes = new List<Type>();
            this._propG = (PropertyGrid)null;
            this.FormCaption = "Collection Class Editor";
            this.ShowPropGridHelp = true;
            this.AllowMultipleSelect = true;
            this.UsePropGridChangeEvent = false;
            this.NameService = NameServices.None;
            this.myType = t;

            this.mycolBaseType = this.CollectionItemType;
            if ((object)this.mycolBaseType == (object)typeof(object))
            {
                this.mycolBaseType = this.GetCollectionItemType(t);
            }

            if ((object)this.mycolBaseType == null)
            {
                this.DisplayError(string.Format("Underlying Type [{0}] must implement 'Item' as a PROPERTY.{1}A NullReferenceException will result trying to use [{2}]", (object)this.myType.ToString(), (object)Environment.NewLine, (object)this.GetType().Name));
            }
            else
            {
                if ((object)this.mycolBaseType != (object)typeof(object))
                    return;
                this.DisplayError(string.Format("No Editor available for [System.Object] in collection [{0}].", (object)this.myType.ToString()));
            }
        }

        private Type GetCollectionItemType(Type t)
        {
            PropertyInfo property = t.GetProperty("Item", new Type[1]
            {
        typeof (int)
            });
            if (property != null)
                return property.PropertyType;
            if ((object)t.GetMethod("Item") == null)
                return (Type)null;
            return (Type)null;
        }

        protected override sealed Type[] CreateNewItemTypes()
        {
            List<Type> typeList = new List<Type>();
            if (!this.mycolBaseType.IsAbstract)
                return base.CreateNewItemTypes();
            Type[] types = Assembly.GetAssembly(this.mycolBaseType).GetTypes();
            int index = 0;
            while (index < types.Length)
            {
                Type type = types[index];
                if (type.IsSubclassOf(this.mycolBaseType) & !type.IsAbstract && (this.ExcludedTypes != null && !this.ExcludedTypes.Contains(type)))
                    typeList.Add(type);
                checked { ++index; }
            }
            return typeList.ToArray();
        }

        protected override bool CanSelectMultipleInstances()
        {
            return this.AllowMultipleSelect;
        }

        public override sealed object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            this.collection = RuntimeHelpers.GetObjectValue(value);
            return base.EditValue(context, provider, RuntimeHelpers.GetObjectValue(value));
        }

        protected override sealed CollectionEditor.CollectionForm CreateCollectionForm()
        {
            CollectionEditor.CollectionForm collectionForm1 = base.CreateCollectionForm();
            this._propG = (PropertyGrid)collectionForm1.Controls["overArchingTableLayoutPanel"].Controls["propertyBrowser"];
            this.itemListBox = (ListBox)collectionForm1.Controls["overArchingTableLayoutPanel"].Controls["listbox"];
            if (this._propG != null)
            {
                this._propG.HelpVisible = this.ShowPropGridHelp;
                if (this.UsePropGridChangeEvent)
                    this._propG.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propGridValChanged);
            }
            CollectionEditor.CollectionForm collectionForm2;
            int num = checked((collectionForm2 = collectionForm1).Height + 40);
            collectionForm2.Height = num;
            collectionForm1.Text = this.FormCaption;
            // ISSUE: reference to a compiler-generated field
            //EnhancedCollectionEditor.EditorFormCreatedEventHandler formCreatedEvent = this.EditorFormCreatedEvent;
            //if (formCreatedEvent != null)
            //    formCreatedEvent((object)this, new EditorCreatedEventArgs((Form)collectionForm1));
            return collectionForm1;
        }

        ~EnhancedCollectionEditor()
        {
            if (this._propG != null)
                this._propG.Dispose();
            if (this.itemListBox != null)
                this.itemListBox.Dispose();
            // ISSUE: explicit finalizer call
            //base.Finalize();
        }

        private void propGridValChanged(object sender, PropertyValueChangedEventArgs e)
        {
            // ISSUE: reference to a compiler-generated field
            //EnhancedCollectionEditor.PropertyValueChangedEventHandler valueChangedEvent = this.PropertyValueChangedEvent;
            //if (valueChangedEvent == null)
            //    return;
            //valueChangedEvent(RuntimeHelpers.GetObjectValue(sender), e);
        }

        protected override sealed object CreateInstance(Type itemType)
        {
            string name = RunTimeUIEdTools.BaseNameFromType(itemType);
            object objectValue = RuntimeHelpers.GetObjectValue(base.CreateInstance(itemType));
            PropertyInfo property = objectValue.GetType().GetProperty("Name");
            if ((uint)this.NameService > 0U)
            {
                if ((object)property == null)
                {
                    this.DisplayError("[Name] property must be implemented to use the NamingService");
                    return objectValue;
                }
                if (!property.CanWrite)
                {
                    this.DisplayError("[Name] property cannot be Read-Only using NamingService");
                    return objectValue;
                }
            }
            switch (this.NameService)
            {
                case NameServices.Automatic:
                    NewItemCreatedEventArgs e = new NewItemCreatedEventArgs(name);
                    // ISSUE: reference to a compiler-generated field
                    //EnhancedCollectionEditor.NewItemCreatedEventHandler itemCreatedEvent = this.NewItemCreatedEvent;
                    //if (itemCreatedEvent != null)
                    //    itemCreatedEvent((object)this, e);
                    //string newName = this.GetNewName(e.ItemBaseName);
                    //property.SetValue(RuntimeHelpers.GetObjectValue(objectValue), (object)newName, (object[])null);
                    break;
                case NameServices.NameProvider:
                    string nameFromProvider = this.GetNameFromProvider(itemType);
                    property.SetValue(RuntimeHelpers.GetObjectValue(objectValue), (object)nameFromProvider, (object[])null);
                    break;
            }
            return objectValue;
        }

        private string GetNameFromProvider(Type itemType)
        {
            string str = "";
            if (this.collection != null && ((IEnumerable<Type>)this.collection.GetType().GetInterfaces()).Contains<Type>(typeof(INameProvider)))
                str = ((INameProvider)this.collection).GetNewName(itemType.ToString());
            if (string.IsNullOrEmpty(str) && this.Context.Instance != null && ((IEnumerable<Type>)this.Context.Instance.GetType().GetInterfaces()).Contains<Type>(typeof(INameProvider)))
                str = ((INameProvider)this.Context.Instance).GetNewName(itemType.ToString());
            if (!string.IsNullOrEmpty(str))
                return str;
            this.DisplayError(string.Format("INameProvider not implemented on property provider: [{0}] {1} or Collection [{2}]", (object)this.Context.Instance.GetType().ToString(), (object)Environment.NewLine, this.collection != null ? (object)this.collection.GetType().ToString() : (object)"???"));
            return "";
        }

        private string GetNewName(string baseName)
        {
            return baseName + this.GetMaxListVal(baseName).ToString();
        }

        private int GetMaxListVal(string basename)
        {
            int num1 = 1;
            if (this.itemListBox != null)
            {
                if (this.itemListBox.Items.Count > 0)
                {
                    int num2 = checked(this.itemListBox.Items.Count - 1);
                    int index = 0;
                    while (index <= num2)
                    {
                        char[] charArray = this.itemListBox.Items[index].ToString().ToCharArray();
                        Func<char, bool> predicate = (Func<char, bool>)(c => char.IsDigit(c));
                        // ISSUE: reference to a compiler-generated field
                        //            if (EnhancedCollectionEditor._Closure\u0024__.\u0024I66\u002D0 != null)
                        //{
                        //                // ISSUE: reference to a compiler-generated field
                        //                predicate = EnhancedCollectionEditor._Closure\u0024__.\u0024I66\u002D0;
                        //            }
                        //else
                        //{
                        //                // ISSUE: reference to a compiler-generated field
                        //                EnhancedCollectionEditor._Closure\u0024__.\u0024I66\u002D0 = predicate = (Func<char, bool>)(c => char.IsDigit(c));
                        //            }
                        int result;
                        if (int.TryParse(new string(((IEnumerable<char>)charArray).Where<char>(predicate).ToArray<char>()), out result) && result > num1)
                            num1 = result;
                        checked { ++index; }
                    }
                    checked { ++num1; }
                }
            }
            else
                this.DisplayError("NamingService not available.");
            return num1;
        }

        protected internal Control GetControlByName(string ctlName, Control.ControlCollection ctls)
        {
            return (ctls["overArchingTableLayoutPanel"].Controls[ctlName] ?? ctls["addRemoveTableLayoutPanel"].Controls[ctlName]) ?? ctls["okCancelTableLayoutPanel"].Controls[ctlName];
        }

        protected internal void DisplayError(string msg)
        {
            int num = (int)MessageBox.Show(msg, "Plutonix CollectionEditor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        protected internal delegate void PropertyValueChangedEventHandler(object sender, PropertyValueChangedEventArgs e);

        protected internal delegate void EditorFormCreatedEventHandler(object sender, EditorCreatedEventArgs e);

        protected internal delegate void NewItemCreatedEventHandler(object sender, NewItemCreatedEventArgs e);
    }
}
