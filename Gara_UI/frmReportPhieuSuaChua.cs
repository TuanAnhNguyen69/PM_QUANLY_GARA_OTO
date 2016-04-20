using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gara_UI
{
    public partial class frmReportPhieuSuaChua : Form
    {
        private string BienSo;
        public frmReportPhieuSuaChua(string BienSo)
        {
            this.BienSo = BienSo;
            InitializeComponent();
        }

        private void frmReportPhieuSuaChua_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DataSet1.PhieuSuaChua' table. You can move, or remove it, as needed.
            this.PhieuSuaChuaTableAdapter.Fill(this.DataSet1.PhieuSuaChua, BienSo);

            this.reportViewer1.RefreshReport();
        }
    }
}
