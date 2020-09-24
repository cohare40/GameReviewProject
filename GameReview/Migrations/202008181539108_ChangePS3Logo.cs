namespace GameReview.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class ChangePS3Logo : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Platforms SET ImageAddress = 'https://logoeps.com/wp-content/uploads/2011/06/sony-ps3-slim-logo-vector-01.png' WHERE Id = 5");
        }
        
        public override void Down()
        {
        }
    }
}
