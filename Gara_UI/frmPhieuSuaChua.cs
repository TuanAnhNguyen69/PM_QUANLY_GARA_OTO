using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Gara_BUS;
using Gara_DATA.GaRa_Info;
using Gara_DATA;

namespace Gara_UI
{
    public partial class frmPhieuSuaChua : Form
    {
        public frmPhieuSuaChua()
        {
            InitializeComponent();
            dtpNgaySuaChua.Format = DateTimePickerFormat.Custom;
        }

        public int superTabControlIndex = -1;

        public event EventHandler OnClosing4;
        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (OnClosing4 != null)
                OnClosing4(this, e);
            Close();
        }

        private void btnQLVTPT_Click(object sender, EventArgs e)
        {
            frmVatTuPhuTung f = new frmVatTuPhuTung() { FormBorderStyle = FormBorderStyle.Sizable };
            
            f.Show();
            f.ShowIcon = false;
            f.ControlBox = false;
        }

        private void btnQLTienCong_Click(object sender, EventArgs e)
        {
            frmTienCong f = new frmTienCong() { FormBorderStyle = FormBorderStyle.Sizable };
            
            f.Show();
            f.ShowIcon = false;
            f.ControlBox = false;
        }



        private bool KTNhapXuat()
        {

            if (txtMaPhieu.Text == "")
            {
                MessageBox.Show("Không để trống Mã Sửa Chữa");
                txtMaPhieu.Focus();
                return false;
            }
            if (txtSoLuong.Text == "")
            {
                MessageBox.Show("Chọn số lượng vật tư");
                txtSoLuong.Focus();
                return false;
            }
                     
            if (txtDonGia.Text == "")
            {
                MessageBox.Show("Chọn sử dụng loại vật tư nào");
                cbVTPT.Focus();
                return false;
            }
            if (txtTienCong.Text == "")
            {
                MessageBox.Show("Chọn nội dung sửa chữa");
                cbNoiDung.Focus();
                return false;
            }

            return true;
        }

        public double DonGia { get; set; }
        public string MaVatTuPhuTung { get; set; }
        public string NoiDungSuaChua { get; set; }
        public double TienCong { get; set; }
        private void frmPhieuSuaChua_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.PhieuSuaChua' table. You can move, or remove it, as needed.
            this.phieuSuaChuaTableAdapter.Fill(this.dataSet1.PhieuSuaChua, cbBienSo.Text);
            cbBienSo.DataSource = XeBUS.Xe_DanhSach();
            cbBienSo.DisplayMember = "BienSo";
            cbBienSo.ValueMember = "BienSo";

            cbVTPT.DataSource = VatTuPhuTungBUS.VatTuPhuTung_GetAll();
            cbVTPT.DisplayMember = "MaVatTuPhuTung";
            cbVTPT.ValueMember = "DonGia";

            cbNoiDung.DataSource = TienCongBUS.TienCong_GetAll();
            cbNoiDung.DisplayMember = "NoiDungSuaChua";
            cbNoiDung.ValueMember = "DonGia";

            cbNoiDung.Text = "";
            cbVTPT.Text = "";
            lbcSoLuongTon.Text = "";
            txtSoLuong.Text = "";

            this.cbBienSo.SelectedIndexChanged += new System.EventHandler(this.cbBienSo_SelectedIndexChanged);
            this.cbNoiDung.SelectedIndexChanged += new System.EventHandler(this.cbNoiDung_SelectedIndexChanged);
            this.cbVTPT.SelectedIndexChanged += new System.EventHandler(this.cbVTPT_SelectedIndexChanged);
        }

        private bool KTMaSuaChua(string masuachua)
        {
            bool kt = true;
            try
            {
                SqlDataReader dr = PhieuSuaChuaBUS.PhieuSuaChua_KTMaSuaChua();
                while (dr.Read())
                {
                    string msc = "";
                    msc = dr[0].ToString();
                    if (msc == masuachua)
                    {
                        kt = false;

                        return kt;
                    }
                    else if (msc != masuachua)
                    {
                        kt = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                SQLDataProvider.connection.Close();
            }

            return kt;
        }

        private bool KTnoidungsuachua(string msc, string noidung)
        {
            bool kt = false;
            if (CT_PhieuSuaChuaBUS.CT_PhieuSuaChua_KiemTra(msc, noidung).Rows.Count == 0)
            {
                kt = true;
                SQLDataProvider.connection.Close();
            }
            else
            {
                kt = false;
            }
            return kt;
        }

        private void DoanhSo_Insert()
        {
            DateTime z = new DateTime();
            z = Convert.ToDateTime(dtpNgaySuaChua.Text);
            int year = z.Year;
            int month = z.Month;
            string s = string.Format("DS{0}-{1}", year, month);
            double SumAll = 0;
            DataTable dt = BaoCaoDoanhSoBUS.BaoCaoDoanhSo_GetDuLieu(month, year);
            foreach (DataRow r in dt.Rows)
            {
                SumAll += double.Parse(r["TongTien"].ToString());
                //SumAll += double.Parse(r["ThanhTien"].ToString());
                //MessageBox.Show(r["TongTien"].ToString());
            }
            BaoCaoDoanhSoBUS.BaoCaoDoanhSo_Insert(month, year, SumAll);
            foreach (DataRow r in dt.Rows)
            {
                float f = ((float.Parse(r["TongTien"].ToString())) * 100 / ((float)SumAll));
                string hx = r["HieuXe"].ToString();
                int count = int.Parse(r["SoLuotSua"].ToString());
                double tt = double.Parse(r["ThanhTien"].ToString());
                
                //kiem tra
                //int kt = BaoCaoDoanhSoBUS.BaoCaoDoanhSo_Kiemtra(s, r["HieuXe"].ToString());
                //if (kt == 0)
                {
                    CT_BaoCaoDoanhSoBUS.CT_BaoCaoDoanhSo_Insert(month, year, hx, count, tt, f);
                }
                //else
                //{
                //    CT_BaoCaoDoanhSoBUS.CT_BaoCaoDoanhSo_Update(s, hx, count, tt, f);
                //}
            }


        }
        public void LoadSoxe(TextBox t1)
        {
            t1.Text = cbBienSo.Text;

        }

        private void btnLapPhieu_Click(object sender, EventArgs e)
        {
            try
            {
                if (KiemTraSoLuongTon())
                {
                    if (KTMaSuaChua(txtMaPhieu.Text))

                        {
                            if (KTNhapXuat())
                            {
                                PhieuSuaChua psc = new PhieuSuaChua();
                                CT_PhieuSuaChua ct_psc = new CT_PhieuSuaChua();

                                psc.MaPhieuSuaChua = txtMaPhieu.Text;
                                psc.NgaySuaChua = dtpNgaySuaChua.Text;
                                psc.BienSo = cbBienSo.Text;
                                psc.TongTien = double.Parse(txtThanhTien.Text);
                                PhieuSuaChuaBUS.PhieuSuaChua_Insert(psc);

                                ct_psc.MaPhieuSuaChua = txtMaPhieu.Text;
                                ct_psc.MaVatTuPhuTung = cbVTPT.Text;
                                ct_psc.NoiDungSuaChua = cbNoiDung.Text;
                                ct_psc.SoLuong = int.Parse(txtSoLuong.Text);
                                ct_psc.DonGia = double.Parse(txtDonGia.Text);
                                ct_psc.TienCong = double.Parse(txtTienCong.Text);
                                ct_psc.ThanhTien = TinhTien();
                                CT_PhieuSuaChuaBUS.CT_PhieuSuaChua_Insert(ct_psc);
                                LoadDSxeTheoBienSo(cbBienSo.Text);
                                lbcSoLuongTon.Text = VatTuPhuTungBUS.VatTuPhuTung_GetSoLuongTon(cbVTPT.Text).ToString();
                                MessageBox.Show("Lập phiếu thành công!");
                                DoanhSo_Insert();
                            }
                        }

                    else
                        MessageBox.Show("Mã sửa chữa đã có");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
               SQLDataProvider.connection.Close();
            }

            btnIn.Enabled = false;
            btnXoa.Enabled = false;
     
}

        private void LoadDSxeTheoBienSo(string BienSo)
        {
            try
            {
                tablePhieuSuaChua.DataSource = CT_PhieuSuaChuaBUS.CT_PhieuSuaChua_GetByBienSo(BienSo);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private double TinhTien()
        {
            if (txtDonGia.Text != "" && txtSoLuong.Text != "")
            {
                double dongia = 0;
                if(double.TryParse(txtDonGia.Text, out dongia))
                {
                    dongia = double.Parse(txtDonGia.Text);
                }
       
                long soluong = long.Parse(txtSoLuong.Text);

                double tiencong = 0;
                if(double.TryParse(txtTienCong.Text,out tiencong))
                {
                    tiencong=double.Parse(txtTienCong.Text);
                }

                double thanhtien = dongia * soluong + tiencong;
                return thanhtien;
            }
            else
            {
                txtSoLuong.Text = "0";
                txtDonGia.Text = "0";
                double t = 0;
         
                if(double.TryParse(txtTienCong.Text, out t))
                {
                   t = double.Parse(txtTienCong.Text);
                }
                return t; 
            }
        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void TongTien(string MSC, string BienSo)
        {
            try
            {
                if (MSC != "")
                {
                    DataTable dt = new DataTable();
                    dt = PhieuSuaChuaBUS.PhieuSuaChua_GetTongTien(MSC, BienSo);
                    foreach (DataRow r in dt.Rows)
                    {
                        txtThanhTien.Text = r["TongTien"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbBienSo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadDSxeTheoBienSo(cbBienSo.Text);
                TongTien(txtMaPhieu.Text, cbBienSo.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }

        private void cbNoiDung_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTienCong.Text = cbNoiDung.SelectedValue.ToString();
            txtThanhTien.Text = TinhTien().ToString();
        }

        private void cbVTPT_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDonGia.Text = cbVTPT.SelectedValue.ToString();
            lbcSoLuongTon.Text = VatTuPhuTungBUS.VatTuPhuTung_GetSoLuongTon(cbVTPT.Text).ToString();
            txtThanhTien.Text = TinhTien().ToString();
        }

        private void tablePhieuSuaChua_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = tablePhieuSuaChua.CurrentCell.RowIndex;
            txtMaPhieu.Text = tablePhieuSuaChua.Rows[index].Cells[0].Value.ToString();
            TongTien(txtMaPhieu.Text, cbBienSo.Text);
            //btnXoa.Enabled = true;
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            if (txtSoLuong.Text != "")
            {
                txtThanhTien.Text = TinhTien().ToString();
            }
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            txtMaPhieu.Text = "";
            txtDonGia.Text = "";
            txtSoLuong.Text = "";
            txtThanhTien.Text = "";
            txtTienCong.Text = "";
            cbVTPT.Text = "";
            cbNoiDung.Text = "";
            cbBienSo.Text = "";
            lbcSoLuongTon.Text = "";
            txtSoLuong.Text= "";

            btnIn.Enabled = false;
            btnXoa.Enabled = false;
            
            //txtTongTien.Text = "";
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            frmReportPhieuSuaChua frm = new frmReportPhieuSuaChua(cbBienSo.Text);
            frm.Show();
            btnIn.Enabled = false;
            btnXoa.Enabled = false;
        }

        private bool KiemTraSoLuongTon()
        {
            bool kt = false;
            if (txtSoLuong.Text != "")
            {
                int sl = int.Parse(txtSoLuong.Text);
                int slt = VatTuPhuTungBUS.VatTuPhuTung_GetSoLuongTon(cbVTPT.Text);
                if (sl <= slt)
                    kt = true;
                else
                {
                    kt = false;
                    MessageBox.Show("Số lượng phải nhỏ hơn số lượng vật tư phụ tùng tồn trong kho", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSoLuong.Clear();
                    txtSoLuong.Focus();
                }

            }
            return kt;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int r = tablePhieuSuaChua.CurrentRow.Index;
                string msc = tablePhieuSuaChua.Rows[r].Cells[0].Value.ToString();
                string noidung = tablePhieuSuaChua.Rows[r].Cells[2].Value.ToString();
                string mavt = tablePhieuSuaChua.Rows[r].Cells[1].Value.ToString();
                CT_PhieuSuaChuaBUS.CT_PhieuSuaChua_Delete(msc, noidung, mavt);
                MessageBox.Show("Xóa thành công!", "Xóa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDonGia.Text = "";
                txtSoLuong.Text = "";
                txtThanhTien.Text = "";
                txtTienCong.Text = "";
                cbVTPT.Text = "";
                cbNoiDung.Text = "";
                lbcSoLuongTon.Text = "";
                txtSoLuong.Text = "";
                txtMaPhieu.Text = "";
                LoadDSxeTheoBienSo(cbBienSo.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            btnXoa.Enabled = false;
            btnIn.Enabled = false;
        }

        private BindingManagerBase bmb;

        private void tablePhieuSuaChua_Click(object sender, EventArgs e)
        {
            txtMaPhieu.DataBindings.Clear();
            txtMaPhieu.DataBindings.Add("text", tablePhieuSuaChua.DataSource, "MaPhieuSuaChua");
            cbVTPT.DataBindings.Clear();
            cbVTPT.DataBindings.Add("text", tablePhieuSuaChua.DataSource, "MaVatTuPhuTung");
            txtSoLuong.DataBindings.Clear();
            txtSoLuong.DataBindings.Add("text", tablePhieuSuaChua.DataSource, "SoLuong");
            txtDonGia.DataBindings.Clear();
            txtDonGia.DataBindings.Add("text", tablePhieuSuaChua.DataSource, "DonGia");
            cbNoiDung.DataBindings.Clear();
            cbNoiDung.DataBindings.Add("text", tablePhieuSuaChua.DataSource, "NoiDungSuaChua");
            txtTienCong.DataBindings.Clear();
            txtTienCong.DataBindings.Add("text", tablePhieuSuaChua.DataSource, "TienCong");
            txtThanhTien.DataBindings.Clear();
            txtThanhTien.DataBindings.Add("text", tablePhieuSuaChua.DataSource, "Thanhtien");
            bmb = BindingContext["MaPhieuSuaChua"];
        }

        private void tablePhieuSuaChua_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            btnXoa.Enabled = true;
            btnIn.Enabled = true;
        }

        private void btnKho_Click(object sender, EventArgs e)
        {
            frmKhoVatTu f = new frmKhoVatTu();
            f.Show();
        }
        

    }

}
