using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWebApp.Data
{
    public static class SqlHelper
    {
        public static void ExecuteNonQuery(String commandText, params SqlParameter[] parameters)
        {
            var conn = ConnectionManager.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn = ConnectionManager.OpenConnection(conn);
            }
            SqlCommand cmd = new SqlCommand(commandText, conn);

            cmd.Parameters.AddRange(parameters);
            cmd.ExecuteNonQuery();
        }

        public static Object ExecuteScalar(String commandText, params SqlParameter[] parameters)
        {

            var conn = ConnectionManager.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn = ConnectionManager.OpenConnection(conn);
            }
            SqlCommand cmd = new SqlCommand(commandText, conn);
            cmd.Parameters.AddRange(parameters);
            return cmd.ExecuteScalar();
        }


        public static SqlDataReader ExecuteReader(String commandText, params SqlParameter[] parameters)
        {
            var conn = ConnectionManager.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn = ConnectionManager.OpenConnection(conn);
            }
            SqlCommand cmd = new SqlCommand(commandText, conn);
            cmd.Parameters.AddRange(parameters);
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;

        }

        public static SqlDataReader ExecuteReader(String commandText)
        {
            var conn = ConnectionManager.GetConnection();
            if (conn.State == ConnectionState.Closed)
            {
                conn = ConnectionManager.OpenConnection(conn);
            }
            SqlCommand cmd = new SqlCommand(commandText, conn);
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return reader;
        }
    }
}
