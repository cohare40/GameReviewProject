namespace GameReview.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class addGameIdLinkFieldToReview : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "gameId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reviews", "gameId");
        }
    }
}
