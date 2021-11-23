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
using PortableAppStudio.Utility;
using System.Text.RegularExpressions;

namespace PortableAppStudio.Controls
{
    public partial class PropertyEditorUserControl : UserControl
    {
        public event Model.IniValueChangedEventHandler PropertyValueChanged;
        public PropertyEditorUserControl()
        {
            InitializeComponent();

            containerGroupBox.Controls.Clear();
        }

        
        private Control GetCollectionControl(Model.IINIList selectedObject)
        {
            Control retVal;
            CollectionEditorUserControl userControl = new CollectionEditorUserControl();
            userControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            userControl.Location = new System.Drawing.Point(8, 15);
            userControl.Name = "collectionEditorUserControl";
            userControl.Size = new System.Drawing.Size(708, 585);
            userControl.TabIndex = 0;

            userControl.PropertyBrowser.SelectedGridItemChanged += PropertyGrid_SelectedGridItemChanged;
            userControl.PropertyValueChanged += UserControl_PropertyValueChanged;

            userControl.SelectedList = selectedObject;
            retVal = userControl;
            return retVal;
        }

        private Control GetPropertyControl(object selectedObject)
        {
            PropertyGrid propertyGrid = new PropertyGrid();

            propertyGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            propertyGrid.Location = new System.Drawing.Point(8, 15);
            propertyGrid.Name = "appInfoPropertyGrid";
            propertyGrid.Size = new System.Drawing.Size(708, 585);
            propertyGrid.TabIndex = 0;
            propertyGrid.PropertySort = PropertySort.NoSort;
            propertyGrid.ToolbarVisible = false;
            propertyGrid.HelpVisible = false;
            propertyGrid.SelectedGridItemChanged += PropertyGrid_SelectedGridItemChanged;
            propertyGrid.PropertyValueChanged += PropertyGrid_PropertyValueChanged;



            propertyGrid.SelectedObject = selectedObject;
            return propertyGrid;
        }

        private void FireValuePropertyValueChanged()
        {
            if (PropertyValueChanged != null)
            {
                PropertyValueChanged(this, new EventArgs());
            }
        }

        private void UserControl_PropertyValueChanged(object s, EventArgs e)
        {
            FireValuePropertyValueChanged();
        }

        private void PropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            FireValuePropertyValueChanged();
        }

        private void PropertyGrid_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
            NavigateHelp(helpTextBox, e.NewSelection.Label);
        }

        Model.IINIList _selectedObjects;
        public void SelectedObjects(Model.IINIList selectedObjects, string sectionName, string selectedProperty)
        {
            containerGroupBox.Controls.Clear();
            _selectedObjects = selectedObjects;

            if (_selectedObjects == null)
            {
                return;
            }

            var control = GetCollectionControl(_selectedObjects);
            if (control != null)
            {
                control.Size = new System.Drawing.Size(containerGroupBox.Width - 15, containerGroupBox.Height - 25);
                containerGroupBox.Controls.Add(control);
                ShowHelp(helpTextBox, sectionName, selectedProperty);
            }
        }

        private void NavigateHelp(RichTextBox textBox, string helpName)
        {
            _lastPropertySelected = helpName;
            if (!string.IsNullOrEmpty(_selectedText))
            {
                Regex helpMatch = new Regex(string.Format("^{0}\\s*$", helpName), RegexOptions.Multiline | RegexOptions.CultureInvariant);
                var match = helpMatch.Match(_selectedText);
                if (match != null && match.Success)
                {
                    int matchIndex = match.Index;
                    textBox.Select(matchIndex, 0);
                    textBox.ScrollToCaret();
                    containerGroupBox.Select();
                }
                else
                {
                    textBox.Select(1, 0);
                    textBox.ScrollToCaret();
                    containerGroupBox.Select();
                }
            }
        }

        string _selectedText = string.Empty;
        string _lastPropertySelected = string.Empty;

        private void ShowHelp(RichTextBox textBox, string helpName, string selectedProprty)
        {
            string helpNameLocal = helpName.RemoveStartEnd('[', ']');
            var tempObj = PathManager.Init.GetHelpData(helpNameLocal);
            if (string.IsNullOrWhiteSpace(tempObj))
            {
                textBox.Text = "";
            }
            else
            {
                textBox.Rtf = tempObj;
                _selectedText = textBox.Text;
                if(!string.IsNullOrWhiteSpace(selectedProprty))
                {
                    NavigateHelp(textBox, selectedProprty);
                }
            }
        }

        object _selectedObject;
        public void SelectedObject(object selectedObject, string sectionName, string selectedProprtyName)
        {

            containerGroupBox.Controls.Clear();
            _selectedObject = selectedObject;

            if (selectedObject == null)
            {
                return;
            }

            var control = GetPropertyControl(selectedObject);
            if (control != null)
            {
                control.Size = new System.Drawing.Size(containerGroupBox.Width - 15, containerGroupBox.Height - 25);
                containerGroupBox.Controls.Add(control);
                ShowHelp(helpTextBox, sectionName, selectedProprtyName);
            }
        }

        private void PropertyEditor_SizeChanged(object sender, EventArgs e)
        {
            //if (launchGroupBox.Controls.Count > 0)
            //{
            //    var control = launchGroupBox.Controls[0];
            //    control.Size = new Size(launchGroupBox.Width - 15, launchGroupBox.Height - 25);
            //}
        }
    }
}
