using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Services;

namespace ORUNETHT21WS2.Controllers {
    [RoutePrefix("api/weather")]
    public class WeatherApiController: ApiController {
        [Route("")]
        [HttpGet]
        public WeatherData GetWeather() {
            var weatherservice = new WeatherService();
            //Örebros lat och lon
            var weatherdata = weatherservice.GetForecast(59.27M, 15.20M);
            return weatherdata;
        }
    }
}
