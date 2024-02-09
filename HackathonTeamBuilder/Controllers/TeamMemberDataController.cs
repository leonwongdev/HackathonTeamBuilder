using HackathonTeamBuilder.Models;
using System;
using System.Linq;
using System.Web.Http;

namespace HackathonTeamBuilder.Controllers
{
    public class TeamMemberDataController : ApiController
    {
        ApplicationDbContext context = new ApplicationDbContext();

        [HttpGet]
        public IHttpActionResult Create()
        {
            return Ok();

        }

        /// <summary>
        /// CRUD - READ
        /// List all team member by team id.
        /// </summary>
        /// <param name="id">Team Id</param>
        /// <returns></returns>
        /// GET /api/teammemberdata/list/{id}
        [HttpGet]
        public IHttpActionResult List(int id)
        {
            try
            {
                // Query ApplicationUserTeam to get all records where TeamId equals the provided id
                var teamMembers = context.ApplicationUserTeams
                    .Where(aut => aut.TeamId == id)
                    .ToList();

                return Ok(teamMembers);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }


        /// <summary>
        /// CRUD - CREATE:
        /// Create a record in the ApplicationUserTeams table, meaning a user is joining a team
        /// </summary>
        /// <param name="userTeam">a record to represent the team and hackathon user is joining to</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult JoinTeam(ApplicationUserTeam userTeam)
        {

            /*            ApplicationUserTeam userTeam = new ApplicationUserTeam
                        {
                            UserId = team.TeamLeaderId,
                            TeamId = team.Id,
                            HackathonId = team.HackathonId
                        };*/
            try
            {
                context.ApplicationUserTeams.Add(userTeam);
                context.SaveChanges();
                return Ok(userTeam);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// CRUD - DELETE:
        /// Create a record in the ApplicationUserTeams table, meaning a user is joining a team
        /// </summary>
        /// <param name="userTeam">a record to represent the team and hackathon user is joining to</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult QuitTeam(ApplicationUserTeam userTeam)
        {

            try
            {
                // Attach the team to the context first to fix the issue of
                // "The object cannot be deleted because it was not found in the ObjectStateManager"
                context.ApplicationUserTeams.Attach(userTeam);
                context.ApplicationUserTeams.Remove(userTeam);
                context.SaveChanges();
                return Ok(userTeam);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
