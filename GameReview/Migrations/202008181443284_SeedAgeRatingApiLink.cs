namespace GameReview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedAgeRatingApiLink : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE AgeRatings SET AgeRatingApiLink = 1 WHERE Rating = 3");
            Sql("UPDATE AgeRatings SET AgeRatingApiLink = 2 WHERE Rating = 7");
            Sql("UPDATE AgeRatings SET AgeRatingApiLink = 3 WHERE Rating = 12");
            Sql("UPDATE AgeRatings SET AgeRatingApiLink = 4 WHERE Rating = 16");
            Sql("UPDATE AgeRatings SET AgeRatingApiLink = 5 WHERE Rating = 18");
        }
        
        public override void Down()
        {
        }
    }
}
