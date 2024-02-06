namespace HackathonTeamBuilder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnewfieldsUsermodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Bio", c => c.String());
            AddColumn("dbo.AspNetUsers", "FullName", c => c.String());
            AddColumn("dbo.AspNetUsers", "LinkedinUrl", c => c.String());
            AddColumn("dbo.AspNetUsers", "GithubUrl", c => c.String());
            AddColumn("dbo.AspNetUsers", "PortfolioUrl", c => c.String());
            AddColumn("dbo.AspNetUsers", "Role", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Role");
            DropColumn("dbo.AspNetUsers", "PortfolioUrl");
            DropColumn("dbo.AspNetUsers", "GithubUrl");
            DropColumn("dbo.AspNetUsers", "LinkedinUrl");
            DropColumn("dbo.AspNetUsers", "FullName");
            DropColumn("dbo.AspNetUsers", "Bio");
        }
    }
}
