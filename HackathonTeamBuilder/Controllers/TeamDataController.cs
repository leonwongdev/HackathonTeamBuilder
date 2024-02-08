using HackathonTeamBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace HackathonTeamBuilder.Controllers
{
    public class TeamDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /teamdata/list
        [HttpGet]
        public IHttpActionResult List()
        {
            try
            {
                List<Team> teams = db.Teams.ToList();
                return Ok(teams);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: /teamdata/find/{id}
        [HttpGet]
        public IHttpActionResult Find(int id)
        {
            try
            {
                Team team = db.Teams.Find(id);

                if (team == null)
                {
                    return NotFound();
                }

                return Ok(team);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Get a list of teams by hackathon id
        /// </summary>
        /// <param name="id">hackathon id</param>
        /// <returns></returns>
        // GET: /teamdata/ListTeamsByHackathon/{id}
        [HttpGet]
        public IHttpActionResult ListTeamsByHackathon(int id)
        {
            try
            {
                List<Team> teams = db.Teams.Where(t => t.HackathonId == id).ToList();

                return Ok(teams);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        // POST: /teamdata/create
        [HttpPost]
        public IHttpActionResult Create(Team team)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                db.Teams.Add(team);
                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = team.Id }, team);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: /teamdata/update/{id}
        [HttpPut]
        public IHttpActionResult Update(int id, Team updatedTeam)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Team existingTeam = db.Teams.Find(id);

                if (existingTeam == null)
                {
                    return NotFound();
                }

                existingTeam.TeamLeaderId = updatedTeam.TeamLeaderId;
                existingTeam.Requirements = updatedTeam.Requirements;
                existingTeam.MaxNumOfMembers = updatedTeam.MaxNumOfMembers;

                db.SaveChanges();

                return Ok(existingTeam);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: /teamdata/delete/{id}
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                Team team = db.Teams.Find(id);

                if (team == null)
                {
                    return NotFound();
                }

                db.Teams.Remove(team);
                db.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
