namespace GameReview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApiLinkToAgeRating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AgeRatings", "AgeRatingApiLink", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AgeRatings", "AgeRatingApiLink");
        }
    }
}
