using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SharingPay.Attendance.Data
{
    public class DapperFactory
    {
        public static string _connectionString = ConfigurationManager.AppSettings["MySqlConnection"];


        public static MySqlConnection CrateOracleConnection(string connectionString = "")
        {
            MySqlConnection connection;
            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                connection = new MySqlConnection(connectionString);
            }
            else
            {
                connection = new MySqlConnection(_connectionString);
            }
            connection.Open();
            return connection;
        }
    }
}
