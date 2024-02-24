using HackathonTeamBuilder.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web;
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

            /*Get current user*/
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext()
                .GetUserManager<ApplicationUserManager>()
                .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            HackathonsViewModel hackathonsViewModel = new HackathonsViewModel();
            hackathonsViewModel.Hackathons = hackathons;
            hackathonsViewModel.CurrentUser = user;
            return View(hackathonsViewModel);
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