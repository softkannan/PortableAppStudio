using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Design;
using System.Drawing.Design;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design;
using System.Windows.Forms.VisualStyles;
using System.ComponentModel.Design;
using PortableAppStudio.Utility;

namespace PortableAppStudio.Controls
{
    public class CollectionEditorUserControl : UserControl
    {
        public event Model.IniValueChangedEventHandler PropertyValueChanged;
        private TableLayoutPanel overArchingTableLayoutPanel;

        private Model.IINIList _selectedList;

        public Model.IINIList SelectedList
        {
            get { return _selectedList; }
            set
            {
                _type = value.GetType();
                _selectedList = value;
                UpdateColumn(CreateNewItem());
                UpdateUI();
            }
        }

        private void UpdateColumn(Model.IINIKeyValuePair sampleItem)
        {
            listView.Columns.Clear();
            if (this.listView.Columns.Count == 0)
            {
                this.listView.Columns.Add(sampleItem.KeyDisplayName, -2, HorizontalAlignment.Right);
                this.listView.Columns.Add("", -2, HorizontalAlignment.Center);
                this.listView.Columns.Add(sampleItem.ValueDisplayName, -2, HorizontalAlignment.Left);
            }
        }
        private void UpdateUI()
        {
            listView.Items.Clear();
            listView.SuspendLayout();
            foreach (Model.IINIKeyValuePair inst in _selectedList)
            {
                var listViewItem = new ListViewItem(new string[] { inst.IniKey,"=", inst.IniValue });
                listViewItem.Tag = inst;
                listView.Items.Add(listViewItem);
            }
            listView.Invalidate();
            listView.ResumeLayout();
        }

        private Type _collectionItemType;
        private PropertyGrid _propertyBrowser;
        private TableLayoutPanel _addRemoveTableLayoutPanel;
        private Button _addButton;
        private Button _removeButton;
        private ListView listView;

        public PropertyGrid PropertyBrowser { get { return _propertyBrowser; } }

        /// <summary>Gets the data type of each item in the collection.</summary>
        /// <returns>The data type of the collection items.</returns>
        public Type CollectionItemType
        {
            get
            {
                if (this._collectionItemType == null)
                {
                    this._collectionItemType = this.CreateCollectionItemType();
                }
                return this._collectionItemType;
            }
        }
        private Type _type;
        /// <summary>Gets the data type of the collection object.</summary>
        /// <returns>The data type of the collection object.</returns>
        public Type CollectionType
        {
            get
            {
                return this._type;
            }
        }

        /// <summary>Gets the data type that this collection contains.</summary>
        /// <returns>The data type of the items in the collection, or an <see cref="T:System.Object" /> if no <see langword="Item" /> property can be located on the collection.</returns>
        protected virtual Type CreateCollectionItemType()
        {
            PropertyInfo[] properties = TypeDescriptor.GetReflectionType(this.CollectionType).GetProperties(BindingFlags.Instance | BindingFlags.Public);
            for (int i = 0; i < properties.Length; i++)
            {
                if (properties[i].Name.Equals("Item") || properties[i].Name.Equals("Items"))
                {
                    return properties[i].PropertyType;
                }
            }
            return typeof(object);
        }
        
        public CollectionEditorUserControl()
        {
            this.InitializeComponent();
            this.HookEvents();
        }

        private void AddButton_click(object sender, EventArgs e)
        {
            this.PerformAdd();
        }
        protected virtual void DisplayError(Exception e)
        {
            string text = e.Message;
            if (text == null || text.Length == 0)
            {
                text = e.ToString();
            }
            MessageBox.Show(null, text, null, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
        }

        private void HookEvents()
        {
            this.listView.KeyDown += this.ListView_keyDown;
            this.listView.SelectedIndexChanged += this.ListView_SelectedIndexChanged;
            this._propertyBrowser.PropertyValueChanged += this.PropertyGrid_propertyValueChanged;
            this._addButton.Click += this.AddButton_click;
            this._removeButton.Click += this.RemoveButton_click;
        }

        private void InitializeComponent()
        {
            this.overArchingTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this._propertyBrowser = new System.Windows.Forms.PropertyGrid();
            this._addRemoveTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this._addButton = new System.Windows.Forms.Button();
            this._removeButton = new System.Windows.Forms.Button();
            this.listView = new System.Windows.Forms.ListView();
            this.overArchingTableLayoutPanel.SuspendLayout();
            this._addRemoveTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // overArchingTableLayoutPanel
            // 
            this.overArchingTableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.overArchingTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.overArchingTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.overArchingTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.overArchingTableLayoutPanel.Controls.Add(this._propertyBrowser, 0, 3);
            this.overArchingTableLayoutPanel.Controls.Add(this._addRemoveTableLayoutPanel, 0, 2);
            this.overArchingTableLayoutPanel.Controls.Add(this.listView, 0, 1);
            this.overArchingTableLayoutPanel.Location = new System.Drawing.Point(5, 3);
            this.overArchingTableLayoutPanel.Name = "overArchingTableLayoutPanel";
            this.overArchingTableLayoutPanel.RowCount = 4;
            this.overArchingTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.overArchingTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.overArchingTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.overArchingTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.overArchingTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.overArchingTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.overArchingTableLayoutPanel.Size = new System.Drawing.Size(572, 466);
            this.overArchingTableLayoutPanel.TabIndex = 0;
            // 
            // _propertyBrowser
            // 
            this._propertyBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._propertyBrowser.CommandsVisibleIfAvailable = false;
            this._propertyBrowser.HelpVisible = false;
            this._propertyBrowser.Location = new System.Drawing.Point(5, 258);
            this._propertyBrowser.Margin = new System.Windows.Forms.Padding(5);
            this._propertyBrowser.Name = "_propertyBrowser";
            this._propertyBrowser.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this._propertyBrowser.Size = new System.Drawing.Size(562, 203);
            this._propertyBrowser.TabIndex = 2;
            this._propertyBrowser.ToolbarVisible = false;
            // 
            // _addRemoveTableLayoutPanel
            // 
            this._addRemoveTableLayoutPanel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this._addRemoveTableLayoutPanel.AutoSize = true;
            this._addRemoveTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._addRemoveTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._addRemoveTableLayoutPanel.Controls.Add(this._addButton, 1, 0);
            this._addRemoveTableLayoutPanel.Controls.Add(this._removeButton, 0, 0);
            this._addRemoveTableLayoutPanel.Location = new System.Drawing.Point(335, 218);
            this._addRemoveTableLayoutPanel.Name = "_addRemoveTableLayoutPanel";
            this._addRemoveTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this._addRemoveTableLayoutPanel.Size = new System.Drawing.Size(234, 29);
            this._addRemoveTableLayoutPanel.TabIndex = 7;
            // 
            // _addButton
            // 
            this._addButton.AutoSize = true;
            this._addButton.Location = new System.Drawing.Point(120, 3);
            this._addButton.Name = "_addButton";
            this._addButton.Size = new System.Drawing.Size(100, 23);
            this._addButton.TabIndex = 0;
            this._addButton.Text = "Add";
            // 
            // _removeButton
            // 
            this._removeButton.AutoSize = true;
            this._removeButton.Location = new System.Drawing.Point(3, 3);
            this._removeButton.Name = "_removeButton";
            this._removeButton.Size = new System.Drawing.Size(111, 23);
            this._removeButton.TabIndex = 1;
            this._removeButton.Text = "Remove";
            // 
            // listView
            // 
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView.FullRowSelect = true;
            this.listView.Location = new System.Drawing.Point(3, 3);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(566, 207);
            this.listView.TabIndex = 3;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            // 
            // CollectionEditorUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.overArchingTableLayoutPanel);
            this.Name = "CollectionEditorUserControl";
            this.Size = new System.Drawing.Size(584, 472);
            this.overArchingTableLayoutPanel.ResumeLayout(false);
            this.overArchingTableLayoutPanel.PerformLayout();
            this._addRemoveTableLayoutPanel.ResumeLayout(false);
            this._addRemoveTableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }
        private void ListView_keyDown(object sender, KeyEventArgs kevent)
        {
            switch (kevent.KeyData)
            {
                case Keys.Delete:
                    this.PerformRemove();
                    break;
                case Keys.Insert:
                    this.PerformAdd();
                    break;
            }
        }

        private void ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.listView.SelectedItems.Count > 0)
            {
                this._propertyBrowser.Enabled = true;
                this._propertyBrowser.SelectedObject = this.listView.SelectedItems[0].Tag;
            }
            else
            {
                this._propertyBrowser.SelectedObject = null;
            }
        }
        
        private void PerformAdd()
        {
            try
            {
                if (_selectedList != null)
                {
                    Model.IINIKeyValuePair newItem = null;
                    var listValue = _selectedList.InvokeMethod("CreateNewItem") as List<string>;
                    if (listValue == null)
                    {
                        newItem = CreateNewItem();
                        _selectedList.Add(newItem);
                        _selectedList.UpdateIndex();
                    }
                    else if (listValue.Count > 0)
                    {
                        foreach (var item in listValue)
                        {
                            newItem = CreateNewItem();
                            newItem.IniValue = item;
                            _selectedList.Add(newItem);
                            _selectedList.UpdateIndex();
                        }
                    }
                    FireValuePropertyValueChanged();
                }
            }
            catch (Exception e)
            {
                this.DisplayError(e);
            }
            UpdateUI();
        }

        private Model.IINIKeyValuePair CreateNewItem()
        {
            return Activator.CreateInstance(this.CollectionItemType) as Model.IINIKeyValuePair;
        }

        private void PerformRemove()
        {
            try
            {
                foreach (ListViewItem item in this.listView.SelectedItems)
                {
                    var removeObj = item.Tag as Model.IINIKeyValuePair;
                    if (removeObj != null)
                    {
                        IDisposable disposable = removeObj as IDisposable;
                        if (disposable != null)
                        {
                            disposable.Dispose();
                        }
                        _selectedList.Remove(removeObj);
                    }
                }

                _selectedList.UpdateIndex();
                FireValuePropertyValueChanged();
            }
            catch (Exception e)
            {
                this.DisplayError(e);
            }
            UpdateUI();
        }

        private void PropertyGrid_propertyValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            UpdateUI();
            FireValuePropertyValueChanged();
        }

        private void FireValuePropertyValueChanged()
        {
            if(PropertyValueChanged  != null)
            {
                PropertyValueChanged(this, new EventArgs());
            }
        }

        private void RemoveButton_click(object sender, EventArgs e)
        {
            this.PerformRemove();
        }
    }
}
