namespace GameReview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedGames : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO GameApiLinks (GameIdentifier, Name, GenreIds) VALUES (60226, 'Angry Birds Fight!', 9)");
            Sql("INSERT INTO GameApiLinks (GameIdentifier, Name, GenreIds) VALUES (138149, 'Junior Arithmancer', 31)");
            Sql("INSERT INTO GameApiLinks (GameIdentifier, Name) VALUES (100750, 'Rattle')");
            Sql("INSERT INTO GameApiLinks (GameIdentifier, Name) VALUES (105842, 'Robots Vs Zombies: Transform To Race And Fight')");
            Sql("INSERT INTO GameApiLinks (GameIdentifier, Name) VALUES (53297, 'Lords of Xulima - The Talisman of Golot Edition')");
            Sql("INSERT INTO GameApiLinks (GameIdentifier, Name) VALUES (89616, 'Bubble Whirl Shooter')");
            Sql("INSERT INTO GameApiLinks (GameIdentifier, Name, GenreIds) VALUES (39455, 'Inspector X', 5)");
            Sql("INSERT INTO GameApiLinks (GameIdentifier, Name) VALUES (85450, 'Transformers Prime: The Game')");
            Sql("INSERT INTO GameApiLinks (GameIdentifier, Name, GenreIds) VALUES (135802, 'Over the Alps: King of the Mountain', 31)");
            Sql("INSERT INTO GameApiLinks (GameIdentifier, Name) VALUES (51663, 'The Firemen 2: Pete & Danny')");

            Sql("INSERT INTO GameApiLinks (GameIdentifier, Name, GenreIds) VALUES (13257, 'Sensible Soccer: European Champions', 14)");
            Sql("INSERT INTO GameApiLinks (GameIdentifier, Name, GenreIds) VALUES (27241, 'Shovel Knight: King of Cards', 8)");
            Sql("INSERT INTO GameApiLinks (GameIdentifier, Name, GenreIds) VALUES (989, 'Tomb Robber', 12)");
            Sql("INSERT INTO GameApiLinks (GameIdentifier, Name, GenreIds) VALUES (90655, 'Halo 3: ODST', 5)");
            Sql("INSERT INTO GameApiLinks (GameIdentifier, Name, GenreIds) VALUES (990, 'Halo: Reach', 5)");
            Sql("INSERT INTO GameApiLinks (GameIdentifier, Name, GenreIds) VALUES (1879, 'Terraria', 8)");
        }
        
        public override void Down()
        {
        }
    }
}
