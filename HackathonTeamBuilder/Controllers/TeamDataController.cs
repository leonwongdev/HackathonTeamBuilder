using HackathonTeamBuilder.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Http;

namespace HackathonTeamBuilder.Controllers
{
    public class TeamDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/teamdata/list
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

        // GET: api/teamdata/find/{id}
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
        // GET: api/teamdata/ListTeamsByHackathon/{id}
        [HttpGet]
        public IHttpActionResult ListTeamsByHackathon(int id)
        {
            try
            {
                List<TeamViewModel> teamsWithUsers = db.Teams
                .Where(t => t.HackathonId == id)
                .Select(t => new TeamViewModel
                {
                    Team = t,
                    TeamLeader = db.Users.FirstOrDefault(u => u.Id == t.TeamLeaderId)
                })
                .ToList();

                return Ok(teamsWithUsers);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        public IHttpActionResult CreateTeamWithLeader([FromBody] Team team)
        {
            // Using transaction here to make sure both team and the relationship can be created successfully togather.
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {

                    // Step 1: Create the Team
                    db.Teams.Add(team);
                    db.SaveChanges(); // This will generate the TeamId

                    // Step 2: Create the ApplicationUserTeam record
                    ApplicationUserTeam userTeam = new ApplicationUserTeam
                    {
                        UserId = team.TeamLeaderId,
                        TeamId = team.Id,
                        HackathonId = team.HackathonId
                    };

                    db.ApplicationUserTeams.Add(userTeam);
                    db.SaveChanges();

                    transaction.Commit();
                    return Ok();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Debug.WriteLine(ex);
                    return InternalServerError();
                }
            }
        }
        // POST: api/teamdata/create
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

        // POST: api/teamdata/update
        [HttpPost]
        public IHttpActionResult Update([FromBody] Team updatedTeam)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Team existingTeam = db.Teams.Find(updatedTeam.Id);

                if (existingTeam == null)
                {
                    return NotFound();
                }

                // For MVP, Only allowing User to change requirments
                existingTeam.Requirements = updatedTeam.Requirements;

                db.SaveChanges();

                return Ok(existingTeam);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/teamdata/delete/{id}
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    // Remove all records of ApplicationUserTeam by TeamId to maintain referential integrity
                    var userTeamRecords = db.ApplicationUserTeams.Where(aut => aut.TeamId == id);
                    db.ApplicationUserTeams.RemoveRange(userTeamRecords);

                    // Delete the Team by Id
                    var teamToDelete = db.Teams.Find(id);
                    if (teamToDelete == null)
                    {
                        return NotFound(); // Team not found
                    }

                    db.Teams.Remove(teamToDelete);
                    db.SaveChanges();

                    transaction.Commit(); // If everything is successful, commit the transaction
                    return Ok();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    return InternalServerError(ex);
                }
            }
        }
    }
}
