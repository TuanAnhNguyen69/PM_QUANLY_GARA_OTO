using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gara_DATA.GaRa_Info;
using Gara_BUS;
using System.Text.RegularExpressions;
namespace Gara_UI
{
    public partial class frmTienCong : Form
    {
        public frmTienCong()
        {
            InitializeComponent();
        }

        

        public int superTabControlIndex = -1;
        public event EventHandler OnClosing8;
        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (OnClosing8 != null)
                OnClosing8(this, e);
            Close();
            MainForm.ActiveForm.Opacity = 1;
        }

        private bool IsNumber(string s)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(s);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                TienCong tc = new TienCong();
                tc.NoiDung = txtNoiDung.Text;
                tc.GiaTienCong = double.Parse(txtTienCong.Text);
                TienCongBUS.TienCong_Insert(tc);
                MessageBox.Show("Thêm thành công!!!");
                tableDSTienCong.DataSource = TienCongBUS.TienCong_GetAll();
                txtNoiDung.Clear();
                txtTienCong.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            btnXoa.Enabled = false;
        }

        private void frmTienCong_Load(object sender, EventArgs e)
        {
            tableDSTienCong.DataSource = TienCongBUS.TienCong_GetAll();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                TienCong tc = new TienCong();
                tc.NoiDung = txtNoiDung.Text;
                tc.GiaTienCong = double.Parse(txtTienCong.Text);
                TienCongBUS.TienCong_Update(tc);
                MessageBox.Show("Sửa thành công!!!");
                tableDSTienCong.DataSource = TienCongBUS.TienCong_GetAll();
                txtNoiDung.Clear();
                txtTienCong.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            btnXoa.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                TienCong tc = new TienCong();
                tc.NoiDung = txtNoiDung.Text;
                TienCongBUS.TienCong_Delete(tc);
                MessageBox.Show("Xóa thành công!!!");
                tableDSTienCong.DataSource = TienCongBUS.TienCong_GetAll();
                txtNoiDung.Clear();
                txtTienCong.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            btnXoa.Enabled = false;
        }

        private void tableDSTienCong_Click(object sender, EventArgs e)
        {
            try
            {
                txtTienCong.DataBindings.Clear();
                txtTienCong.DataBindings.Add("text", tableDSTienCong.DataSource, "dongia");
                txtNoiDung.DataBindings.Clear();
                txtNoiDung.DataBindings.Add("text", tableDSTienCong.DataSource, "NoiDungSuaChua");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtGiaTri_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void tableDSTienCong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnXoa.Enabled = true;
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            txtNoiDung.Text = "";
            txtTienCong.Text = "";
            btnXoa.Enabled = false;
        }

        
       
    }
}
