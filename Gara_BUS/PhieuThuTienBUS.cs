using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gara_DATA.Gara_DAL;
using Gara_DATA.GaRa_Info;
using System.Data;

namespace Gara_BUS
{
    public class PhieuThuTienBUS
    {
        private static readonly PhieuThuTienDAL db = new PhieuThuTienDAL();
        public static void PhieuThuTien_Insert(PhieuThuTien Data)
        {
            db.PhieuThuTien_Insert(Data);
        }
    }
}
