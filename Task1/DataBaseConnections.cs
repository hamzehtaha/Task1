using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey
{
    // This Class For Data Base and get Connection with data base  
    public static class DataBaseConnections
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["connection_string"].ConnectionString;
        public static SqlConnection GetConnection()
        {
            try
            {
                return new SqlConnection(DataBaseConnections.connectionString);
            }
            catch (Exception ex)
            {
                return new SqlConnection(DataBaseConnections.connectionString);
            }
        }

    }
}
