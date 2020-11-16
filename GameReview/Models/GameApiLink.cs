using System.ComponentModel.DataAnnotations;

namespace GameReview.Models
{
    public class GameApiLink
    {
        public int Id { get; set; }
        [Required]

        [Display (Name = "GameID")]
        public int GameIdentifier { get; set; }
        [Required]
        public string Name { get; set; }

        public string GenreIds { get; set; }

        public double AverageRating { get; set; }

    }
}