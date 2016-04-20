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
    public partial class frmBaoCaoTon : Form
    {
        public frmBaoCaoTon()
        {
            InitializeComponent();
        }
        public int superTabControlIndex = -1;
        private void btnLapBaoCao_Click(object sender, EventArgs e)
        {
            if (int.Parse(txtNam.Text) > DateTime.Now.Year)
            {
                MessageBox.Show("Năm lập báo cáo không được lớn hơn năm hiện tại");
            }
            else
            {
                if (int.Parse(txtNam.Text) == DateTime.Now.Year)
                {
                    if (int.Parse(cbThang.Text) > DateTime.Now.Month)
                    {
                        MessageBox.Show("Tháng lập báo cáo không được lớn hơn tháng hiện tại");
                    }
                    else
                    {
                        BAOCAOTONTableAdapter.Fill(DataSet1.BAOCAOTON);
                        //baocaotonTableAdapter1.Fill(dataSet11.BAOCAOTON);
                        rpvBaoCaoTon.RefreshReport();
                    }
                }
                else
                {
                    BAOCAOTONTableAdapter.Fill(DataSet1.BAOCAOTON);
                    //baocaotonTableAdapter1.Fill(dataSet11.BAOCAOTON);
                    rpvBaoCaoTon.RefreshReport();
                }

            }


        }

        private void frmBaoCaoTon_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DataSet1.BAOCAOTON' table. You can move, or remove it, as needed.
            this.BAOCAOTONTableAdapter.Fill(this.DataSet1.BAOCAOTON);

        }
        public event EventHandler OnClosing1;
        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (OnClosing1 != null)
                OnClosing1(this, e);
            Close(); 
        }
    }
}
