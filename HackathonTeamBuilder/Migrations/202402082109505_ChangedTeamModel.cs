namespace HackathonTeamBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedTeamModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Teams", "Requirements", c => c.String(maxLength: 300));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Teams", "Requirements", c => c.String());
        }
    }
}
