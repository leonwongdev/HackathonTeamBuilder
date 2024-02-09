using HackathonTeamBuilder.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;
namespace HackathonTeamBuilder.Controllers
{
    public class TeamMemberController : Controller
    {
        HttpClient client;
        JavaScriptSerializer jss;

        public TeamMemberController()
        {
            client = new HttpClient();
            jss = new JavaScriptSerializer();
            client.BaseAddress = new Uri(Constant.BASE_URL);
        }


        /// <summary>
        /// CRUD - CREATE:
        /// Create a record in the ApplicationUserTeams table, meaning a user is joining a team
        /// </summary>
        /// <param name="userTeam">a record to represent the team and hackathon user is joining to</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult JoinTeam(ApplicationUserTeam userTeam)
        {
            var jsonPayload = jss.Serialize(userTeam);
            var content = new StringContent(jsonPayload);
            content.Headers.ContentType.MediaType = "application/json";

            var response = client.PostAsync("teammemberdata/jointeam", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List", new { Id = userTeam.TeamId });
            }
            else
            {
                ViewBag.ErrorMessage = "Fail to join this team. You are already in another team for this same hackathon";
                return View("Error");
            }
        }

        /// <summary>
        /// display all member by team id
        /// </summary>
        /// <param name="id">team Id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult List(int id)
        {
            var response = client.GetAsync($"teammemberdata/list/{id}").Result;
            var memberList = response.Content.ReadAsAsync<List<ApplicationUserTeam>>().Result;

            if (memberList.Count < 0)
            {
                ViewBag.ErrorMessage = "Unable to find any team member.";
                return View("Error");
            }

            return View(memberList);
        }



        /// <summary>
        /// CRUD - DELETE:
        /// Deleting a record in the ApplicationUserTeams table meaning the a user is quiting a team.
        /// </summary>
        /// <param name="userTeam">a record to represent the team and hackathon user is quiting from</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult QuitTeam(ApplicationUserTeam userTeam)
        {
            var jsonPayload = jss.Serialize(userTeam);
            var content = new StringContent(jsonPayload);
            content.Headers.ContentType.MediaType = "application/json";

            var response = client.PostAsync("teammemberdata/quitteam", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List", new { Id = userTeam.TeamId });
            }
            else
            {
                ViewBag.ErrorMessage = "Fail to quit this team.";
                return View("Error");
            }
        }
    }
}