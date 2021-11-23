using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Model.LaunchINI
{
    public class ExeProcessList<T> : INIValueList<T>
        where T : class, IINIKeyValuePair, new()
    {
        //public object CreateNewItem()
        //{
        //    Dialogs.AddProcForm addForm = new Dialogs.AddProcForm();
        //    addForm.AutoCompleteList = Model.ExeFileNameListStringConverter.ExeFileNameList;
        //    if (addForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {
        //        return addForm.SelectedItems;
        //    }
        //    return new List<string>();
        //}
    }
}
