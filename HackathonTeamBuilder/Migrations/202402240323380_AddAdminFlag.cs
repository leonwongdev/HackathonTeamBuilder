namespace HackathonTeamBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAdminFlag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "isAdministrator", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "isAdministrator");
        }
    }
}
