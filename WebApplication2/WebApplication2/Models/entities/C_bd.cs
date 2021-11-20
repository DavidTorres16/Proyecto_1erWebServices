using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class C_bd
    {
        private MySqlConnection connection;


        public C_bd()
        {
            connection = new MySqlConnection("server = localhost; port = 3306; userid = root; Password = ; database = ejerciciocarlos");
        }

        public bool SqlOperations(string sql)
        {
            MySqlCommand query = new MySqlCommand(sql, connection);
            try
            {
                connection.Open();
                query.ExecuteNonQuery();
                connection.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public DataTable getData(string sql)
        {
            DataTable dataTable = new DataTable(); 
            MySqlCommand query = new MySqlCommand(sql, connection);
            try
            {
                connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(query);
                adapter.Fill(dataTable);
                connection.Close();
                adapter.Dispose();
            }
            catch
            {
                return null;
            }
            return dataTable;
        }
    }
}
