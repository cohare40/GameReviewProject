using System.ComponentModel.DataAnnotations;

namespace GameReview.Models
{
    public class Review
    {
        
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        
        [Display(Name = "Review")]
        public string ReviewText { get; set; }

        [Required]
        [Display(Name = "Rating")]
        public int RatingScore { get; set; }

        [Required]
        public int GameId { get; set; }

        
        public ApplicationUser User { get; set; }

    }
}