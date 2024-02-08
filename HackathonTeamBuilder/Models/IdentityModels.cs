using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HackathonTeamBuilder.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        // Passion project: Add new fields to User table

        public string FullName { get; set; }
        public string Bio { get; set; }
        public string LinkedinUrl { get; set; }
        public string GithubUrl { get; set; }
        public string PortfolioUrl { get; set; }
        public string Role { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        // Add Entity
        public DbSet<Hackathon> Hackathons { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<ApplicationUserTeam> TeamApplicationUsers { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUserTeam>()
                .HasRequired(e => e.Team)
                .WithMany()
                .HasForeignKey(e => e.TeamId)
                .WillCascadeOnDelete(false); // Specify no cascade delete for Team relationship

            base.OnModelCreating(modelBuilder);
        }
    }
}