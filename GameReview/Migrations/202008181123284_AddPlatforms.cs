namespace GameReview.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddPlatforms : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Platforms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ImageAddress = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Platforms");
        }
    }
}
