using System.Collections.Generic;
using System.Linq;
using GameReview.Models;
using GameReview.Modules;

namespace GameReview.ViewModels
{
    public class GameDetailsViewModel
    {
        public GameDetailsViewModel(Game game)
        {
            var gameService = new DbService();
            Game = game;
            Reviews = gameService.GetReviews(game.Id);
            Platforms = gameService.GetPlatforms(game.Platforms);
            if (Reviews.Any())
            {
                AverageRating = gameService.GetAverageRating(game.Id);
            }
        }


        public Game Game { get; set; }
        public Review CreateReview { get; set; }

        public List<Review> Reviews { get; set; }
        public List<Platform> Platforms { get; set; }
        public double AverageRating { get; set; } = 0;
    }
}