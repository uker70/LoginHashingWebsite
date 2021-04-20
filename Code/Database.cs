using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginHashing
{
    class Database
    {
        //connection string
        private static string connectionString = "Server=localhost;Database=HashingDatabase;Trusted_Connection=True;";

        //method that gets the database stored procedure, fills the parameters and calls it
        public static void Create(string username, string password, string salt)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("CreateUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
                cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = password;
                cmd.Parameters.Add("@salt", SqlDbType.VarChar).Value = salt;

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public static string[] Login(string username)
        {
            string[] result = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UserLogin", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@username", SqlDbType.VarChar).Value = username;

                //reads the result from the stored procedure
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = new[] { $"{reader["Password"]}", $"{reader["Salt"]}" };
                    }
                }
            }

            return result;
        }
    }
}
