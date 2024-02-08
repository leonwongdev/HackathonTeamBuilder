using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HackathonTeamBuilder.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }

        // Foreign key reference to Hackathon, the name has to be same as the name of nvigation property below
        [ForeignKey("Hackathon")]
        public int HackathonId { get; set; }

        // Navigation property
        public virtual Hackathon Hackathon { get; set; }
        public string TeamLeaderId { get; set; }
        public string Requirements { get; set; }
        public int MaxNumOfMembers { get; set; }
    }
}