namespace GameReview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldToGenre : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Genres", "IgdbId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Genres", "IgdbId");
        }
    }
}
