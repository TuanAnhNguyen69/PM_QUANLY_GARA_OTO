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
    public partial class frmHieuXe : Form
    {
        public frmHieuXe()
        {
            InitializeComponent();
        }
        public int superTabControlIndex = -1;
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtTenHieuXe.Text != "")
            {
                try
                {
                    HieuXe hx = new HieuXe() { TenHieuXe = txtTenHieuXe.Text };
                    HieuXeBUS.HieuXe_Insert(hx);
                    MessageBox.Show("Thêm thành công");
                    dgvDanhSachHieuXe.DataSource = HieuXeBUS.HieuXe_GetHieuXe();
                    txtTenHieuXe.Text = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Chưa nhập tên hiệu xe!", "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            btnXoa.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try 
            {
                HieuXe hx = new HieuXe() { TenHieuXe = txtTenHieuXe.Text };
                HieuXeBUS.HieuXe_Delete(hx);
                MessageBox.Show("Xóa thành công");
                dgvDanhSachHieuXe.DataSource=HieuXeBUS.HieuXe_GetHieuXe();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            btnXoa.Enabled = false;
            txtTenHieuXe.Text = "";
        }

        private void frmHieuXe_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'quanlygaraoto1.sp_HieuXe_GetHieuXe' table. You can move, or remove it, as needed.
            this.sp_HieuXe_GetHieuXeTableAdapter.Fill(this.quanlygaraoto.sp_HieuXe_GetHieuXe);
            dgvDanhSachHieuXe.DataSource = HieuXeBUS.HieuXe_GetHieuXe();
        }

        private void dgvDanhSachHieuXe_Click(object sender, EventArgs e)
        {
            txtTenHieuXe.DataBindings.Clear();
            txtTenHieuXe.DataBindings.Add("text", dgvDanhSachHieuXe.DataSource, "HieuXe");
        }

        public event EventHandler OnClosing3;        

        private void btnThoat_Click_1(object sender, EventArgs e)
        {
            if (OnClosing3 != null)
                OnClosing3(this, e);
            Close();
        }

        private void dgvDanhSachHieuXe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnXoa.Enabled = true;
        }

    }
}
