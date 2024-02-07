using HackathonTeamBuilder.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Http;

namespace HackathonTeamBuilder.Controllers
{
    public class HackathonDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET /HackathonData/ListAll
        [HttpGet]
        public IHttpActionResult ListAll()
        {

            try
            {
                List<Hackathon> hackathons = db.Hackathons.ToList();
                return Ok(hackathons);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Debug.WriteLine(ex.Message);
                return InternalServerError(new Exception("An error occurred while fetching hackathons.", ex));
            }

        }
    }
}
