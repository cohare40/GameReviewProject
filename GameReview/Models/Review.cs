using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameReview.Models
{
    public class Review
    {
        
        public int Id { get; set; }

        [Required]
        public string UserAccountId { get; set; }

        [Required]
        
        [Display(Name = "Review")]
        public string ReviewText { get; set; }

        [Required]
        [Display(Name = "Rating")]
        public int RatingScore { get; set; }

        [Required] public int gameId { get; set; }
    }
}