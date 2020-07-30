using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GameReview.Models;
using Newtonsoft.Json;

namespace GameReview.Models
{

    public enum GenreEnum
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

    public enum PlatFormEnum
    {
        console = 1,
        arcade = 2,
        platform = 3,
        operating_system = 4,
        portable_console = 5,
        computer= 6
    }

    public class Game

    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("age_ratings")]
        public  IEnumerable<int> AgeRatingImageUrl { get; set; }

        [JsonProperty("cover")]
        public string CoverArtUrl { get; set; }

        [JsonProperty("first_release_date")]
        public DateTime FirstReleaseDate { get; set; }

        [JsonProperty("genres")]
        public IEnumerable<int> GenreName { get; set; }

        [JsonProperty("platforms")]
        public IEnumerable<int> Platforms { get; set; }

        [JsonProperty("rating")]
        public double RatingScore{ get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("websites")]
        public string Websites { get; set; }

        [JsonProperty("involved_companies")]
        public IEnumerable<int> InvolvedCompanies{ get; set; }

        //Add in constructor to set these values based on id given
    }
}