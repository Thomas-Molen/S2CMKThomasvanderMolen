using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConntectionTesting
{
    class HttpConnector
    {
        [HttpPost]
        public void SendRun(Run run)
        {
            using (var client = new HttpClient())
            {
                string JSONresult = JsonConvert.SerializeObject(run);
                var content = new StringContent(JSONresult, Encoding.UTF8, "application/json");
                var postTask = client.PostAsync("http://platformerspeedrunner/api/submit_run", content);
                postTask.Wait();
                if (postTask.Result.IsSuccessStatusCode)
                {
                    Console.WriteLine("Data Sent");
                }
            }
        }

        [HttpGet]
        public void GetUser()
        {
            using (var client = new HttpClient())
            {
                var getTask = client.GetStringAsync("http://platformerspeedrunner/api/get_user");
                getTask.Wait();
                Console.WriteLine(getTask.Result);
            }
        }
    }
}
