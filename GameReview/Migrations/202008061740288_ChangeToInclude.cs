using System.Data.SqlTypes;

namespace GameReview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeToInclude : DbMigration
    {
        public override void Up()
        {
            
            AddColumn("dbo.Reviews", "UserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Reviews", "UserId");
            AddForeignKey("dbo.Reviews", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            DropColumn("dbo.Reviews", "UserAccountId");
            
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reviews", "UserAccountId", c => c.String(nullable: false));
            DropForeignKey("dbo.Reviews", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Reviews", new[] { "UserId" });
            DropColumn("dbo.Reviews", "UserId");
        }
    }
}
