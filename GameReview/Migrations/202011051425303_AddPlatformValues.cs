using System.Data.Entity.Migrations.Model;

namespace GameReview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlatformValues : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Platforms SET ApiPlatformId = 49 WHERE Name = 'Xbox One';");
            Sql("UPDATE Platforms SET ApiPlatformId = 12 WHERE Name = 'Xbox 360';");
            Sql("UPDATE Platforms SET ApiPlatformId = 130 WHERE Name = 'Nintendo Switch';");
            Sql("UPDATE Platforms SET ApiPlatformId = 48 WHERE Name = 'PlayStation 4';");
            Sql("UPDATE Platforms SET ApiPlatformId = 9 WHERE Name = 'PlayStation 3';");
            Sql("UPDATE Platforms SET ApiPlatformId = 92 WHERE Name = 'Steam';");
            Sql("UPDATE Platforms SET ApiPlatformId = 167 WHERE Name = 'PlayStation 5';");
            Sql("UPDATE Platforms SET ApiPlatformId = 169 WHERE Name = 'Xbox Series X';");
            Sql("UPDATE Platforms SET ApiPlatformId = 34 WHERE Name = 'Android';");
            Sql("UPDATE Platforms SET ApiPlatformId = 39 WHERE Name = 'iOS';");
        }
        
        public override void Down()
        {
        }
    }
}
