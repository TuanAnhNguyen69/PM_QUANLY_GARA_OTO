﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gara_DATA.GaRa_Info;
using System.Data;
using System.Data.SqlClient;

namespace Gara_DATA.Gara_DAL
{
    public class PhieuThuTienDAL:SQLDataProvider
    {
        public void PhieuThuTien_Insert(PhieuThuTien Data)
        {
            using (var cmd = new SqlCommand("sp_PhieuThuTien_Insert", GetConnection()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@MaPhieuThuTien", Data.MaPhieuThuTien));
                cmd.Parameters.Add(new SqlParameter("@BienSo", Data.BienSo));
                cmd.Parameters.Add(new SqlParameter("@NgayThuTien", Data.NgayThuTien));
                cmd.Parameters.Add(new SqlParameter("@SoTienThu", Data.SoTienThu));
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
