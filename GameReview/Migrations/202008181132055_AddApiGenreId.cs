namespace GameReview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApiGenreId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Platforms", "ApiGenreId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Platforms", "ApiGenreId");
        }
    }
}
