using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Services;

namespace WS2.Controllers {
    [RoutePrefix("api/weather")]
    public class WeatherApiController : ApiController {

        [Route("")]
        public WeatherData GetWeather() {
            var service = new WeatherService();
            var weatherdata = service.GetForecast(59.27M, 15.20M);
            return weatherdata;
        }
    }
}