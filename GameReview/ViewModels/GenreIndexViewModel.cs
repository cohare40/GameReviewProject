using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameReview.Models;

namespace GameReview.ViewModels
{
    public class GenreIndexViewModel
    {
        public IEnumerable Genres { get; set; }
    }
}