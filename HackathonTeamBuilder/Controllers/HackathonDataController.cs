using HackathonTeamBuilder.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace HackathonTeamBuilder.Controllers
{
    public class HackathonDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        /// <summary>
        /// For getting all hackathon records from the DB.
        /// </summary>
        /// <returns>200 OK with a list of hackathon data</returns>
        // GET api/HackathonData/ListAll
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

        // GET /api/hackathondata/findbyid/{id}
        [HttpGet]
        public IHttpActionResult FindById(int id)
        {

            try
            {
                Hackathon hackathon = db.Hackathons.Find(id);
                if (hackathon == null)
                {
                    return NotFound();
                }
                return Ok(hackathon);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Debug.WriteLine(ex.Message);
                return InternalServerError(new Exception("An error occurred while fetching hackathons.", ex));
            }

        }

        // POST: /api/hackathondata/create
        [HttpPost]
        public IHttpActionResult Create(Hackathon hackathon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Hackathons.Add(hackathon);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = hackathon.Id }, hackathon);
        }

        // POST: /api/hackathondata/edit/{id}
        [HttpPost]
        public IHttpActionResult Edit(int id, Hackathon updatedHackathon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != updatedHackathon.Id)
            {

                return BadRequest();
            }

            db.Entry(updatedHackathon).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IsHackathonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: /api/hackathondata/delete/{id}
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var hackathon = db.Hackathons.Find(id);

            if (hackathon == null)
            {
                return NotFound();
            }

            db.Hackathons.Remove(hackathon);
            db.SaveChanges();
            return Ok();
        }

        private bool IsHackathonExists(int id)
        {
            return db.Hackathons.Count(h => h.Id == id) > 0;
        }
    }
}
