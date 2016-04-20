using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gara_BUS;
using Gara_DATA.GaRa_Info;
namespace Gara_UI
{
    public partial class frmKhoVatTu : Form
    {
        public frmKhoVatTu()
        {
            InitializeComponent();
        }

        private void frmKhoVatTu_Load(object sender, EventArgs e)
        {
            tableKhoVatTu.DataSource = VatTuPhuTungBUS.VatTuPhuTung_GetAll();
        }
    }
}
