using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;

namespace Gara_DATA
{
    public class SQLDataProvider
    {
        public static string connectionstring;
        public static SqlConnection connection = null;
        //public static string strConStr = System.Configuration.ConfigurationManager.ConnectionStrings["Data Source=TUNGUYEN\ANHTU;Initial Catalog=QUANLYGARAOTO;Integrated Security=True"].ConnectionString;
        public SQLDataProvider() 
        {
            if (connection == null) 
            { 
                
                //connection = new SqlConnection(strConStr);
                connectionstring = ConfigurationManager.ConnectionStrings["Gara_UI.Properties.Settings.QUANLYGARAOTOConnectionString"].ToString();
                connection = new SqlConnection(connectionstring);
            }
            //if (connection.State != ConnectionState.Open) { connection.Open(); }
        }
        public SqlConnection GetConnection()
        {
            {
                if (connection.State == ConnectionState.Closed)
                {
                    //connection.Close();
                    connection.Open();
                    return connection;
                }
                else
                    return connection;
            }
        }
    }
}

