using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppStudio.Dialogs
{
    public partial class ProgressDialog : Form
    {
        ApplicationContext _appContext = new ApplicationContext();
        ManualResetEvent _jobCompleted = new ManualResetEvent(false);

        Point _ownerLocation;
        Size _ownerSize;
        int _ownerOffSet;

        public ProgressDialog()
        {
            InitializeComponent();
        }

        private void mainTimer_Tick(object sender, EventArgs e)
        {
            if(mainProgressBar.Value >= mainProgressBar.Maximum)
            {
                mainProgressBar.Value = 1;
            }
            mainProgressBar.PerformStep();
        }

        private void ProgressDialog_Load(object sender, EventArgs e)
        {
            mainProgressBar.Value = 1;
            mainTimer.Start();
        }

        protected override void OnShown(EventArgs e)
        {
            
            Point p = new Point(_ownerLocation.X + _ownerSize.Width / 2 - Width / 2 + _ownerOffSet, _ownerLocation.Y + _ownerSize.Height / 2 - Height / 2 + _ownerOffSet);
            this.Location = p;

            if (Debugger.IsAttached == true)
            {
                this.Hide();
            }

            base.OnShown(e);
        }

        public static ProgressDialog Run(Action action, Form parent, string title, string format, params object[] args)
        {
            ProgressDialog progressBar = Run(parent, title, format, args);
            if (progressBar != null)
            {
                progressBar.Continue(action);
            }
            return progressBar;
        }

        public static ProgressDialog Run(Form parent, string title, string format, params object[] args)
        {
            ProgressDialog progressBar = null;
            ManualResetEvent _jobStart = new ManualResetEvent(false);

            string fullMessage = string.Format(format, args);
            Point ownerLoc = parent.Location;
            Size ownerSize = parent.Size;
            int offset = parent.OwnedForms.Length * 38;  // approx. 10mm

            Utility.TaskUtility.StartSTAThread(() =>
            {
                progressBar = new ProgressDialog();
                progressBar._ownerOffSet = offset;
                progressBar._ownerLocation = ownerLoc;
                progressBar._ownerSize = ownerSize;
                progressBar.Text = title;
                progressBar.infoLabel.Text = fullMessage;
                progressBar.Show();
                _jobStart.Set();
                Application.Run(progressBar._appContext);
                progressBar.Dispose();
                progressBar.Close();
            });

            _jobStart.WaitOne();
            _jobStart.Dispose();

            return progressBar;
        }

        public void Wait()
        {
            _jobCompleted.WaitOne();
        }

        public void Continue(Action action)
        {
            _jobCompleted.Reset();

            Task saveTask = new Task(() =>
            {
                action();
                _jobCompleted.Set();
            });

            saveTask.Start();
        }

        public void ShutDown()
        {
            _appContext.ExitThread();
        }

        protected override void WndProc(ref Message message)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MOVE = 0xF010;

            switch (message.Msg)
            {
                case WM_SYSCOMMAND:
                    int command = message.WParam.ToInt32() & 0xfff0;
                    if (command == SC_MOVE)
                        return;
                    break;
            }

            base.WndProc(ref message);
        }
    }
}
