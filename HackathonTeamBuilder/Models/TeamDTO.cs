using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackathonTeamBuilder.Models
{
    public class TeamDTO
    {
        public Team Team { get; set; }
        public ApplicationUser TeamLeader {  get; set; }
    }
}