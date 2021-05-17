using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseConntectionTesting
{
    class HttpConnector
    {
        [HttpPost]
        public async Task SendRun(Run run)
        {
            using (var client = new HttpClient())
            {
                string JSONresult = JsonConvert.SerializeObject(run);
                var content = new StringContent(JSONresult, Encoding.UTF8, "application/json");
                var postTask = await client.PostAsync("http://platformerspeedrunner/api/submit_run", content);
                if (postTask.IsSuccessStatusCode)
                {
                    Console.WriteLine("Data Sent");
                }
            }
        }

        [HttpGet]
        public string GetUsername(string unique_key)
        {
            using (var client = new HttpClient())
            {
                return client.GetStringAsync("http://platformerspeedrunner/api/get_username/" + unique_key).Result;
            }
        }

        [HttpGet]
        public string GetUniqueKey()
        {
            using (var client = new HttpClient())
            {
                return client.GetStringAsync("http://platformerspeedrunner/api/get_unique_key").Result;
            }
        }
    }
}
