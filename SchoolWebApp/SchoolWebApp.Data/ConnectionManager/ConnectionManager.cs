using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWebApp.Data
{
    public sealed class ConnectionManager
    {
        private static SqlConnection connection = null;
        private ConnectionManager()
        { }
        public static SqlConnection GetConnection()

        {
            if (connection == null)
            {
                 //string connectionString = ConfigurationManager.AppSettings["ConnectionString"];

                string connectionString = ConfigurationManager.ConnectionStrings["SQLConnection"].ConnectionString;
               // string connectionString = "Data Source=.; Initial Catalog=SchoolApp; Integrated Security=True";
                connection = new SqlConnection(connectionString);
                connection.Open();
            }

            return connection;
        }
        public static SqlConnection OpenConnection(SqlConnection connection)
        {
            if (connection == null) return null;
            else
            {
                connection.Open();
                return connection;
            }

        }

    }
}

