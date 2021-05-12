using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace DatabaseConntectionTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            Run run = new Run()
            {
                unique_key = "1",
                duration = 231
            };
            HttpConnector connector = new HttpConnector();
            //connector.SendRun(run);
            connector.GetUser();
        }
    }
}
