using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HackathonTeamBuilder.Models
{
    /// <summary>
    /// Junction Class / Assocation class of between Team and Application User
    /// </summary>
    public class ApplicationUserTeam
    {
        // Using composite Key here to prevent one user joing multiple teams of the same hackathon
        [Key, Column(Order = 0), ForeignKey("User")]
        public string UserId { get; set; }

        [Key, Column(Order = 1), ForeignKey("Team")]
        public int TeamId { get; set; }

        [Key, Column(Order = 2), ForeignKey("Hackathon")]
        public int HackathonId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Team Team { get; set; }
        public virtual Hackathon Hackathon { get; set; }
    }
}