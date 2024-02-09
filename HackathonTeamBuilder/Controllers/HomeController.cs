using HackathonTeamBuilder.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace HackathonTeamBuilder.Controllers
{
    public class HomeController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static HomeController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(Constant.BASE_URL);
        }

        /// <summary>
        /// For display a list of Hackathon on the home page
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            string endpoint = "hackathondata/listall";
            HttpResponseMessage response = client.GetAsync(endpoint).Result;
            List<Hackathon> hackathons = response.Content.ReadAsAsync<List<Hackathon>>().Result;
            return View(hackathons);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}