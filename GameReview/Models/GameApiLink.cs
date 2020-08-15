using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameReview.Models
{
    public class GameApiLink
    {
        public int Id { get; set; }
        [Required]
        public int GameIdentifier { get; set; }
        [Required]
        public string Name { get; set; }


    }
}