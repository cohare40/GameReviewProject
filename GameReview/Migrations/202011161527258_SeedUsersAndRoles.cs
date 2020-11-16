namespace GameReview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsersAndRoles : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [ProfilePic], [FirstName], [LastName]) VALUES (N'4394800a-dc4a-4115-91ad-bb66bfd897ef', N'admin@admin.com', 0, N'APCAiiAezf7L5FvxFlfsDnPTKFUmL4H8nD50HoHZkfJ5jioU2AVLM3DmtggKKwG3+Q==', N'fe6ca5d0-c737-4818-a2cc-45deab981e2d', NULL, 0, 0, NULL, 1, 0, N'admin@admin.com', NULL, N'Admin', N'Admin')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [ProfilePic], [FirstName], [LastName]) VALUES (N'6cd7db88-b173-46f2-9d23-292191f1385e', N'cameronohare.98@hotmail.co.uk', 0, N'AJGmVc0BiFd0tdPqDvWvniGgIHDSDrRO9mDoFaAdHIfNHUv3FnnRQNupAeKpMJCyEQ==', N'8e9733eb-3e0e-4e24-95e3-829a1b2f8aa2', NULL, 0, 0, NULL, 1, 0, N'cameronohare.98@hotmail.co.uk', NULL, N'Cameron', N'O''Hare')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'fe9fcd14-b7d9-4f74-aa7b-f6039d3dae8a', N'Admin')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'4394800a-dc4a-4115-91ad-bb66bfd897ef', N'fe9fcd14-b7d9-4f74-aa7b-f6039d3dae8a')

");
        }
        
        public override void Down()
        {
        }
    }
}
