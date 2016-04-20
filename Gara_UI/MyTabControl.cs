using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gara_UI
{
    public class MyTabControl: TabPage
        {
            private Form frm;
            public void MyTagControl(MyFormPage frm_contensido)
            {
                this.frm = frm_contensido;
                this.Controls.Add(frm_contensido.pnl);
                this.Text = frm_contensido.Text;
            }
            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    frm.Dispose();
                }
                base.Dispose(disposing);
            }
        }
}
