using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ICAD.Data.DataContext
{
    public class FinacleContext
    {
        private string ConnectionString { get; set; }
        public FinacleContext()
        {
            ConnectionString = ConfigurationManager.AppSettings["FinacleConnection"].ToString();
        }

        public string GetConnection()
        {
            if (string.IsNullOrEmpty(ConnectionString))
            {
                ConnectionString = ConfigurationManager.AppSettings["FinacleConnection"].ToString();
            }
            return ConnectionString;
        }

        public OracleDataReader RunQuery(string query)
        {
            var con = GetConnection();
            OracleConnection orConn = new OracleConnection(con);
            OracleCommand orCmd = new OracleCommand();

            orCmd.Connection = orConn;
            orCmd.CommandType = CommandType.Text;
            orCmd.CommandText = query;

            orConn.Open();
            return orCmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public string GetOrdinalStringValue(OracleDataReader dr, string SColName)
        {
            if (!dr.IsDBNull(dr.GetOrdinal(SColName)))
            {
                return HttpUtility.HtmlDecode(dr.GetString(dr.GetOrdinal(SColName)));
            }
            else
            {
                return null;
            }
        }

        public DateTime? GetOrdinalDateValue(OracleDataReader dr, string SColName)
        {
            if (!dr.IsDBNull(dr.GetOrdinal(SColName)))
            {
                var odata = dr[SColName].ToString();
                var date = DateTime.ParseExact(odata, "dd-MM-yyyy", null);
                return date;
            }
            else
            {
                return null;
            }
        }

        public decimal GetOrdinalDecimalValue(OracleDataReader dr, string SColName)
        {
            decimal result = 0;
            if (!dr.IsDBNull(dr.GetOrdinal(SColName)))
            {
                result = dr.GetDecimal(dr.GetOrdinal(SColName));
            }
            return result;
        }
    }
}
