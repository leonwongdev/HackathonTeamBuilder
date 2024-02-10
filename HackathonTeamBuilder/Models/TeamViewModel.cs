using System.Collections.Generic;

namespace HackathonTeamBuilder.Models
{
    public class TeamViewModel
    {
        public List<TeamDTO> TeamDTO { get; set; }

        public Hackathon Hackathon { get; set; } // current hackathon
    }
}