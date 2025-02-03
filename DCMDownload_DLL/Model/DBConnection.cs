using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace DCMDownload_DLL.Model
{
    public class DBConnection
    {
        public static SqlConnection DCMSource() {
            String connectionString = ConfigurationManager.ConnectionStrings["CCMSConn"].ToString();
            SqlConnection connection = new SqlConnection(connectionString);
            //sconnection.Open();
            return connection;
        }
        public static SqlConnection CCMSPortalDestination()
        {
            String connectionString = ConfigurationManager.ConnectionStrings["CCMSPortalConn"].ToString();
            SqlConnection connection = new SqlConnection(connectionString);
            //connection.Open();
            return connection;
        }
    }
}
