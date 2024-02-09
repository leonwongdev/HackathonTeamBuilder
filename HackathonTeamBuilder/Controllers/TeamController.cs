using HackathonTeamBuilder.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace HackathonTeamBuilder.Controllers
{
    [Authorize]
    public class TeamController : Controller
    {

        HttpClient client;
        JavaScriptSerializer jss;
        public TeamController()
        {
            client = new HttpClient();
            client.BaseAddress = new System.Uri(Constant.BASE_URL);
            jss = new JavaScriptSerializer();
        }
        /// <summary>
        /// Display a list of teams for a hackthon
        /// </summary>
        /// <param name="id">hackthon id</param>
        /// <returns></returns>
        public ActionResult List(int Id)
        {
            HttpResponseMessage response = client.GetAsync($"teamdata/ListTeamsByHackathon/{Id}").Result;
            List<TeamViewModel> teamViewModels = response.Content.ReadAsAsync<List<TeamViewModel>>().Result;

            return View(teamViewModels);
        }


        /// <summary>
        /// For displaying team creation form.
        /// </summary>
        /// <param name="hackathonId">Id of a hackathon</param>
        /// <returns></returns>
        public ActionResult Create(int hackathonId)
        {
            var response = client.GetAsync($"hackathondata/findbyid/{hackathonId}").Result;
            var hackathon = response.Content.ReadAsAsync<Hackathon>().Result;
            var UserId = User.Identity.GetUserId();

            var tempTeam = new Team
            {
                HackathonId = hackathonId,
                TeamLeaderId = UserId,
                Hackathon = hackathon
            };

            return View(tempTeam);
        }

        /// <summary>
        /// For sending form data of new teamto the web api layer.
        /// </summary>
        /// <param name="team">New Team data</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(Team team)
        {
            // Build payload
            string jsonpayload = jss.Serialize(team);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            var response = client.PostAsync($"teamdata/CreateTeamWithLeader", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List", new { Id = team.HackathonId });
            }
            else
            {
                ViewBag.ErrorMessage = "Unable to create team, you might have already created a team for this hackathon.";
                return View("Error");
            }
        }

        /// <summary>
        /// For displaying the form to update team requirements.
        /// </summary>
        /// <param name="id">team id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Update(int id)
        {
            var currentUserId = User.Identity.GetUserId();
            var response = client.GetAsync($"teamdata/find/{id}").Result;
            var team = response.Content.ReadAsAsync<Team>().Result;
            if (team == null)
            {
                ViewBag.ErrorMessage = "Unable to render form for updating team info: Team not found.";
                return View("Error");
            }
            else if (team.TeamLeaderId != currentUserId)
            {
                ViewBag.ErrorMessage = "Unable to update team info as you are not the team leader of this team.";
                return View("Error");
            }
            return View(team);
        }


        /// <summary>
        /// For sending data to the web api layer for updating team data in database
        /// </summary>
        /// <param name="team">New team data</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Update(Team team)
        {
            string payload = jss.Serialize(team);
            HttpContent content = new StringContent(payload);
            content.Headers.ContentType.MediaType = "application/json";
            var response = client.PostAsync($"teamdata/update/", content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List", new { Id = team.HackathonId });
            }
            else
            {
                ViewBag.ErrorMessage = "Unable to update team info. Contact Administrator please.";
                return View("Error");
            }
        }


        /// <summary>
        /// Display delete confirm page
        /// </summary>
        /// <param name="id">Id of Team</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteConfirm(int id)
        {
            var currentUserId = User.Identity.GetUserId();
            var response = client.GetAsync($"teamdata/find/{id}").Result;
            var team = response.Content.ReadAsAsync<Team>().Result;
            if (team == null)
            {
                ViewBag.ErrorMessage = "Unable to display delete page. Reason: Team not found.";
                return View("Error");
            }
            else if (team.TeamLeaderId != currentUserId)
            {
                ViewBag.ErrorMessage = "Unable to display delete page. Reason: You are not the team leader, only team leader can delete this team .";
                return View("Error");
            }
            return View(team);
        }

        /// <summary>
        /// For sending neccessary data to web api layer to delete a team in database
        /// </summary>
        /// <param name="id">team id</param>
        /// <param name="hackathonId">hackathon id, for redirect to team listing page of a ceratin hackathon.</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Delete(int id, int hackathonId)
        {

            string url = "teamdata/delete/" + id;
            HttpResponseMessage response = client.DeleteAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List", new { Id = hackathonId });
            }
            else
            {
                ViewBag.ErrorMessage = "Failed to delete the team.";
                return View("Error");
            }
        }
    }
}