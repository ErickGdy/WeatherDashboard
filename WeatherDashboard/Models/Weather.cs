using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace WeatherDashboard.Models
{
    public class Weather
    {
        private string CountryCode = "MX";
        //private string KeyAPI = "d886958a47e54146beb2bf216d01adc1";
        private string KeyAPI = "ad6f842661a04f91a1abc13e5eabf8bd";
        //private string KeyAPI = "9be7376621824790a057346903925db3";
        private string rootURL = "https://api.weatherbit.io/v2.0/forecast/daily?";
        private string rootURLHistory = "https://api.weatherbit.io/v2.0/history/daily?";
        public string getWeather(string City, string State, string DateStart, string DateEnd, string Scale)
        {
            string url = "city=" + City + "&country=" + CountryCode + "&state=" + State + "&start_date=" + DateStart+ "&end_date=" + DateEnd;
            if (Scale == "I")
                url += "&units=" + Scale;
            url = Uri.EscapeUriString(url);
            var client = new WebClient();
            url = rootURLHistory + url + "&key=" + KeyAPI;
            string content = client.DownloadString(url);
            return content;
        }

        public Object getWeatherDays(string City, string State, string days, string Scale)
        {
            string url = "city=" + City + "&country=" + CountryCode + "&state=" + State + "&days=" + days;
            if (Scale == "I")
                url += "&units=" + Scale;
            url = Uri.EscapeUriString(url);
            var client = new WebClient();
            url = rootURL + url + "&key=" + KeyAPI;
            var content = client.DownloadString(url);
            var serializer = new JavaScriptSerializer();
            var jsonContent = serializer.Deserialize<Object>(content);
            return jsonContent;
        }
    }
}