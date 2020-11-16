using System.Collections.Generic;
using System.Linq;
using GameReview.Models;
using GameReview.Modules;

namespace GameReview.ViewModels
{
    public class GameItemViewModel
    {
        public Game Game { get; set; }

        public List<string> PlatformImageUrls { get; set; }

        public List<Review> Reviews { get; set; }
        public double AverageRating { get; set; } = 0;


        public GameItemViewModel(Game game, ApplicationDbContext context)
        {
            var gameService = new DbService();
            Game = game;
            PlatformImageUrls = Game.Platforms
                .Select(platform => context.Platforms.SingleOrDefault(p => p.ApiPlatformId == platform))
                .Where(platformToAdd => platformToAdd != null)
                .Select(p => p.ImageAddress)
                .ToList();
            Reviews = gameService.GetReviews(game.Id);
            if (Reviews.Any())
            {
                AverageRating = gameService.GetAverageRating(game.Id);
            }
        }

    }
}