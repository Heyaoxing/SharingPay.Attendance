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
        public static readonly string _connectionString = ConfigurationManager.AppSettings["MySqlConnection"];


        public static MySqlConnection CrateOracleConnection()
        {
            var connection = new MySqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}
