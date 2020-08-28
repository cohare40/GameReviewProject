using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace GameReview.Models
{
    public class Platform
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageAddress { get; set; }
        public int ApiGenreId { get; set; }
    }
}