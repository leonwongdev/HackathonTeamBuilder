using System.Collections.Generic;

namespace HackathonTeamBuilder.Models
{
    public class HackathonsViewModel
    {
        public List<Hackathon> Hackathons { get; set; }
        public ApplicationUser CurrentUser { get; set; }
    }
}