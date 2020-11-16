namespace GameReview.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedReviews : DbMigration
    {
        public override void Up()
        {
            Sql(@"
SET IDENTITY_INSERT [dbo].[Reviews] ON
INSERT INTO [dbo].[Reviews] ([Id], [ReviewText], [RatingScore], [gameId], [UserId]) VALUES (18, N'Boring very quickly', 40, 989, N'6cd7db88-b173-46f2-9d23-292191f1385e')
INSERT INTO [dbo].[Reviews] ([Id], [ReviewText], [RatingScore], [gameId], [UserId]) VALUES (19, N'Best Halo game', 100, 989, N'6cd7db88-b173-46f2-9d23-292191f1385e')
INSERT INTO [dbo].[Reviews] ([Id], [ReviewText], [RatingScore], [gameId], [UserId]) VALUES (20, N'Terrific', 80, 990, N'6cd7db88-b173-46f2-9d23-292191f1385e')
INSERT INTO [dbo].[Reviews] ([Id], [ReviewText], [RatingScore], [gameId], [UserId]) VALUES (21, N'Definitive VR game', 80, 72766, N'6cd7db88-b173-46f2-9d23-292191f1385e')
INSERT INTO [dbo].[Reviews] ([Id], [ReviewText], [RatingScore], [gameId], [UserId]) VALUES (22, N'Awful game', 20, 1879, N'6cd7db88-b173-46f2-9d23-292191f1385e')
INSERT INTO [dbo].[Reviews] ([Id], [ReviewText], [RatingScore], [gameId], [UserId]) VALUES (23, N'Boring after a while', 60, 1879, N'6cd7db88-b173-46f2-9d23-292191f1385e')
INSERT INTO [dbo].[Reviews] ([Id], [ReviewText], [RatingScore], [gameId], [UserId]) VALUES (24, N'Addictive', 100, 1879, N'6cd7db88-b173-46f2-9d23-292191f1385e')
INSERT INTO [dbo].[Reviews] ([Id], [ReviewText], [RatingScore], [gameId], [UserId]) VALUES (25, N'Favourite game at the minute', 80, 1879, N'565fd90e-4a30-43f6-8d0b-44adb456f60b')
SET IDENTITY_INSERT [dbo].[Reviews] OFF

");
        }
        
        public override void Down()
        {
        }
    }
}
