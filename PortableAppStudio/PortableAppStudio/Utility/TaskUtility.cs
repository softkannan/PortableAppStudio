using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppStudio.Utility
{
    public static class TaskUtility
    {
        public static Task StartSTATask<T>(Func<T> func)
        {
            var tcs = new TaskCompletionSource<T>();
            Thread thread = new Thread(() =>
            {
                try
                {
                    tcs.SetResult(func());
                    Application.Run();
                }
                catch (Exception e)
                {
                    tcs.SetException(e);
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            return tcs.Task;
        }

        public static Thread StartSTAThread(Action action)
        {
            Thread thread = new Thread(() =>
            {
                action();
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            return thread;
        }
    }
}
