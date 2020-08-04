namespace GameReview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovingOldUserAccountModelFromReviewModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reviews", "UserAccount_Id", "dbo.UserAccounts");
            DropIndex("dbo.Reviews", new[] { "UserAccount_Id" });
            DropColumn("dbo.Reviews", "UserAccount_Id");
            DropTable("dbo.UserAccounts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        ProfilePicture = c.String(),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Reviews", "UserAccount_Id", c => c.Int());
            CreateIndex("dbo.Reviews", "UserAccount_Id");
            AddForeignKey("dbo.Reviews", "UserAccount_Id", "dbo.UserAccounts", "Id");
        }
    }
}
