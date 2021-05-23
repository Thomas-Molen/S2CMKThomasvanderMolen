using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PlatformerSpeedRunner.Helper
{
    public class DatabaseHelper
    {
        private SaveDataHelper dataHelper;
        public class Run
        {
            public string unique_key { get; set; }
            public int duration { get; set; }
        }
        public DatabaseHelper()
        {
            dataHelper = new SaveDataHelper();
        }

        [HttpPost]
        public async Task SendRun(int duration)
        {
            Run run = new Run()
            {
                unique_key = dataHelper.GetSaveData(),
                duration = duration
            };

            using (var client = new HttpClient())
            {
                string JSONresult = JsonConvert.SerializeObject(run);
                var content = new StringContent(JSONresult, Encoding.UTF8, "application/json");
                var postTask = await client.PostAsync("http://platformerspeedrunner/api/submit_run", content);
            }
        }

        [HttpGet]
        public string GetUsername()
        {
            using (var client = new HttpClient())
            {
                return client.GetStringAsync("http://platformerspeedrunner/api/get_username/" + dataHelper.GetSaveData()).Result;
            }
        }

        [HttpGet]
        public string GetBestTime()
        {
            using (var client = new HttpClient())
            {
                return client.GetStringAsync("http://platformerspeedrunner/api/get_best_time/" + dataHelper.GetSaveData()).Result;
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
