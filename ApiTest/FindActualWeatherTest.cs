using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiTest
{
    public class FindActualWeatherTest
    {
        HttpClient client = new HttpClient();
        [Test]
        public async Task GetWeather()
        {
            string response = await client.GetStringAsync("https://www.metaweather.com/api/location/834463");
            Newtonsoft.Json.Linq.JObject jsonString = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(response);
            List<JsonResult> result = JsonConvert.DeserializeObject<List<JsonResult>>(jsonString.First.First.ToString());
            if (result[0].applicable_date == DateTime.Now.ToString("yyyy-MM-dd")) Assert.Pass(); else Assert.Fail();
        }
        class JsonResult
        {
            public long id { get; set; }
            public string weather_state_name { get; set; }
            public string weather_state_abbr { get; set; }
            public string wind_direction_compass { get; set; }
            public string created { get; set; }
            public string applicable_date { get; set; }
            public float min_temp { get; set; }
            public float max_temp { get; set; }
            public float the_temp { get; set; }
            public float wind_speed { get; set; }
            public float wind_direction { get; set; }
            public float air_pressure { get; set; }
            public float humidity { get; set; }
            public float visibility { get; set; }
            public float predictability { get; set; }
        }
    }
}