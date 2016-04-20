using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Gara_BUS;
using Gara_DATA.GaRa_Info;
using Gara_DATA;

namespace Gara_UI
{
    public partial class frmTimKiem : MyFormPage
    {
        public frmTimKiem()
        {
            InitializeComponent();
            dateNgayTiepNhan.Format = DateTimePickerFormat.Custom;
            dateNgayTiepNhan.CustomFormat = "dd/MM/yyyy";   
            pnl = tableLayoutPanel1;
            rbt_TenChuXe.Checked = true;
            //cb_TimKiem.SelectedIndex = 1;
        }

        private void frmTimKiem_Load(object sender, EventArgs e)
        {
            cbHieuXe.DataSource = HieuXeBUS.HieuXe_GetHieuXe();
            cbHieuXe.DisplayMember = "HieuXe";
            cbHieuXe.ValueMember = "HieuXe";
            cb_TimKiem.DataSource = XeBUS.Xe_DanhSach();
            cb_TimKiem.DisplayMember = "TenChuXe";
            cb_TimKiem.ValueMember = "TenChuXe";
            SQLDataProvider.connection.Close();
        }
        bool kiemtra = false;
        private void MyTextChange(ComboBox combobox, EventArgs e)
        {
            if(combobox.Text != "" && !kiemtra)
            {
                string s = combobox.Text;
                int index = combobox.FindString(s);
                if(index != -1)
                {
                    combobox.SelectedIndex = index;
                }
                combobox.SelectionStart = s.Length;
                combobox.SelectionLength = combobox.Text.Length - combobox.SelectionStart;
            }
        }
        private void MyKeyPress(ComboBox combobox, KeyPressEventArgs e)
        {
            if(e.KeyChar == (int)Keys.Escape)
            {
                combobox.SelectedIndex = -1;
                combobox.Text = "";
                kiemtra = true;
            }
            else if(char.IsControl(e.KeyChar))
            {
                kiemtra = true;
            }
            else
                kiemtra = false;
        }

        private void rbt_BienSo_CheckedChanged(object sender, EventArgs e)
        {
            cb_TimKiem.DataSource = XeBUS.Xe_GetAllBienSo();
            cb_TimKiem.DisplayMember = "BienSo";
            cb_TimKiem.ValueMember = "BienSo";
        }

        private void rbt_TenChuXe_CheckedChanged(object sender, EventArgs e)
        {
            cb_TimKiem.DataSource = XeBUS.Xe_GetAllTen();
            cb_TimKiem.DisplayMember = "TenChuXe";
            cb_TimKiem.ValueMember = "TenChuXe";
        }

        private void rbt_HieuXe_CheckedChanged(object sender, EventArgs e)
        {
            cb_TimKiem.DataSource = HieuXeBUS.HieuXe_GetHieuXe();
            cb_TimKiem.DisplayMember = "HieuXe";
            cb_TimKiem.ValueMember = "HieuXe";
        }

        private void cb_TimKiem_KeyPress(object sender, KeyPressEventArgs e)
        {
            MyKeyPress(cb_TimKiem, e);
        }

        private void cb_TimKiem_TextChanged(object sender, EventArgs e)
        {
            MyTextChange(cb_TimKiem, e);
        }
        private void ReLoad()
        {
            if(rbt_BienSo.Checked == true)
            {
                table_DanhSach.DataSource = XeBUS.Xe_GetBienSo(cb_TimKiem.Text);
            }
            if(rbt_TenChuXe.Checked == true)
            {
                table_DanhSach.DataSource = XeBUS.Xe_GetTenChuXe(cb_TimKiem.Text);
            }
            if(rbt_HieuXe.Checked == true)
            {

                table_DanhSach.DataSource = XeBUS.Xe_GetHieuXe(cb_TimKiem.Text);
                if (table_DanhSach.Rows[0].Cells[0].Value == null)
                {
                    MessageBox.Show("Chưa tiếp nhận hiệu xe này!!!", "Chọn lại!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            cb_TimKiem.DataSource = XeBUS.Xe_DanhSach();
           
        }
        private void btn_Tim_Click(object sender, EventArgs e)
        {
            if (cb_TimKiem.FindStringExact(cb_TimKiem.Text) == -1)
            {
                MessageBox.Show("Tên, Hiệu xe hoặc Biển số không đúng!!");

            }
            else
            {
                ReLoad();
            }
        }
        private bool KT()
        {
            if(txtEmail.Text.Trim().Equals(""))
            {
                MessageBox.Show("Tên chủ xe không được để trống!!!");
                txtEmail.Focus();
                return false;
            }
            if (txtBienSo.Text.Trim().Equals(""))
            {
                MessageBox.Show("Biển số không được để trống!");
                txtBienSo.Focus();
                return false;
            }
            if (cbHieuXe.Text.Trim().Equals(""))
            {
                MessageBox.Show("Hiệu xe chưa được chọn!");
                cbHieuXe.Focus();
                return false;
            }
            if (cbHieuXe.FindStringExact(cbHieuXe.Text) == -1)
            {
                MessageBox.Show("Hiệu xe không có trong Gara!");
                cbHieuXe.Focus();
                return false;
            }
            if (txtDiaChi.Text.Trim().Equals(""))
            {
                MessageBox.Show("Địa chỉ không được để trống!");
                txtDiaChi.Focus();
                return false;
            }
            if (txtDienThoai.Text.Trim().Equals(""))
            {
                MessageBox.Show("Điện thoại không được để trống!");
                txtDienThoai.Focus();
                return false;
            }
            if (txtTenChuXe.Text.Trim().Equals(""))
            {
                MessageBox.Show("Email không được để trống!");
                txtTenChuXe.Focus();
                return false;
            }
            if (cb_TimKiem.FindStringExact(txtBienSo.Text) != -1)
            {
                MessageBox.Show("Biển số đã có!");
                txtBienSo.Focus();
                return false;
            }
            return true;
        }
        private void LoadDLtuGrid()
        {
            txtBienSo.DataBindings.Clear();
            txtBienSo.DataBindings.Add("text", table_DanhSach.DataSource, "BienSo");
            txtTenChuXe.DataBindings.Clear();
            txtTenChuXe.DataBindings.Add("text", table_DanhSach.DataSource, "TenChuXe");
            txtDiaChi.DataBindings.Clear();
            txtDiaChi.DataBindings.Add("text", table_DanhSach.DataSource, "DiaChi");
            txtDienThoai.DataBindings.Clear();
            txtDienThoai.DataBindings.Add("text", table_DanhSach.DataSource, "DienThoai");
            txtTienNo.DataBindings.Clear();
            txtTienNo.DataBindings.Add("text", table_DanhSach.DataSource, "TienNo");
            txtEmail.DataBindings.Clear();
            txtEmail.DataBindings.Add("text", table_DanhSach.DataSource, "Email");
            cbHieuXe.DataBindings.Clear();
            cbHieuXe.DataBindings.Add("text", table_DanhSach.DataSource, "HieuXe");
            dateNgayTiepNhan.DataBindings.Clear();
            dateNgayTiepNhan.DataBindings.Add("text", table_DanhSach.DataSource, "NgayTiepNhan");
            //dateNgayTiepNhan.Text = table_DanhSach.CurrentRow.Cells["NgayTiepNhan"].Value.ToString();
        }
        private void btn_capnhat_Click(object sender, EventArgs e)
        {
                Xe xe = new Xe();
                try 
                {
                    xe.BienSo = txtBienSo.Text;
                    xe.DiaChi = txtDiaChi.Text;
                    xe.DienThoai = txtDienThoai.Text;
                    xe.Email = txtEmail.Text;
                    xe.HieuXe = cbHieuXe.Text;
                    xe.NgayTiepNhan = dateNgayTiepNhan.Text; 
                    xe.TenChuXe = txtTenChuXe.Text;
                    xe.TienNo = double.Parse(txtTienNo.Text);
                    XeBUS.Xe_Update(xe);
                    MessageBox.Show("Cập nhật thành công!!!", "Thông Báo");
                    ReLoad();
                    txtBienSo.Text = "";
                    txtDiaChi.Text = "";
                    txtDienThoai.Text = "";
                    txtEmail.Text = "";
                    txtTenChuXe.Text = "";
                    txtTienNo.Text = "";
                    
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }
        private void txtDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtTienNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        public int superTabControlIndex = -1;
        public event EventHandler OnClosing6;
        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (OnClosing6 != null)
                OnClosing6(this, e);
            Close();
        }

        private void cb_TimKiem_SelectedIndexChanged(object sender, EventArgs e)
        {
            cb_TimKiem.DataSource = XeBUS.Xe_DanhSach();
        }
        

        private void table_DanhSach_Click(object sender, EventArgs e)
        {
            LoadDLtuGrid();
        }

        private void table_DanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnCapNhat.Enabled = true;
        }

        private void table_DanhSach_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

       
    }
}

