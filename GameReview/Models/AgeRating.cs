using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameReview.Models
{
    public class AgeRating
    {
        public int Id { get; set; }
        public string Rating { get; set; }
        public string ImageAddress { get; set; }
        public int AgeRatingApiLink { get; set; }

    }
}