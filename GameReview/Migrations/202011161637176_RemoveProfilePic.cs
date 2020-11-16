namespace GameReview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveProfilePic : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "ProfilePic");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "ProfilePic", c => c.String());
        }
    }
}
