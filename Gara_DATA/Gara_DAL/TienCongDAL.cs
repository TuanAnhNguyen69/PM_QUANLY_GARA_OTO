using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gara_DATA.GaRa_Info;
using System.Data;
using System.Data.SqlClient;

namespace Gara_DATA.Gara_DAL
{
    public class TienCongDAL: SQLDataProvider
    {
        public DataTable TienCong_GetAll()
        {
            using (var cmd = new SqlCommand("sp_TienCong_GetAll", GetConnection()))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                var da = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        public void TienCong_Insert(TienCong Data)
        {
            using (var cmd = new SqlCommand("sp_TienCong_Insert", GetConnection()))
            {
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@NoiDungSuaChua", Data.NoiDung));
                    cmd.Parameters.Add(new SqlParameter("@DonGia", Data.GiaTienCong));
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void TienCong_Update(TienCong Data)
        {
            using (var cmd = new SqlCommand("sp_TienCong_Update", GetConnection()))
            {
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@NoiDungSuaChua", Data.NoiDung));
                    cmd.Parameters.Add(new SqlParameter("@DonGia", Data.GiaTienCong));
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void TienCong_Delete(TienCong Data)
        {
            using (var cmd = new SqlCommand("sp_TienCong_Delete", GetConnection()))
            {
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@NoiDungSuaChua", Data.NoiDung));
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
    }
    
}
