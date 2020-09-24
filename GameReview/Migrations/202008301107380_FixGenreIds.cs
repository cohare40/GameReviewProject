namespace GameReview.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class FixGenreIds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GameApiLinks", "GenreIds", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GameApiLinks", "GenreIds");
        }
    }
}
