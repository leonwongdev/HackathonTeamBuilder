using HackathonTeamBuilder.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace HackathonTeamBuilder.Controllers
{
    public class TeamController : Controller
    {

        HttpClient client;
        JavaScriptSerializer jss;
        public TeamController()
        {
            client = new HttpClient();
            client.BaseAddress = new System.Uri(Constant.BASE_URL);
        }
        /// <summary>
        /// Display a list of teams for a hackthon
        /// </summary>
        /// <param name="id">hackthon id</param>
        /// <returns></returns>
        public ActionResult List(int Id)
        {
            HttpResponseMessage response = client.GetAsync($"teamdata/ListTeamsByHackathon/{Id}").Result;
            List<Team> teams = response.Content.ReadAsAsync<List<Team>>().Result;

            return View(teams);
        }
    }
}