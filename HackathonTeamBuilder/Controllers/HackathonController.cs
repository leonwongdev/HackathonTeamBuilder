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
                return RedirectToAction("Error");
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
                return RedirectToAction("Error");
            }

        }

        [HttpGet]
        public ActionResult DeleteConfirm(int id)
        {
            //the existing hackathon information
            string endpoint = "hackathondata/findbyid/" + id;
            HttpResponseMessage response = client.GetAsync(endpoint).Result;
            Hackathon selectedHackathon = response.Content.ReadAsAsync<Hackathon>().Result;
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
                return RedirectToAction("Error");
            }
        }
    }
}