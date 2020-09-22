namespace GameReview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlatformDescriptionColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Platforms", "PlatformDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Platforms", "PlatformDescription");
        }
    }
}
