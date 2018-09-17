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
            //List<SelectListItem> Cities = new List<SelectListItem>();
            //var Ciudades = StatesCitiesModel.GetCiudades("Sonora");
            //for (int i = 0; i < Ciudades.Length; i++)
            //{
            //    Cities.Add(new SelectListItem {  Value = Ciudades[i] });
            //}
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

        [HttpPost]
        public ActionResult GetCities(string iso3)
        {
            if (!string.IsNullOrWhiteSpace(iso3) && iso3.Length == 3)
            {
                ViewData["States"] = iso3;
                ViewData["Cities"] = StatesCitiesModel.GetCiudades(iso3);
                return Json(StatesCitiesModel.GetCiudades(iso3),JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        public JsonResult GetWeather(string City, string State, string DateStart,string DateEnd, string Scale)
        {
            Weather weather = new Weather();
            return Json(weather.getWeather(City, State, DateStart,DateEnd,Scale),JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetWeatherDays(string City, string State, string Days, string Scale)
        {
            Weather weather = new Weather();
            return Json(weather.getWeatherDays(City, State, Days, Scale), JsonRequestBehavior.AllowGet);
        }
    }
}