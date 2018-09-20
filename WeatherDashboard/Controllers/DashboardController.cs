using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeatherDashboard.Models;

namespace WeatherDashboard.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            ViewData["States"] = "Sonora";
            ViewData["Cities"] = StatesCitiesModel.GetCiudades("Sonora");
            return View();
        }

        [HttpGet]
        public ActionResult GetStates()
        {
            List<SelectListItem> States = new List<SelectListItem>();
            var Estados = StatesCitiesModel.GetEstados();
            for (int i = 0; i < Estados.Length; i++)
            {
                States.Add(new SelectListItem { Text = Estados[i], Value = Estados[i] });
            }
            return Json(States, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCities(string state)
        {
            if (!string.IsNullOrWhiteSpace(state))
            {
                ViewData["States"] = state;
                ViewData["Cities"] = StatesCitiesModel.GetCiudades(state);
                var content = StatesCitiesModel.GetCiudades(state);
                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                var jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(content);
                return Json(jsonContent, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        public JsonResult GetWeatherHistorical(string City, string State, string DateStart,string DateEnd, string Scale)
        {
            if (!string.IsNullOrWhiteSpace(City) && !string.IsNullOrWhiteSpace(State) && !string.IsNullOrWhiteSpace(DateStart) && !string.IsNullOrWhiteSpace(DateEnd) && !string.IsNullOrWhiteSpace(Scale))
            {
                Weather weather = new Weather();
                List<WeatherCity> weathers = new List<WeatherCity>();
                CultureInfo format = CultureInfo.CreateSpecificCulture("ja-JP");
                DateTime start = DateTime.Parse(DateStart);
                DateTime end = DateTime.Parse(DateEnd);
                var days = end.Subtract(start).TotalDays;
                DateTime date = start;
                WeatherCity weather_city;
                for (int i = 0; i < days; i++)
                {
                    string aux = weather.getWeather(City, State, date.ToString("d", format).Replace("/", "-"), date.AddDays(1).ToString("d", format).Replace("/", "-"), Scale);
                    var obj = JObject.Parse(aux);
                    weather_city = new WeatherCity();
                    weather_city.date = (string)obj["data"][0]["datetime"];
                    weather_city.temp_min = (string)obj["data"][0]["min_temp"];
                    weather_city.temp_max = (string)obj["data"][0]["max_temp"];
                    weather_city.temperature = (string)obj["data"][0]["temp"];
                    //JsonConvert.DeserializeObject<WeatherCity>(aux);
                    weathers.Add(weather_city);
                date = date.AddDays(1);
                }
                return Json(weathers, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        public JsonResult GetWeatherDays(string City, string State, string Days, string Scale)
        {
            if (!string.IsNullOrWhiteSpace(City) && !string.IsNullOrWhiteSpace(State) && !string.IsNullOrWhiteSpace(Days) && !string.IsNullOrWhiteSpace(Scale))
            {
                Weather weather = new Weather();
                return Json(weather.getWeatherDays(City, State, Days, Scale), JsonRequestBehavior.AllowGet);
            }
            return null;
        }
    }
}