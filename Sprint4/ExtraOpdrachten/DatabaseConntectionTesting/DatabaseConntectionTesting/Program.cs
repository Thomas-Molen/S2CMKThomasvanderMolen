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
            //var query = connector.SendRun(run);
            //var query = connector.GetUsername("1");

            SaveDataHelper saveDataHelper = new SaveDataHelper();
            saveDataHelper.CreateSaveFile();
            Console.WriteLine(connector.GetUsername("2"));
            Console.WriteLine(saveDataHelper.GetSaveData());
        }
    }
}
