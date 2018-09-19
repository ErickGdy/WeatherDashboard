using System;
using System.Collections.Generic;
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

        public JsonResult GetWeather(string City, string State, string DateStart,string DateEnd, string Scale)
        {
            if (!string.IsNullOrWhiteSpace(City) && !string.IsNullOrWhiteSpace(State) && !string.IsNullOrWhiteSpace(DateStart) && !string.IsNullOrWhiteSpace(DateEnd) && !string.IsNullOrWhiteSpace(Scale))
            {
                Weather weather = new Weather();
                return Json(weather.getWeather(City, State, DateStart, DateEnd, Scale), JsonRequestBehavior.AllowGet);
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