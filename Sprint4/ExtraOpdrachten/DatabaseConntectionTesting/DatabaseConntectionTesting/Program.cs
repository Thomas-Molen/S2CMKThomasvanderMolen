using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace DatabaseConntectionTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            Run run = new Run();
            run.create(run);
        }
    }
}
