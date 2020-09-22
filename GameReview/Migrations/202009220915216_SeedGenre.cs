namespace GameReview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedGenre : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Genres (Name, IgdbId, BackgroundImgUrl) VALUES ('Fighting', 4, 'https://i.imgur.com/H2S25ib.jpg')");

            Sql("INSERT INTO Genres (Name, IgdbId, BackgroundImgUrl) VALUES ('Shooter', 5, 'https://i.imgur.com/AxV29E2.jpg')");

            Sql("INSERT INTO Genres (Name, IgdbId, BackgroundImgUrl) VALUES ('Music', 7, 'https://i.imgur.com/lpyDW5N.jpg')");

            Sql("INSERT INTO Genres (Name, IgdbId, BackgroundImgUrl) VALUES ('Platform', 8, 'https://i.imgur.com/Hpz7xoI.jpg')");

            Sql("INSERT INTO Genres (Name, IgdbId, BackgroundImgUrl) VALUES ('Puzzle', 9, 'https://i.imgur.com/RsquSjw.png')");

            Sql("INSERT INTO Genres (Name, IgdbId, BackgroundImgUrl) VALUES ('Racing', 10, 'https://i.imgur.com/hfuwkvL.jpg')");

            Sql("INSERT INTO Genres (Name, IgdbId, BackgroundImgUrl) VALUES ('Real Time Strategy', 11, 'https://i.imgur.com/zapFcuh.jpg')");

            Sql("INSERT INTO Genres (Name, IgdbId, BackgroundImgUrl) VALUES ('Role Playing Game', 12, 'https://i.imgur.com/Z8RryvM.jpg')");

            Sql("INSERT INTO Genres (Name, IgdbId, BackgroundImgUrl) VALUES ('Simulator', 13, 'https://i.imgur.com/HEy3lq2.jpg')");

            Sql("INSERT INTO Genres (Name, IgdbId, BackgroundImgUrl) VALUES ('Sport', 14, 'https://i.imgur.com/prfDh6m.png')");

            Sql("INSERT INTO Genres (Name, IgdbId, BackgroundImgUrl) VALUES ('Strategy', 15, 'https://i.imgur.com/HL7wRod.jpg')");

            Sql("INSERT INTO Genres (Name, IgdbId, BackgroundImgUrl) VALUES ('Turn-based Strategy', 16, 'https://i.imgur.com/uN52Ou8.jpg')");

            Sql("INSERT INTO Genres (Name, IgdbId, BackgroundImgUrl) VALUES ('Quiz/Trivia', 26, 'https://i.imgur.com/x29DqOJ.jpg')");

            Sql("INSERT INTO Genres (Name, IgdbId, BackgroundImgUrl) VALUES ('Hack and Slash', 25, 'https://i.imgur.com/ufHoRbm.jpg')");

            Sql("INSERT INTO Genres (Name, IgdbId, BackgroundImgUrl) VALUES ('Pinball', 30, 'https://i.imgur.com/4AwYe0i.jpg')");

            Sql("INSERT INTO Genres (Name, IgdbId, BackgroundImgUrl) VALUES ('Adventure', 31, 'https://i.imgur.com/qMwkOCN.jpg')");

            Sql("INSERT INTO Genres (Name, IgdbId, BackgroundImgUrl) VALUES ('Arcade', 33, 'https://i.imgur.com/x4baI0L.jpg')");

            Sql("INSERT INTO Genres (Name, IgdbId, BackgroundImgUrl) VALUES ('Visual Novel', 34, 'https://i.imgur.com/FvRgiLK.jpg')");

            Sql("INSERT INTO Genres (Name, IgdbId, BackgroundImgUrl) VALUES ('Indie', 32, 'https://i.imgur.com/M2clHyP.jpg')");

            Sql("INSERT INTO Genres (Name, IgdbId, BackgroundImgUrl) VALUES ('Card & Board Game', 35, 'https://i.imgur.com/UhHwFsY.jpg')");

            Sql("INSERT INTO Genres (Name, IgdbId, BackgroundImgUrl) VALUES ('MOBA', 36, 'https://i.imgur.com/6mEGBjK.jpg')");

            Sql("INSERT INTO Genres (Name, IgdbId, BackgroundImgUrl) VALUES ('Point and Click', 2, 'https://i.imgur.com/bK5sO0f.jpg')");
        }
        
        public override void Down()
        {
        }
    }
}
