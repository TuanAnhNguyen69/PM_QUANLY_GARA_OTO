using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Gara_BUS;
using Gara_DATA.GaRa_Info;
namespace Gara_UI
{
    public partial class frmPhieuThuTien : MyFormPage
    {
        public frmPhieuThuTien()
            
        {
            InitializeComponent();
            dateNgayThuTien.Format = DateTimePickerFormat.Custom;
            dateNgayThuTien.CustomFormat = "dd/MM/yyyy";
            //txtSoTienThu.ToString("{0:#,0.#} VNÐ");
            //string.Format("{0:C}", txtSoTienThu.ToString());
        }

        public int superTabControlIndex = -1;
        private void frmPhieuThuTien_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DataSet1.PhieuThuTien' table. You can move, or remove it, as needed.
            this.PhieuThuTienTableAdapter.Fill(this.DataSet1.PhieuThuTien, cbBienSo.Text);
           
            cbBienSo.DataSource = XeBUS.Xe_DanhSach();
            cbBienSo.DisplayMember = "BienSo";
            cbBienSo.ValueMember = "BienSo";
           // cbBienSo.SelectedIndexChanged = cbBienSo_SelectedIndexChanged(sender, e);
            cbBienSo.SelectedIndexChanged += cbBienSo_SelectedIndexChanged;
            //btnLapPhieu.Enabled = false;
            btnIn.Enabled = false;
            
        }

        public event EventHandler OnClosing5;
        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (OnClosing5 != null)
                OnClosing5(this, e);
            Close();
        }

        private void cbBienSo_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnIn.Enabled = true;
           // btnLapPhieu.Enabled = true;
            DataTable dtb = new DataTable();
            dtb = XeBUS.Xe_GetBienSo(cbBienSo.Text);
            foreach(DataRow row in dtb.Rows)
            {
                txtTenChuXe.Text = row["TenChuXe"].ToString();
                txtDienThoai.Text = row["DienThoai"].ToString();
                txtDiaChi.Text = row["DiaChi"].ToString();
                txtEmail.Text = row["Email"].ToString();
                txtTienNo.Text = row["TienNo"].ToString();
               
            }
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            txtMaPhieuThu.Text = "";
            txtTenChuXe.Text = "";
            txtDienThoai.Text = "";
            txtEmail.Text = "";
            txtDiaChi.Text = "";
            txtTienNo.Text = "";
            txtSoTienThu.Text = "";
        }

        private void btnLapPhieu_Click(object sender, EventArgs e)
        {
            if (txtMaPhieuThu.Text == "")
            {
               MessageBox.Show("Chưa có mã phiếu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
               txtMaPhieuThu.Focus();
            }
            else if (txtSoTienThu.Text == "")
            {
                MessageBox.Show("Chưa nhập số tiền thu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSoTienThu.Focus();
            }
            else
            {
                try
                {
                    PhieuThuTien pt = new PhieuThuTien() { BienSo = cbBienSo.Text, MaPhieuThuTien = txtMaPhieuThu.Text, /*pt.NgayThuTien = dateNgayThuTien.Value.ToShortDateString();*/ /*pt.NgayThuTien = dateNgayThuTien.Value.ToString();*/NgayThuTien = dateNgayThuTien.Text, SoTienThu = double.Parse(txtSoTienThu.Text) };
                    string[] s = txtTienNo.Text.Split(',');
                    if (double.Parse(txtSoTienThu.Text) > double.Parse(s[0]))
                    {
                        MessageBox.Show("Số tiền thu phải nhỏ hơn số tiền nợ");
                        txtSoTienThu.Text = "";
                    }
                    PhieuThuTienBUS.PhieuThuTien_Insert(pt);
                    MessageBox.Show("Lập phiếu thành công!!");
                    DataTable dtb = new DataTable();
                    dtb = XeBUS.Xe_GetBienSo(cbBienSo.Text);

                    foreach (DataRow r in dtb.Rows)
                    {
                        txtTienNo.Text = r["TienNo"].ToString();
                    }
                    btnIn.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void txtSoTienThu_TextChanged(object sender, EventArgs e)
        {
            if (txtSoTienThu.Text != "" && txtTienNo.Text != "")
            {
                if (double.Parse(txtSoTienThu.Text) > double.Parse(txtTienNo.Text))
                {
                    MessageBox.Show("Số tiền thu phải nhỏ hơn tiền nợ !");
                    txtSoTienThu.Clear();
                    txtSoTienThu.Focus();
                }
            }
        }

        private void txtSoTienThu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            try
            {

                this.PhieuThuTienTableAdapter.FillByBienSo(DataSet1.PhieuThuTien, cbBienSo.Text);
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
