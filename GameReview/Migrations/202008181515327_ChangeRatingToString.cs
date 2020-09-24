namespace GameReview.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class ChangeRatingToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AgeRatings", "Rating", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AgeRatings", "Rating", c => c.Int(nullable: false));
        }
    }
}
