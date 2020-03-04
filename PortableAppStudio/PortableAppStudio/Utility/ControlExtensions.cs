using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppStudio.Utility
{
    public static class ControlExtensions
    {
        private delegate void SetPropertyThreadSafeDelegate<TResult>(Control pThis, Expression<Func<TResult>> property, TResult value);

        public static void SetPropertyThreadSafe<TResult>(this Control pThis, Expression<Func<TResult>> property, TResult value)
        {
            var propertyInfo = (property.Body as MemberExpression).Member as PropertyInfo;
            if (propertyInfo == null || !pThis.GetType().IsSubclassOf(propertyInfo.ReflectedType) ||
                pThis.GetType().GetProperty(propertyInfo.Name, propertyInfo.PropertyType) == null)
            {
                throw new ArgumentException("The lambda expression 'property' must reference a valid property on this Control.");
            }

            if (pThis.InvokeRequired)
            {
                pThis.Invoke(new SetPropertyThreadSafeDelegate<TResult>(SetPropertyThreadSafe), new object[] { pThis, property, value });
            }
            else
            {
                pThis.GetType().InvokeMember(propertyInfo.Name, BindingFlags.SetProperty, null, pThis, new object[] { value });
            }
        }

        public static void Execute(this Form pThis, Action action)
        {
            pThis.BeginInvoke(action);
        }
    }
}
