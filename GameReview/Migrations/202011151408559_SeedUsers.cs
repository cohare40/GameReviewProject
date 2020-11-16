namespace GameReview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [ProfilePic], [FirstName], [LastName]) VALUES (N'32f5c548-3a9b-40cc-903e-57bd6c4f6f6b', N'Cameronohare.98@hotmail.co.uk', 0, N'AKAtTdO7E7W7zKmdP189nP6r2JFRpw7345c3Y9Fjoce4FOM3vwTgbgb2RwEykWVjMg==', N'0e1b91fa-389d-43b0-aa7c-53fed54f8338', NULL, 0, 0, NULL, 1, 0, N'Cameronohare.98@hotmail.co.uk', NULL, N'Cameron', N'O''Hare')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [ProfilePic], [FirstName], [LastName]) VALUES (N'50971739-ab42-4525-ad08-197d364139d9', N'admin@admin.com', 0, N'ABYrilexOkG9tcKYsPzcoRnpZGE5kNqkxd9mRTvoUzUl/hMatGEuM1V0/Hf+3qJ/Hg==', N'6f216354-fc31-42b2-a072-8813634b702c', NULL, 0, 0, NULL, 1, 0, N'admin@admin.com', NULL, N'Admin', N'Account')


INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'6e959893-f34a-45c6-98cf-38fb24ec2ac2', N'admin')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'6e959893-f34a-45c6-98cf-38fb24ec2ac2', N'admin')

");
        }
        
        public override void Down()
        {
        }
    }
}
