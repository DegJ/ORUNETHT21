using System;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;

namespace Services {
    public class WeatherService {
        public WeatherData GetForecast(decimal lat, decimal lon) {
            //https://opendata-download-metfcst.smhi.se/api/category/pmp3g/version/2/geotype/point/lon/15.2/lat/59.27/data.json
            var model = new WeatherData();

            using (var http = new HttpClient()) {
                //skapa urlen mot SMHI, .ToString("F3") betyder att vi använder max 3 decimaler i lat och lon
                //.Replace(",", ".") är för att svenska (vilket tas från datorn språkinställningar i vilket koden körs) decimaltal skrivs med "kommatecken" men apiet vill ha med punkt.
                var url = "https://opendata-download-metfcst.smhi.se/api/category/pmp3g/version/2/geotype/point/lon/" +
                          lon.ToString("F3").Replace(",", ".") + "/lat/" + lat.ToString("F3").Replace(",", ".") + "/data.json";
                var response = http.GetStringAsync(url).Result;
                var responsemodel = JsonConvert.DeserializeObject<SmhiWeatherRoot>(response);

                //vad de olika sakerna betyder: https://opendata.smhi.se/apidocs/metfcst/parameters.html
                // parameter namnet "t" är temperatur, "Wsymb2" är vilket typ av väder det är.
                var weathernow = responsemodel.timeSeries.FirstOrDefault();
                model.TemperatureNow = (decimal)weathernow.parameters.First(x => string.Equals("t", x.name)).values[0];
                model.WeatherNow = (WeatherType)Enum.Parse(typeof(WeatherType), weathernow.parameters.First(x => string.Equals("Wsymb2", x.name)).values[0].ToString());

                var weathertomorrow = responsemodel.timeSeries.FirstOrDefault(x => x.validTime == weathernow.validTime.AddDays(1));
                if (weathertomorrow != null) {
                    model.TemperatureTomorrow = (decimal)weathertomorrow.parameters.First(x => string.Equals("t", x.name)).values[0];
                    model.WeatherTomorrow = (WeatherType)Enum.Parse(typeof(WeatherType), weathertomorrow.parameters.First(x => string.Equals("Wsymb2", x.name)).values[0].ToString());
                }
            }

            return model;
        }
    }


    public class WeatherData {
        public decimal TemperatureNow { get; set; }
        public decimal TemperatureTomorrow { get; set; }
        public WeatherType WeatherNow { get; set; }
        public WeatherType WeatherTomorrow { get; set; }
    }

    public enum WeatherType {
        NotSet = 0,
        Clearsky = 1,
        Nearlyclearsky = 2,
        Variablecloudiness = 3,
        Halfclearsky = 4,
        Cloudysky = 5,
        Overcast = 6,
        Fog = 7,
        Lightrainshowers = 8,
        Moderaterainshowers = 9,
        Heavyrainshowers = 10,
        Thunderstorm = 11,
        Lightsleetshowers = 12,
        Moderatesleetshowers = 13,
        Heavysleetshowers = 14,
        Lightsnowshowers = 15,
        Moderatesnowshowers = 16,
        Heavysnowshowers = 17,
        Lightrain = 18,
        Moderaterain = 19,
        Heavyrain = 20,
        Thunder = 21,
        Lightsleet = 22,
        Moderatesleet = 23,
        Heavysleet = 24,
        Lightsnowfall = 25,
        Moderatesnowfall = 26,
        Heavysnowfall = 27,
    }

    public class SmhiWeatherRoot {
        public DateTime approvedTime { get; set; }
        public DateTime referenceTime { get; set; }
        public Geometry geometry { get; set; }
        public Timeseries[] timeSeries { get; set; }
    }

    public class Geometry {
        public string type { get; set; }
        public float[][] coordinates { get; set; }
    }

    public class Timeseries {
        public DateTime validTime { get; set; }
        public Parameter[] parameters { get; set; }
    }

    public class Parameter {
        public string name { get; set; }
        public string levelType { get; set; }
        public int level { get; set; }
        public string unit { get; set; }
        public float[] values { get; set; }
    }
}
