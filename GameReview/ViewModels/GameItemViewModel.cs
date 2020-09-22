using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using GameReview.Models;

namespace GameReview.ViewModels
{
    public class GameItemViewModel
    {
        public Game Game { get; set; }

        public List<string> PlatformImageUrls { get; set; }


        public GameItemViewModel(Game game, ApplicationDbContext context)
        {
            Game = game;
            PlatformImageUrls = Game.Platforms
                .Select(platform => context.Platforms.SingleOrDefault(p => p.ApiGenreId == platform))
                .Where(platformToAdd => platformToAdd != null)
                .Select(p => p.ImageAddress)
                .ToList();
        }

    }
}