namespace GameReview.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddedGenres : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.GameIds", newName: "GameApiLinks");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.GameApiLinks", newName: "GameIds");
        }
    }
}
