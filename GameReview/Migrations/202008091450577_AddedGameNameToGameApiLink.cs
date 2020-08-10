namespace GameReview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedGameNameToGameApiLink : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GameApiLinks", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GameApiLinks", "Name");
        }
    }
}
