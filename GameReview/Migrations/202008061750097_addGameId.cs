namespace GameReview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addGameId : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GameIds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GameIdentifier = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GameIds");
        }
    }
}
