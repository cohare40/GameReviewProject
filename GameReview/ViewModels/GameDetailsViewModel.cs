using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameReview.Models;

namespace GameReview.ViewModels
{
    public class GameDetailsViewModel
    {
        public Game Game { get; set; }
        public Review Review1 { get; set; }
        public IEnumerable<Review> Review { get; set; }
        public List<ApplicationUser> User { get; set; }
    }
}