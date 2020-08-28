namespace GameReview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedAgeRating : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO AgeRatings (Rating, ImageAddress) VALUES (3, 'https://pegi.info/sites/default/files/inline-images/age-3-black_0.jpg')");

            Sql("INSERT INTO AgeRatings (Rating, ImageAddress) VALUES (7, 'https://pegi.info/sites/default/files/inline-images/age-7-black.jpg')");

            Sql("INSERT INTO AgeRatings (Rating, ImageAddress) VALUES (12, 'https://pegi.info/sites/default/files/inline-images/age-12-black.jpg')");

            Sql("INSERT INTO AgeRatings (Rating, ImageAddress) VALUES (16, 'https://pegi.info/sites/default/files/inline-images/age-16-black.jpg')");

            Sql("INSERT INTO AgeRatings (Rating, ImageAddress) VALUES (18, 'https://pegi.info/sites/default/files/inline-images/age-18-black%202_0.jpg')");
        }
        
        public override void Down()
        {
        }
    }
}
