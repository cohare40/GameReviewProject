using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GameReview.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace GameReview.Controllers
{
    public class GameController : Controller
    {


        // GET: Game
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Details()
        {
            var source = new CancellationTokenSource();
            var token = source.Token;
            var gameFields =
                "fields age_ratings, cover, first_release_date, genres, involved_companies, name, platforms, rating, summary, websites, id; where id = 1544;";

            var iGdb = new IGDB(); //create igdb object to post game info request
            var game =await iGdb.PostBasicAsync(gameFields, token, "https://api-v3.igdb.com/games");
            
            var gameName = game.Children<JObject>()["name"].First().ToString();
            var gameReleaseDate = game.Children<JObject>()["first_release_date"].First().ToString();
            var gameSummary = game.Children<JObject>()["summary"].First().ToString();
            var gameRating = game.Children<JObject>()["rating"].First().ToString();

            //Only return IDs that need more processing
            var jsonAgeRating = game.Children<JObject>()["age_ratings"].First().ToArray();
            var testing = jsonAgeRating.Values<string>().ToList();
            var formattedJson = string.Join(", ", testing);
            var ageRatingPostMsg = $"fields rating; where id = ({formattedJson});";
            

            var gameGenres = game.Children<JObject>()["genres"].First().ToArray();
            var gameCompanies = game.Children<JObject>()["involved_companies"].First().ToArray();
            var gamePlatforms = game.Children<JObject>()["platforms"].First().ToArray();
            var gameCover = game.Children<JObject>()["cover"].First().ToString();
            var gameWebsite = game.Children<JObject>()["websites"].First().ToArray();

            var ageRatingResult = await iGdb.PostBasicAsync(ageRatingPostMsg, token, "https://api-v3.igdb.com/age_ratings");


            var testGame1 = new Game
            {
                //AgeRatingImageUrl = "Eighteen",
                CoverArtUrl = "https://images.igdb.com/igdb/image/upload/t_cover_big/co1r0o.jpg",
                FirstReleaseDate = new DateTime(2020, 07, 14),
                GenreName = new int[] {1, 3, 5},
                Id = 21,
                InvolvedCompanies = new int[] {5, 7 ,8},
                Name = "tes",
                Platforms = new int[] {4, 6, 7},
                RatingScore = 90,
                Websites = "www.trialsofmana.com",
                Summary =
                    "Set 5 years after the events of The Last of Us, we see the return of Joel and Ellie. Driven by hatred, Ellie sets out for Seattle to serve justice. However, she begins to wonder what justice really means."
            };



            return View();
        }

        
    }
}