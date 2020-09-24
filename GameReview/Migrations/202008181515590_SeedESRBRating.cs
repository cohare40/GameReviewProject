namespace GameReview.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class SeedESRBRating : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO AgeRatings (Rating, ImageAddress, AgeRatingApiLink) VALUES('Everyone', 'https://www.esrb.org/wp-content/uploads/2019/05/E.svg', 8)");

            Sql("INSERT INTO AgeRatings (Rating, ImageAddress, AgeRatingApiLink) VALUES('Everyone 10+', 'https://www.esrb.org/wp-content/uploads/2019/05/E10plus.svg', 9)");

            Sql("INSERT INTO AgeRatings (Rating, ImageAddress, AgeRatingApiLink) VALUES('Teen', 'https://www.esrb.org/wp-content/uploads/2019/05/T.svg', 10)");

            Sql("INSERT INTO AgeRatings (Rating, ImageAddress, AgeRatingApiLink) VALUES('Mature', 'https://www.esrb.org/wp-content/uploads/2019/05/M.svg', 11)");

            Sql("INSERT INTO AgeRatings (Rating, ImageAddress, AgeRatingApiLink) VALUES('Adults Only', 'https://www.esrb.org/wp-content/uploads/2019/05/AO.svg', 12)");

            Sql("INSERT INTO AgeRatings (Rating, ImageAddress, AgeRatingApiLink) VALUES('Rating Pending', 'https://www.esrb.org/wp-content/uploads/2019/05/RP.svg', 6)");
        }
        
        public override void Down()
        {
        }
    }
}
