
using MobileNotification.WinForm.Model.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MobileNotification.WinForm
{
    public class BaseForm : Form
    {
        public event Action<Action, EventArgs<Exception>  > OnException;

        public void Set(Control ctrl, Action action)
        {
            Invoke(ctrl, action);
        }
       
        public void Invoke(Control ctrl, Action action)
        {
            if (ctrl.InvokeRequired)
            {
                ctrl.Invoke(action);
            }
            else
            {
                action();
            }
        }
          
        public void BeginInvoke(Control ctrl, Action action)
        {
            ctrl.BeginInvoke(action);
        }

        public void Run(Action action)
        {
            if (action != null)
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    OnException?.Invoke(action, ex);
                }
            }
        }

        public string GetMethodName(Action action)
        {
            var currentMethod = action.Method;
            var fullMethodName = "";

            if (currentMethod != null)
            {
                fullMethodName = currentMethod.DeclaringType.FullName + "." + currentMethod.Name;
            }

            return fullMethodName;
        }

        public void SetText(ToolStripItem label, string text)
        {
            label.Text = text;
        }

        public void SetEnabled(ToolStripItem label, bool value)
        {
            label.Enabled = value;
        }
    }
}
