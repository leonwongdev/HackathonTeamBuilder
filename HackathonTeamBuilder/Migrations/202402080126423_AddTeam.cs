namespace HackathonTeamBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTeam : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HackathonId = c.Int(nullable: false),
                        TeamLeaderId = c.String(),
                        Requirements = c.String(),
                        MaxNumOfMembers = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hackathons", t => t.HackathonId, cascadeDelete: true)
                .Index(t => t.HackathonId);
            
            CreateTable(
                "dbo.ApplicationUserTeams",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        TeamId = c.Int(nullable: false),
                        HackathonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.TeamId, t.HackathonId })
                .ForeignKey("dbo.Hackathons", t => t.HackathonId, cascadeDelete: true)
                .ForeignKey("dbo.Teams", t => t.TeamId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.TeamId)
                .Index(t => t.HackathonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUserTeams", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserTeams", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.ApplicationUserTeams", "HackathonId", "dbo.Hackathons");
            DropForeignKey("dbo.Teams", "HackathonId", "dbo.Hackathons");
            DropIndex("dbo.ApplicationUserTeams", new[] { "HackathonId" });
            DropIndex("dbo.ApplicationUserTeams", new[] { "TeamId" });
            DropIndex("dbo.ApplicationUserTeams", new[] { "UserId" });
            DropIndex("dbo.Teams", new[] { "HackathonId" });
            DropTable("dbo.ApplicationUserTeams");
            DropTable("dbo.Teams");
        }
    }
}
