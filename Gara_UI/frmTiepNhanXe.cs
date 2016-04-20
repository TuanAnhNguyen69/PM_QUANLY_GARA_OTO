using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Gara_DATA.GaRa_Info;
using Gara_BUS;
using Gara_DATA;
using DevComponents.DotNetBar.Controls;
using System.Timers;
namespace Gara_UI
{
    public partial class frmTiepNhanXe : MyFormPage
    {
        public int superTabControlIndex = -1;

        public frmTiepNhanXe()
        {
            InitializeComponent();
            this.pnl = tableLayoutPanel1;
            dateNgayTiepNhan.Format = DateTimePickerFormat.Custom;
            dateNgayTiepNhan.CustomFormat = "yyyy/MM/dd";
        }

        private void frmTiepnhanxe_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'quanlygaraoto1.sp_Xe_DanhSach' table. You can move, or remove it, as needed.
            this.sp_Xe_DanhSachTableAdapter.Fill(this.quanlygaraoto1.sp_Xe_DanhSach);
            try
            {
                cbHieuXe.DataSource = HieuXeBUS.HieuXe_GetHieuXe();
                cbHieuXe.DisplayMember = "HieuXe";
                cbHieuXe.ValueMember = "HieuXe";
                LoadDSXeSuaTrongNgay(dateNgayTiepNhan.Text);
                

                sp_Xe_DanhSachTableAdapter.Fill(quanlygaraoto1.sp_Xe_DanhSach);
                tableTiepNhanXe.DataSource = XeBUS.Xe_DanhSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                SQLDataProvider.connection.Close();
            }
        }
        private void SoXeTiepNhanTrongNgay(string Ngay)
        {

            int count = XeBUS.Xe_DemDSTheoNgay(dateNgayTiepNhan.Text);
            if (count >= XeBUS.SoXeSuaChuaTrongNgay())
            {
                MessageBox.Show("Số xe tiếp nhận trong ngày đã đủ");
            }

        }
        private void LoadDSXeSuaTrongNgay(string Ngay)
        {
            try
            {
                tableTiepNhanXe.DataSource = XeBUS.Xe_DSTheoNgay(Ngay);
                SoXeTiepNhanTrongNgay(Ngay);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi");
            }
        }


        public Control pnl { get; set; }

        private bool KiemTra() 
        {
            if (txtTenChuXe.Text.Trim().Equals(""))
            {
                MessageBox.Show("Tên chủ xe chưa có !!!");
                txtTenChuXe.Focus();
                return false;
            }
            if (txtBienSo.Text.Trim().Equals(""))
            {
                MessageBox.Show("Biển số chưa có !!!");
                txtBienSo.Focus();
                return false;
            }
            if (cbHieuXe.Text.Trim().Equals(""))
            {
                MessageBox.Show("Hiệu xe chưa được chọn !!!");
                cbHieuXe.Focus();
                return false;
            }
            if (cbHieuXe.FindStringExact(cbHieuXe.Text) == -1)
            {
                MessageBox.Show("Hiệu xe không có trong gara!!");
                cbHieuXe.Focus();
                return false;
            }
            if (txtDiaChi.Text.Trim().Equals(""))
            {
                MessageBox.Show("Địa chỉ chưa có!!!");
                txtDiaChi.Focus();
                return false;
            }
            if (txtDienThoai.Text.Trim().Equals(""))
            {
                MessageBox.Show("Chưa nhập số điện thoại!!!");
                txtDienThoai.Focus();
                return false;
            }
            if (txtEmail.Text.Trim().Equals(""))
            {
                MessageBox.Show("Chưa nhập email !!!");
                txtEmail.Focus();
                return false;
            }
            DataTable dt = XeBUS.Xe_DanhSach();
            DataView dv = dt.DefaultView;
            dv.Sort = "BienSo";
            if (dv.Find(txtBienSo.Text) != -1)
            {
                MessageBox.Show("Biển số đã có !");
                txtBienSo.Focus();
                return false;
            }
            if (XeBUS.Xe_DemDSTheoNgay(dateNgayTiepNhan.Text) >= XeBUS.SoXeSuaChuaTrongNgay())
            {
                MessageBox.Show(string.Format("Số xe đã tiếp nhận đủ {0}chiếc", XeBUS.SoXeSuaChuaTrongNgay()), "Thông báo");
                return false;
            }
            return true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (KiemTra())
            {
                Xe xe = new Xe();
                try
                {
                    xe.TenChuXe = txtTenChuXe.Text;
                    xe.BienSo = txtBienSo.Text;
                    xe.DiaChi = txtDiaChi.Text;
                    xe.Email = txtEmail.Text;
                    xe.DienThoai = txtDienThoai.Text;
                    xe.HieuXe = cbHieuXe.SelectedValue.ToString();
                    xe.NgayTiepNhan = dateNgayTiepNhan.Text;
                    //xe.TienNo = 0;
                    XeBUS.Xe_Insert(xe);
                    tableTiepNhanXe.DataSource = XeBUS.Xe_DanhSach();
                    //tableTiepNhanXe.DataSource = XeBUS.Xe_DSTheoNgay(dateNgayTiepNhan.Text);
                   // tableTiepNhanXe.DataSource = XeBUS.Xe_DSTheoNgay(xe.NgayTiepNhan.ToString());
                    MessageBox.Show("Nhập thành công !!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    SQLDataProvider.connection.Close();
                }
                btnXoa.Enabled = false;
                //btnThemMoi.Enabled = true;
            }
        }

        private void txtDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void DeleteLast(ref TextBoxX txt)
        {
            foreach (char i in txt.Text)
            {
                string j = Convert.ToString(i);
                if (IsNumber(j) == true)
                {
                    MessageBox.Show("Tên không hợp lệ!");
                    txt.Text = txt.Text.Substring(0, txt.TextLength - 1);
                }
            }
        }
        private bool IsNumber(string s)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(s);
        }
        private void txtTenChuXe_TextChanged_1(object sender, EventArgs e)
        {
            DeleteLast(ref txtTenChuXe);
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            txtBienSo.Text = "";
            txtDiaChi.Text = "";
            txtDienThoai.Text = "";
            txtEmail.Text = "";
            txtTenChuXe.Text = "";
            if (XeBUS.Xe_DemDSTheoNgay(dateNgayTiepNhan.Text) >= XeBUS.SoXeSuaChuaTrongNgay())
            {
                MessageBox.Show(string.Format("Số xe đã tiếp nhận đủ {0}chiếc", XeBUS.SoXeSuaChuaTrongNgay()), "Thông báo");
                
            }
            btnXoa.Enabled = false;
        }

        public event EventHandler OnClosing;
        private void btnThoat_Click(object sender, EventArgs e)
        {
            // MainForm.superTabControl1.Close();
            if (OnClosing != null)
                OnClosing(this, e);
            Close();
        }



        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                Xe xe = new Xe() { BienSo = txtBienSo.Text };
                XeBUS.Xe_Delete(xe);
                MessageBox.Show("Xóa thành công!!");
                tableTiepNhanXe.DataSource = XeBUS.Xe_DanhSach();
                txtBienSo.Text = "";
                txtDiaChi.Text = "";
                txtDienThoai.Text = "";
                txtEmail.Text = "";
                txtTenChuXe.Text = "";
                cbHieuXe.SelectedIndex = 0;
                txtBienSo.Focus();
            }
            catch (Exception)
            {
                MessageBox.Show("Không thể xóa xe đang sửa chữa!","Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            btnXoa.Enabled = false;
        }

        private void tableTiepNhanXe_Click(object sender, EventArgs e)
        {
            
            txtBienSo.DataBindings.Clear();
            txtBienSo.DataBindings.Add("text", tableTiepNhanXe.DataSource, "BienSo");
            txtTenChuXe.DataBindings.Clear();
            txtTenChuXe.DataBindings.Add("text", tableTiepNhanXe.DataSource, "TenChuXe");
            txtDiaChi.DataBindings.Clear();
            txtDiaChi.DataBindings.Add("text", tableTiepNhanXe.DataSource, "DiaChi");
            txtDienThoai.DataBindings.Clear();
            txtDienThoai.DataBindings.Add("text", tableTiepNhanXe.DataSource, "DienThoai");
            txtEmail.DataBindings.Clear();
            txtEmail.DataBindings.Add("text", tableTiepNhanXe.DataSource, "Email");
            cbHieuXe.DataBindings.Clear();
            cbHieuXe.DataBindings.Add("text", tableTiepNhanXe.DataSource, "HieuXe");
            dateNgayTiepNhan.DataBindings.Clear();
            dateNgayTiepNhan.DataBindings.Add("text", tableTiepNhanXe.DataSource, "NgayTiepNhan");
            //dateNgayTiepNhan.Text = tableTiepNhanXe.CurrentRow.Cells["NgayTiepNhan"].Value.ToString();
            bmb = BindingContext["BienSo"];
            btnThemMoi.Enabled = true;
        }
        private BindingManagerBase bmb;

        private void tableTiepNhanXe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnXoa.Enabled = true;
        }

       
       
        


        // dinh nghia 1 su kien
        // su kien nay xay ra khi tat form


    }
}
