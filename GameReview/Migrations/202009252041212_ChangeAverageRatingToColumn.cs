namespace GameReview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeAverageRatingToColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GameApiLinks", "AverageRating", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GameApiLinks", "AverageRating");
        }
    }
}
