namespace GameReview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RejigTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Platforms", "ApiPlatformId", c => c.Int(nullable: false));
            DropColumn("dbo.Platforms", "ApiGenreId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Platforms", "ApiGenreId", c => c.Int(nullable: false));
            DropColumn("dbo.Platforms", "ApiPlatformId");
        }
    }
}
