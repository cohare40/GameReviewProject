namespace GameReview.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class SeedPlatform : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Platforms (Name, ImageAddress, ApiGenreId) VALUES ('Xbox One', 'https://images.igdb.com/igdb/image/upload/t_logo_med/pl6a.png', 49)");

            Sql("INSERT INTO Platforms (Name, ImageAddress, ApiGenreId) VALUES ('Xbox 360', 'https://images.igdb.com/igdb/image/upload/t_logo_med/pl6z.png', 12)");

            Sql("INSERT INTO Platforms (Name, ImageAddress, ApiGenreId) VALUES ('Nintendo Switch', 'https://images.igdb.com/igdb/image/upload/t_logo_med/pl6b.png', 130)");

            Sql("INSERT INTO Platforms (Name, ImageAddress, ApiGenreId) VALUES ('PlayStation 4', 'https://images.igdb.com/igdb/image/upload/t_logo_med/pl6e.png', 48)");

            Sql("INSERT INTO Platforms (Name, ImageAddress, ApiGenreId) VALUES ('PlayStation 3', 'https://images.igdb.com/igdb/image/upload/t_logo_med/pl6l.png', 9)");

            Sql("INSERT INTO Platforms (Name, ImageAddress, ApiGenreId) VALUES ('Steam', 'https://images.igdb.com/igdb/image/upload/t_logo_med/pl6i.png', 92)");

            Sql("INSERT INTO Platforms (Name, ImageAddress, ApiGenreId) VALUES ('PlayStation 5', 'https://images.igdb.com/igdb/image/upload/t_logo_med/plcv.png', 167)");

            Sql("INSERT INTO Platforms (Name, ImageAddress, ApiGenreId) VALUES ('Xbox Series X', 'https://images.igdb.com/igdb/image/upload/t_logo_med/plfl.png', 169)");

            Sql("INSERT INTO Platforms (Name, ImageAddress, ApiGenreId) VALUES ('Android', 'https://images.igdb.com/igdb/image/upload/t_logo_med/plag.png', 34)");

            Sql("INSERT INTO Platforms (Name, ImageAddress, ApiGenreId) VALUES ('iOS', 'https://images.igdb.com/igdb/image/upload/t_logo_med/pl6w.png', 39)");
        }
        
        public override void Down()
        {
        }
    }
}
