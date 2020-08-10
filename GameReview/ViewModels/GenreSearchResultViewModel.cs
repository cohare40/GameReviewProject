using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameReview.Models;

namespace GameReview.ViewModels
{
    public class GenreSearchResultViewModel
    {
        public IEnumerable<Game> Games { get; set; }
    }
}