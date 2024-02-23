using HackathonTeamBuilder.Models;
using System.Diagnostics;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace HackathonTeamBuilder.Controllers
{
    public class HackathonController : Controller
    {
        private readonly HttpClient client;
        private readonly JavaScriptSerializer jss;
        public HackathonController()
        {
            client = new HttpClient();
            client.BaseAddress = new System.Uri(Constant.BASE_URL);
            jss = new JavaScriptSerializer();
        }
        // GET: Hackathon/Detail
        public ActionResult Detail()
        {

            return View();
        }

        // GET: Hackathon/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // Post: Hackathon/Create
        [HttpPost]
        public ActionResult Create(Hackathon hackathon)
        {
            string url = "hackathondata/create";
            string jsonpayload = jss.Serialize(hackathon);
            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // return to error action and give the error message
                return RedirectToAction("Error", new { message = "Unable to create the hackathon" });
            }
        }

        /// <summary>
        /// Render Edit form for hackathon
        /// </summary>
        /// <param name="id">hackathon id</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            //the existing hackathon information
            string endpoint = "hackathondata/findbyid/" + id;
            HttpResponseMessage response = client.GetAsync(endpoint).Result;
            Hackathon selectedHackathon = response.Content.ReadAsAsync<Hackathon>().Result;
            // check if the selectedHackathon is null
            if (selectedHackathon == null)
            {
                // return to error action and give the error message
                return RedirectToAction("Error", new { message = "Unable to find the hackathon for editing" });
            }
            return View(selectedHackathon);
        }

        [HttpPost]
        public ActionResult Edit(Hackathon updatedHackathon)
        {
            string url = "hackathondata/edit/" + updatedHackathon.Id;
            string jsonpayload = jss.Serialize(updatedHackathon);
            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // return to error action and give the error message
                return RedirectToAction("Error", new { message = "Unable to update the hackathon" });
            }

        }

        [HttpGet]
        public ActionResult DeleteConfirm(int id)
        {
            //the existing hackathon information
            string endpoint = "hackathondata/findbyid/" + id;
            HttpResponseMessage response = client.GetAsync(endpoint).Result;
            Hackathon selectedHackathon = response.Content.ReadAsAsync<Hackathon>().Result;
            // add null checking for the selectedHackathon
            if (selectedHackathon == null)
            {
                // return to error action and give the error message
                return RedirectToAction("Error", new { message = "Unable to find the hackathon fo deletion" });
            }

            return View(selectedHackathon);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            string url = "hackathondata/delete/" + id;
            HttpResponseMessage response = client.DeleteAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // return to error action and give the error message
                return RedirectToAction("Error", new { message = "Unable to delete the hackathon" });
            }
        }

        // GET: Hackathon/Error
        public ActionResult Error(string message)
        {
            // using named parameter to pass the message to the view
            return View("Error", model: message);
        }
    }
}