using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameReview.Models;

namespace GameReview.ViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<Game> TopRatedGames { get; set; }
        public IEnumerable<Game> DiscoverGames { get; set; }
    }
}