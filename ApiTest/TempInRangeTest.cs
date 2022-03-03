using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiTest
{
    public class TempInRange
    {
        HttpClient client = new HttpClient();
        [Test]
        public async Task GetWeather()
        {
            bool InRange(double number, double min, double max)
            {
                if (number >= min && number <= max)
                    return true;
                else
                    return false;
            }
            string response = await client.GetStringAsync("https://www.metaweather.com/api/location/834463/");
            Newtonsoft.Json.Linq.JObject jsonString = (Newtonsoft.Json.Linq.JObject)JsonConvert.DeserializeObject(response);
            List<JsonResult> result = JsonConvert.DeserializeObject<List<JsonResult>>(jsonString.First.First.ToString());
            double minWinter = -39.1, minSummer = 0;
            double maxWinter = 10.5, maxSummer = 36.5;
            foreach (var item in result)
            {
                if (item.the_temp >= 0.0)
                {
                    if (!InRange(item.the_temp, minSummer, maxSummer)) Assert.Fail();
                }
                if (item.the_temp < 0.0)
                {
                    if (!InRange(item.the_temp, minWinter, maxWinter)) Assert.Fail();
                }
            }
            Assert.Pass();
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