using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiTest
{
    public class CoordsTest
    {
        HttpClient client = new HttpClient();
        [Test]
        public async Task GetWeather()
        {
            string response = await client.GetStringAsync("https://www.metaweather.com/api/location/search/?query=min");
            List<JsonResult> result = JsonConvert.DeserializeObject<List<JsonResult>>(response);
            foreach (var item in result)
            {
                JsonResult resultMinsk;
                if (item.title == "Minsk")
                {
                    resultMinsk = item;
                    if(resultMinsk.latt_long.Contains("53.") && resultMinsk.latt_long.Contains("27."))
                    {
                        Assert.Pass();
                    }
                }
            }
            Assert.Fail();
        }
        class JsonResult
        {
            public string title { get; set; }
            public string location_type { get; set; }
            public int woeid { get; set; }
            public string latt_long { get; set; }
        }
    }
}