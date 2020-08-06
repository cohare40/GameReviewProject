namespace GameReview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeReviewPropertyTypeToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reviews", "UserAccountId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reviews", "UserAccountId", c => c.Byte(nullable: false));
        }
    }
}
