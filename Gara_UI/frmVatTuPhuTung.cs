using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Gara_BUS;
using Gara_DATA.GaRa_Info;
namespace Gara_UI
{
    public partial class frmVatTuPhuTung : Form
    {
        public frmVatTuPhuTung()
        {
            InitializeComponent();
            dateNgayNhap.Format = DateTimePickerFormat.Custom;
            dateNgayNhap.CustomFormat = "yyyy/MM/dd";
        }
        public int superTabControlIndex = -1;
        public event EventHandler OnClosing7;
        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (OnClosing7 != null)
                OnClosing7(this, e);
            Close();
        }
        private bool IsNumber(string s)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(s);
        }
        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            try
            {
                if (PhieuNhapVatTuPhuTungBUS.PhieuNhapVatTuPhuTung_KiemTra(txtMaPhieu.Text) == 0)
                {
                    VatTuPhuTung vtpt = new VatTuPhuTung();
                    PhieuNhapVatTuPhuTung pnvt = new PhieuNhapVatTuPhuTung();
                    CT_PhieuNhapVatTuPhuTung ctpn = new CT_PhieuNhapVatTuPhuTung();
                    pnvt.SoPhieuNhap = txtMaPhieu.Text;
                    pnvt.NgayNhap = dateNgayNhap.Text;
                    vtpt.MaVatTuPhuTung = cbMaVTPT.Text;
                    vtpt.TenVatTuPhuTung = txtTenVTPT.Text;
                    ctpn.SoPhieuNhap = txtMaPhieu.Text;
                    ctpn.MaVatTuPhuTung = cbMaVTPT.Text;
                    ctpn.DonGia = double.Parse(txtDonGia.Text);
                    ctpn.SoLuong = int.Parse(txtSoLuong.Text);
                    ctpn.ThanhTien = double.Parse(txtThanhTien.Text);
                    ctpn.TenVatTuPhuTung = txtTenVTPT.Text;
                    VatTuPhuTungBUS.VatTuPhuTung_Insert(vtpt);
                    PhieuNhapVatTuPhuTungBUS.PhieuNhapVatTuPhuTung_Insert(pnvt);
                    CT_PhieuNhapVatTuPhuTungBUS.CT_PhieuNhapVatTuPhuTung_Insert(ctpn);
                    MessageBox.Show("Thêm mới phiếu nhập vật tư thành công!");
                    tableDSVTPT.DataSource = PhieuNhapVatTuPhuTungBUS.PhieuNhapVatTuPhuTung_GetAll(dateNgayNhap.Text);
                    for (int i = 0; i < tableDSVTPT.Rows.Count; i++)
                    {
                        tableDSVTPT.Rows[i].HeaderCell.Value = (i + 1).ToString();
                    }

                }
                else
                    MessageBox.Show("Phiếu nhập vật tư đã có. Xin nhập phiếu khác !!!","Lỗi",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            btnXoa.Enabled = false;
            tableDSVTPT.Enabled = true;
        }

        private void TinhThanhTien()
        {
            if (txtSoLuong.Text != "" && txtDonGia.Text != "")
            {
                int soluong = int.Parse(txtSoLuong.Text);
                double dongia = double.Parse(txtDonGia.Text);
                double thanhtien = dongia * soluong;
                txtThanhTien.Text = thanhtien.ToString();
            }
        }

        private void txtDonGia_TextChanged(object sender, EventArgs e)
        {
            TinhThanhTien();
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            TinhThanhTien();
        }
        private void txtDonGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void btnKhoVT_Click(object sender, EventArgs e)
        {
            frmKhoVatTu f = new frmKhoVatTu();
            f.Show();
        }

        private void frmVatTuPhuTung_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'quanlygaraoto1.CT_PHIEUSUACHUA' table. You can move, or remove it, as needed.
            this.cT_PHIEUSUACHUATableAdapter.Fill(this.quanlygaraoto1.CT_PHIEUSUACHUA);
            // TODO: This line of code loads data into the 'quanlygaraoto1.sp_VatTuPhuTung_GetAll' table. You can move, or remove it, as needed.
            this.sp_VatTuPhuTung_GetAllTableAdapter.Fill(this.quanlygaraoto1.sp_VatTuPhuTung_GetAll);
            // TODO: This line of code loads data into the 'quanlygaraoto1.CT_PHIEUSUACHUA' table. You can move, or remove it, as needed.
            this.cT_PHIEUSUACHUATableAdapter.Fill(this.quanlygaraoto1.CT_PHIEUSUACHUA);
            // TODO: This line of code loads data into the 'quanlygaraoto1.sp_VatTuPhuTung_GetAll' table. You can move, or remove it, as needed.
            this.sp_VatTuPhuTung_GetAllTableAdapter.Fill(this.quanlygaraoto1.sp_VatTuPhuTung_GetAll);
            tableDSVTPT.DataSource = PhieuNhapVatTuPhuTungBUS.PhieuNhapVatTuPhuTung_GetAll(dateNgayNhap.Text);
            cbMaVTPT.DataSource = VatTuPhuTungBUS.VatTuPhuTung_GetAll();
            cbMaVTPT.DisplayMember = "mavattuphutung";
            cbMaVTPT.ValueMember = "DonGia";
            cbMaVTPT.Text = "";
            txtTenVTPT.Text = "";
            txtDonGia.Text = "";
        }
        private void loadMaPhieu()
        {
        }
       
        
        private void LoadVatTu()
        {
            tableDSVTPT.DataSource = VatTuPhuTungBUS.VatTuPhuTung_GetAll();
        }

        private void tableDSVTPT_Click(object sender, EventArgs e)
        {
            txtMaPhieu.DataBindings.Clear();
            txtMaPhieu.DataBindings.Add("text", tableDSVTPT.DataSource, "SoPhieuNhap");
            cbMaVTPT.DataBindings.Clear();
            cbMaVTPT.DataBindings.Add("text", tableDSVTPT.DataSource, "mavattuphutung");
            txtTenVTPT.DataBindings.Clear();
            txtTenVTPT.DataBindings.Add("text", tableDSVTPT.DataSource, "Tenvattuphutung");
            txtDonGia.DataBindings.Clear();
            txtDonGia.DataBindings.Add("text", tableDSVTPT.DataSource, "DonGia");
            txtSoLuong.DataBindings.Clear();
            txtSoLuong.DataBindings.Add("text", tableDSVTPT.DataSource, "SoLuong");
            txtThanhTien.DataBindings.Clear();
            txtThanhTien.DataBindings.Add("text", tableDSVTPT.DataSource, "ThanhTien");
            //dateNgayNhap.Text = tableDSVTPT.CurrentRow.Cells["NgayNhap"].Value.ToString();
        }

        private void Click_LamMoi(object sender, EventArgs e)
        {
            txtMaPhieu.Text = "";
            cbMaVTPT.Text = "";
            txtTenVTPT.Clear();
            txtDonGia.Clear();
            txtSoLuong.Clear();
            txtThanhTien.Clear();

            tableDSVTPT.Enabled = false;

            txtTenVTPT.ReadOnly = false;
            btnXoa.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                CT_PhieuNhapVatTuPhuTungBUS.CT_PhieuNhapVatTuPhuTung_Delete(txtMaPhieu.Text, cbMaVTPT.Text);
                tableDSVTPT.DataSource = PhieuNhapVatTuPhuTungBUS.PhieuNhapVatTuPhuTung_GetAll(dateNgayNhap.Text);
                MessageBox.Show("Đã Xóa ");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            txtMaPhieu.Text = "";
            cbMaVTPT.Text = "";
            txtTenVTPT.Clear();
            txtDonGia.Clear();
            txtSoLuong.Clear();
            txtThanhTien.Clear();
            btnXoa.Enabled = false;
        }

        private void cbMaVTPT_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDonGia.Text = cbMaVTPT.SelectedValue.ToString();

            txtTenVTPT.Text = VatTuPhuTungBUS.VatTuPhuTung_GetTenVatTu(cbMaVTPT.Text).ToString() ;

            txtTenVTPT.ReadOnly = true;
            
        }

        private void tableDSVTPT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnXoa.Enabled = true;
        }

       


        
       
    }
}
