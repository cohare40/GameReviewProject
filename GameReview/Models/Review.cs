using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameReview.Models
{
    public class Review
    {
        [Required]
        public int Id { get; set; }

        public UserAccount UserAccount { get; set; }

        [Required]
        public byte UserAccountId { get; set; }

        [Required]
        public string ReviewText { get; set; }

        [Required]
        public int RatingScore { get; set; }

        [Required] public int gameId { get; set; }
    }
}