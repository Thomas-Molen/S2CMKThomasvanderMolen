using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DatabaseConntectionTesting
{
    class Run
    {
        public string user_id { get; set; } = null;
        public string active { get; set; } = null;
        public string custom_name { get; set; } = null;
        public string created_at { get; set; } = null;
        public int upvotes { get; set; } = 0;
        public string information { get; set; } = null;


        public string unique_key { get; set; } = "1";
        public int duration { get; set; } = 124;

        [HttpPost]
        public void create(Run run)
        {
            using (var client = new HttpClient())
            {
                var stringContent = new StringContent(run.ToString());
                var postTask = client.PostAsync("http://platformerspeedrunner/api/submit_run", stringContent);
                postTask.Wait();
            }
            Console.WriteLine("Done");
        }
    }
}
