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
    public partial class frmBaoCaoDoanhSo : Form
    {
        public frmBaoCaoDoanhSo()
        {
            InitializeComponent();
        }
        public int superTabControlIndex = -1;
        private void frmBaoCaoDoanhSo_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DataSet1.DoanhSo' table. You can move, or remove it, as needed.
            this.DoanhSoTableAdapter.Fill(this.DataSet1.DoanhSo);

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (OnClosing2 != null)
                OnClosing2(this, e);
            Close();
        }
        public event EventHandler OnClosing2;

        private void btnLapBaoCao_Click(object sender, EventArgs e)
        {
            try
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
                            this.DoanhSoTableAdapter.FillByDate(DataSet1.DoanhSo, int.Parse(cbThang.Text), int.Parse(txtNam.Text));
                            this.rpvBaoCaoDoanhSo.RefreshReport();
                        }
                    }
                    else
                    {
                        this.DoanhSoTableAdapter.FillByDate(DataSet1.DoanhSo, int.Parse(cbThang.Text), int.Parse(txtNam.Text));
                        this.rpvBaoCaoDoanhSo.RefreshReport();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
