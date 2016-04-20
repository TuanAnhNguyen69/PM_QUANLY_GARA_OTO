using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevComponents.DotNetBar;
using DevExpress.XtraBars;
using Gara_DATA;
using Gara_BUS;

namespace Gara_UI
{
    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public MainForm()
        {
            InitializeComponent();
        }
       
        public bool CheckOpenTabs(string name)
        {
            for (int i = 0; i < superTabControl1.Tabs.Count; i++)
            {
                if (superTabControl1.Tabs[i].Text == name)
                {
                    superTabControl1.SelectedTabIndex = i;
                    return true;
                }
            }
            return false;
        }
       

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            
            if (!CheckOpenTabs("Tiếp nhận xe"))
            {
                SuperTabItem t = superTabControl1.CreateTab("Tiếp nhận xe");
                frmTiepNhanXe frm = new frmTiepNhanXe() { TopLevel = false, Dock = DockStyle.Fill, StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen, superTabControlIndex = superTabControl1.Tabs.Count - 1 /**/ };
                frm.OnClosing += frm_OnClosing; //
                t.AttachedControl.Controls.Add(frm);
                frm.Show();
                superTabControl1.SelectedTabIndex = superTabControl1.Tabs.Count - 1;
            }
            else
                superTabControl1.TabIndex = superTabControl1.Tabs.Count - 1;
        }
        void frm_OnClosing(object sender, EventArgs e)
        {
            superTabControl1.Tabs.RemoveAt(((frmTiepNhanXe)sender).superTabControlIndex);
        }

        private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
        {

            if (!CheckOpenTabs("Phiếu thu tiền"))
            {
                SuperTabItem t = superTabControl1.CreateTab("Phiếu thu tiền");
                frmPhieuThuTien frm = new frmPhieuThuTien() { TopLevel = false, Dock = DockStyle.Fill, StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen, superTabControlIndex = superTabControl1.Tabs.Count - 1 };
                frm.OnClosing5 += frm_OnClosing5;
                t.AttachedControl.Controls.Add(frm);
                frm.Show();
                superTabControl1.SelectedTabIndex = superTabControl1.Tabs.Count - 1;
                
            }
            else
                superTabControl1.TabIndex = superTabControl1.Tabs.Count - 1;
        }
        void frm_OnClosing5(object sender, EventArgs e)
        {
            superTabControl1.Tabs.RemoveAt(((frmPhieuThuTien)sender).superTabControlIndex);
        }

        
        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!CheckOpenTabs("Tìm kiếm"))
            {
                SuperTabItem t = superTabControl1.CreateTab("Tìm kiếm");
                frmTimKiem frm = new frmTimKiem() { TopLevel = false, Dock = DockStyle.Fill, StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen, superTabControlIndex = superTabControl1.Tabs.Count - 1 };
                frm.OnClosing6 += frm_OnClosing6;
                t.AttachedControl.Controls.Add(frm);
                frm.Show();
                superTabControl1.SelectedTabIndex = superTabControl1.Tabs.Count - 1;
            }
            else
                superTabControl1.TabIndex = superTabControl1.Tabs.Count - 1;
        }
        void frm_OnClosing6(object sender, EventArgs e)
        {
            superTabControl1.Tabs.RemoveAt(((frmTimKiem)sender).superTabControlIndex);
        }

        private void btnPhieuSuaChua_ItemClick(object sender, ItemClickEventArgs e)
        {
             if (!CheckOpenTabs("Phiếu sữa chữa"))
            {
                SuperTabItem t = superTabControl1.CreateTab("Phiếu sữa chữa");
                frmPhieuSuaChua frm = new frmPhieuSuaChua() { TopLevel = false, Dock = DockStyle.Fill, StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen, superTabControlIndex = superTabControl1.Tabs.Count - 1 };
                frm.OnClosing4 += frm_OnClosing4;
                t.AttachedControl.Controls.Add(frm);
                frm.Show();
                superTabControl1.SelectedTabIndex = superTabControl1.Tabs.Count - 1;
            }
            else
                superTabControl1.TabIndex = superTabControl1.Tabs.Count - 1;
        
        }
        void frm_OnClosing4(object sender, EventArgs e)
        {
            superTabControl1.Tabs.RemoveAt(((frmPhieuSuaChua)sender).superTabControlIndex);
        }


        private void btnBaoCaoDoanhSo_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!CheckOpenTabs("Báo cáo doanh số"))
            {
                SuperTabItem t = superTabControl1.CreateTab("Báo cáo doanh số");
                frmBaoCaoDoanhSo frm = new frmBaoCaoDoanhSo() { TopLevel = false, Dock = DockStyle.Fill, StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen, superTabControlIndex = superTabControl1.Tabs.Count - 1 };
                frm.OnClosing2 += frm_OnClosing2;
                t.AttachedControl.Controls.Add(frm);
                frm.Show();
                superTabControl1.SelectedTabIndex = superTabControl1.Tabs.Count - 1;
            }
            else
                superTabControl1.TabIndex = superTabControl1.Tabs.Count - 1;
        }
        void frm_OnClosing2(object sender, EventArgs e)
        {
            superTabControl1.Tabs.RemoveAt(((frmBaoCaoDoanhSo)sender).superTabControlIndex);
        }

        private void btnBaoCaoTon_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!CheckOpenTabs("Báo cáo tồn"))
            {
                SuperTabItem t = superTabControl1.CreateTab("Báo cáo tồn");
                frmBaoCaoTon frm = new frmBaoCaoTon() { TopLevel = false, Dock = DockStyle.Fill, StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen, superTabControlIndex = superTabControl1.Tabs.Count - 1 };
                frm.OnClosing1 += frm_OnClosing1;
                t.AttachedControl.Controls.Add(frm);
                frm.Show();
                superTabControl1.SelectedTabIndex = superTabControl1.Tabs.Count - 1;
            }
            else
                superTabControl1.TabIndex = superTabControl1.Tabs.Count - 1;
        }
        void frm_OnClosing1(object sender, EventArgs e)
        {
            superTabControl1.Tabs.RemoveAt(((frmBaoCaoTon)sender).superTabControlIndex);
        }

        private void btnQuanLyHieuXe_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!CheckOpenTabs("Quản lý hiệu xe"))
            {
                SuperTabItem t = superTabControl1.CreateTab("Quản lý hiệu xe");
                frmHieuXe frm = new frmHieuXe() { TopLevel = false, Dock = DockStyle.Fill, StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen, superTabControlIndex = superTabControl1.Tabs.Count - 1 };
                frm.OnClosing3 += frm_OnClosing3;
                t.AttachedControl.Controls.Add(frm);
                frm.Show();
                superTabControl1.SelectedTabIndex = superTabControl1.Tabs.Count - 1;
            }
            else
                superTabControl1.TabIndex = superTabControl1.Tabs.Count - 1;
        }
        void frm_OnClosing3(object sender, EventArgs e)
        {
            superTabControl1.Tabs.RemoveAt(((frmHieuXe)sender).superTabControlIndex);
        }

        private void btnQuanLyVatTuPhuTung_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (!CheckOpenTabs("Quản lý vật tư phụ tùng"))
            {
                SuperTabItem t = superTabControl1.CreateTab("Quản lý vật tư phụ tùng");
                frmVatTuPhuTung frm = new frmVatTuPhuTung() { TopLevel = false, Dock = DockStyle.Fill, StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen, superTabControlIndex = superTabControl1.Tabs.Count - 1 };
                frm.OnClosing7 += frm_OnClosing7;
                t.AttachedControl.Controls.Add(frm);
                frm.Show();
                superTabControl1.SelectedTabIndex = superTabControl1.Tabs.Count - 1;
            }
            else
                superTabControl1.TabIndex = superTabControl1.Tabs.Count - 1;
        }
        void frm_OnClosing7(object sender, EventArgs e)
        {
            superTabControl1.Tabs.RemoveAt(((frmVatTuPhuTung)sender).superTabControlIndex);
        }

        private void btnQuanLyTienCong_ItemClick(object sender, ItemClickEventArgs e)
        {

            if (!CheckOpenTabs("Quản lý tiền công"))
            {
                SuperTabItem t = superTabControl1.CreateTab("Quản lý tiền công");
                frmTienCong frm = new frmTienCong() { TopLevel = false, Dock = DockStyle.Fill, StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen, superTabControlIndex = superTabControl1.Tabs.Count - 1 };
                frm.OnClosing8 += frm_OnClosing8;
                t.AttachedControl.Controls.Add(frm);
                frm.Show();
                superTabControl1.SelectedTabIndex = superTabControl1.Tabs.Count - 1;
            }
            else
                superTabControl1.TabIndex = superTabControl1.Tabs.Count - 1;
        }
        void frm_OnClosing8(object sender, EventArgs e)
        {
            superTabControl1.Tabs.RemoveAt(((frmTienCong)sender).superTabControlIndex);
        }

        private void barbtn_capnhatsoxe_ItemClick(object sender, ItemClickEventArgs e)
        {
            SQLDataProvider connect = new SQLDataProvider();
            SqlCommand cmd = new SqlCommand("sp_ThayDoiSoXeSuaChuaToiDa", connect.GetConnection());
            int SoLuotSua = int.Parse(barEdit_soxe.EditValue.ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@SoXeSuaChuaToiDa", SoLuotSua));
            cmd.ExecuteNonQuery();
            SQLDataProvider.connection.Close();
            MessageBox.Show("Cập nhật số xe sửa chửa tối đa trong ngày thành công!!");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            barEdit_soxe.EditValue = XeBUS.SoXeSuaChuaTrongNgay();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

      

        //private void barbtn_capnhatsoxe_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    SQLDataProvider connect = new SQLDataProvider();
        //    SqlCommand cmd = new SqlCommand("sp_ThayDoiSoXeSuaChuaToiDa", connect.GetConnection());
        //    int SoLuotSua = int.Parse(barEdit_soxe.EditValue.ToString());
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add(new SqlParameter("@SoXeSuaChuaToiDa", SoLuotSua));
        //    cmd.ExecuteNonQuery();
        //    SQLDataProvider.connection.Close();
        //    MessageBox.Show("Cập nhật số xe sửa chửa tối đa trong ngày thành công!!");
            
        //}

      

        
    }
}
