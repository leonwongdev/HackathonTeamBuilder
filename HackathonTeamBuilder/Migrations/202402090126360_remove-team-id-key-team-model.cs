namespace HackathonTeamBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeteamidkeyteammodel : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ApplicationUserTeams");
            AddPrimaryKey("dbo.ApplicationUserTeams", new[] { "UserId", "HackathonId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ApplicationUserTeams");
            AddPrimaryKey("dbo.ApplicationUserTeams", new[] { "UserId", "TeamId", "HackathonId" });
        }
    }
}
