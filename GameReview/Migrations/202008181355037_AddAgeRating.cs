namespace GameReview.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddAgeRating : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AgeRatings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rating = c.Int(nullable: false),
                        ImageAddress = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AgeRatings");
        }
    }
}
