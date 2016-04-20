using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Gara_DATA.GaRa_Info;

namespace Gara_DATA.Gara_DAL
{
    public class CT_BaoCaoDoanhSoDAL:SQLDataProvider
    {
        public void CT_BaoCaoDoanhSo_Insert(int Thang,int Nam, string HieuXe, int SoLuotSua, double ThanhTien, float TiLe)
        {
            using (var cmd = new SqlCommand("sp_CT_BaoCaoDoanhSo_Insert", GetConnection()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Thang", Thang));
                cmd.Parameters.Add(new SqlParameter("@Nam", Nam));
                cmd.Parameters.Add(new SqlParameter("@HieuXe", HieuXe));
                cmd.Parameters.Add(new SqlParameter("@SoLuotSua", SoLuotSua));
                cmd.Parameters.Add(new SqlParameter("@ThanhTien", ThanhTien));
                cmd.Parameters.Add(new SqlParameter("@TiLe", TiLe));
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void CT_BaoCaoDoanhSo_Update(int Thang, int Nam, string HieuXe, int SoLuotSua, double ThanhTien, float TiLe)
        {
            using (var cmd = new SqlCommand("sp_CT_BaoCaoDoanhSo_Update", GetConnection()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Thang", Thang));
                cmd.Parameters.Add(new SqlParameter("@Nam", Nam));
                cmd.Parameters.Add(new SqlParameter("@HieuXe", HieuXe));
                cmd.Parameters.Add(new SqlParameter("@SoLuotSua", SoLuotSua));
                cmd.Parameters.Add(new SqlParameter("@ThanhTien", ThanhTien));
                cmd.Parameters.Add(new SqlParameter("@TiLe", TiLe));
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
