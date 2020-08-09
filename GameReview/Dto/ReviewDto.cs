using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using GameReview.Models;

namespace GameReview.Dto
{
    public class ReviewDto
    {
        
        public int Id { get; set; }

        [Required]
        
        public string UserId { get; set; }

        [Required]
        public string ReviewText { get; set; }

        [Required]
        public int RatingScore { get; set; }

        [Required] public int gameId { get; set; }

        public ApplicationUser User { get; set; }
    }
}