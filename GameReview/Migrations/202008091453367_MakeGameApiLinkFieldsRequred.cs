namespace GameReview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeGameApiLinkFieldsRequred : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.GameApiLinks", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.GameApiLinks", "Name", c => c.String());
        }
    }
}
