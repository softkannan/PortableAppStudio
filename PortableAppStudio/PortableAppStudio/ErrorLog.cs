using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppStudio
{
    public class ErrorLog
    {
        private static ErrorLog _inst = new ErrorLog();
        public static ErrorLog Inst
        {
            get
            {
                return _inst;
            }
        }

        private ToolStripStatusLabel _mainStatusLabel;
        private ToolStripStatusLabel _rightStatusLabel;
        private MainStudio _mainForm;
        public delegate void SetLabelCallBack(ToolStripStatusLabel label, string str);

        private SetLabelCallBack _labelCallBack;

        public void Initialize(MainStudio mainForm,ToolStripStatusLabel mainStatusLabel,ToolStripStatusLabel rightStatusLabel)
        {
            _mainForm = mainForm;
            _mainStatusLabel = mainStatusLabel;
            _rightStatusLabel = rightStatusLabel;
            _labelCallBack = SetStatusLabel;
        }

        public bool ShowYesNo(string format, params object[] args)
        {
            string message = string.Format(format, args);
            return MessageBox.Show(message, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
        }

        public void LogError(string format,params object[] args)
        {
            string message = string.Format(format, args);
            MessageBox.Show(message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
        }

        public void ShowError(string format, params object[] args)
        {
            string message = string.Format(format, args);
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ShowInfo(string format, params object[] args)
        {
            string message = string.Format(format, args);
            MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void WriteStatus(string format, params object[] args)
        {
            var labelText = string.Format(format, args);
            if (_mainForm.InvokeRequired)
            {
                _mainForm.BeginInvoke(_labelCallBack, _mainStatusLabel, labelText);
            }
            else
            {
                _mainStatusLabel.Text = labelText;
            }
            _mainForm.Refresh();
        }

        private void SetStatusLabel(ToolStripStatusLabel label,string str)
        {
            label.Text = str;
        }

        public void WriteRightStatus(string format, params object[] args)
        {
            _rightStatusLabel.Text = string.Format(format, args);
        }
    }
}
