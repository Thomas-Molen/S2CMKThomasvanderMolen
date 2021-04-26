using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace DatabaseConntectionTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "server=localhost;uid=root;pwd=;database=platformspeedrunner_web_systeem";
            string dbQuery = "SELECT* FROM platformspeedrunner_web_systeem.run.custom_name;";

            MySqlConnection sqlConnection = new MySqlConnection(connectionString);
            MySqlCommand sqlCommand = new MySqlCommand(dbQuery, sqlConnection);

            sqlConnection.Open();

            MySqlDataAdapter sqlDataAdapter = new MySqlDataAdapter();
            sqlDataAdapter.SelectCommand = sqlCommand;

            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            Console.WriteLine(dataTable);
        }
    }
}
