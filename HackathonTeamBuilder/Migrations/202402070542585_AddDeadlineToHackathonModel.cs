namespace HackathonTeamBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeadlineToHackathonModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Hackathons", "Deadline", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Hackathons", "Deadline");
        }
    }
}
