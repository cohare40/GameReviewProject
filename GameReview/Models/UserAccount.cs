using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameReview.Models
{
    public class UserAccount
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string ProfilePicture { get; set; }

        [Required]
        public string Name { get; set; }
    }
}