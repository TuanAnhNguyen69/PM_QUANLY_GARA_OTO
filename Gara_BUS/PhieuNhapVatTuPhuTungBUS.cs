using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Gara_DATA.Gara_DAL;
using Gara_DATA.GaRa_Info;
using System.Data.SqlClient;
using Gara_DATA;
namespace Gara_BUS
{
    public class PhieuNhapVatTuPhuTungBUS
    {
        private static readonly PhieuNhapVatTuPhuTungDAL db = new PhieuNhapVatTuPhuTungDAL();
        public static void PhieuNhapVatTuPhuTung_Insert(PhieuNhapVatTuPhuTung Data)
        {
            db.PhieuNhapVatTuPhuTung_Insert(Data);
        }
        public static DataTable PhieuNhapVatTuPhuTung_GetAll(string NgayNhap)
        {
            return db.PhieuNhapVatTuPhuTung_GetAll(NgayNhap);
        }
        public static int PhieuNhapVatTuPhuTung_KiemTra(string SoPhieuNhap)
        {
            return db.PhieuNhapVatTuPhuTung_KiemTra(SoPhieuNhap);
        }
        public static DataTable PhieuNhapVatTuPhuTung_GetPhieuNhap()
        {
            return db.PhieuNhapVatTuPhuTung_GetPhieuNhap();
        }
    }
}
