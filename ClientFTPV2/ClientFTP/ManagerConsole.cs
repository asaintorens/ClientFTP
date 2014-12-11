using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientFTP
{
  public  class ManagerConsole
    {
        
        RichTextBox laconsole;
        Form leForm;

        public void AppendText( string text, Color color)
        {

            MethodInvoker monInvoker = delegate
            {
                this.laconsole.SelectionStart = this.laconsole.TextLength;
                this.laconsole.SelectionLength = 0;

                this.laconsole.SelectionColor = Color.Black;
                this.laconsole.AppendText("\n" + DateTime.Now.ToShortTimeString()+":"+DateTime.Now.Second);
                this.laconsole.SelectionColor = color;
                this.laconsole.AppendText("\n" + text);
                
            };
            leForm.Invoke(monInvoker);
        }

        internal void setConsole(RichTextBox p_laconsole ,Form p_leForm)
        {
            this.laconsole = p_laconsole;
            this.leForm = p_leForm;
        }
    }
}
