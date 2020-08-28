﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameReview.Models;
using Newtonsoft.Json;

namespace GameReview.Models
{

    public enum AgeRatingEnum
    {
        Three = 1,
        Seven = 2,
        Twelve = 3,
        Sixteen = 4,
        Eighteen = 5,
        RP = 6,
        EC = 7,
        E = 8,
        E10 = 9,
        T = 10,
        M = 11,
        AO = 12
    }


    public class Game

    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<string> AgeRatingImage { get; set; }

        public string CoverArtUrl { get; set; }

        public string FirstReleaseDate { get; set; }

        public IEnumerable<string> GenreName { get; set; }

        public IEnumerable<int> Platforms { get; set; }

        public string Summary { get; set; }

        public string Websites { get; set; }

        public IEnumerable<string> InvolvedCompanies{ get; set; }
    }
}