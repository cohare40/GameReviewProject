using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json.Linq;

namespace GameReview.Models
{
    public class IGDBService
    {
        public async Task<Game> GetGameDetailsAsync(int id)
        {
            var formattedJson = "";
            int ageRatingValues = 0;
            string[] genreValues = { };
            string[] companiesValues = { };
            string[] platformsValues = { };
            string[] coverValues = { };
            string[] websiteValues = { };
            var source = new CancellationTokenSource();
            var token = source.Token;
            var gameFields =
                $"fields age_ratings, cover, first_release_date, genres, involved_companies, name, platforms, rating, summary, websites, id; where id = {id};";

            var iGdb = new IGDB(); //create igdb object to post game info request
            var game = await iGdb.PostBasicAsync(gameFields, token, "https://api-v3.igdb.com/games");


            //IDs that need more processing
            //Age Rating
            var jsonAgeRating = game.Children<JObject>()["age_ratings"];
            if (jsonAgeRating.Any())
            {
                formattedJson = GetPostMessage(jsonAgeRating.First().ToArray());
                var ageRatingPostMsg = $"fields rating; where id = ({formattedJson}) & category = 2;";
                var ageRatingResult = await iGdb.PostBasicAsync(ageRatingPostMsg, token, "https://api-v3.igdb.com/age_ratings");
                if (ageRatingResult.Children<JObject>()["rating"].Any())
                    ageRatingValues =  ageRatingResult.Children<JObject>()["rating"].FirstOrDefault().Value<int>();
            }

            //Genres
            var jsonGenres = game.Children<JObject>()["genres"].First();
            if (jsonGenres.Any())
            {
                formattedJson = GetPostMessage(jsonGenres.ToArray());
                var genrePostMsg = $"fields name; where id = ({formattedJson});";
                var genreResult = await iGdb.PostBasicAsync(genrePostMsg, token, "https://api-v3.igdb.com/genres");
                genreValues = genreResult.Children<JObject>()["name"].Values<string>().ToArray();
            }

            //Companies
            var jsonCompanies = game.Children<JObject>()["involved_companies"].First();
            if (jsonCompanies.Any())
            {
                formattedJson = GetPostMessage(jsonCompanies.ToArray());
                var companiesPostMsg = $"fields name; where id = ({formattedJson});";
                var companiesResult = await iGdb.PostBasicAsync(companiesPostMsg, token, "https://api-v3.igdb.com/companies");
                companiesValues = companiesResult.Children<JObject>()["name"].Values<string>().ToArray();
            }

            //Platforms
            var jsonPlatforms = game.Children<JObject>()["platforms"].First();
            if (jsonPlatforms.Any())
            {
                formattedJson = GetPostMessage(jsonPlatforms.ToArray());
                var platformsPostMsg = $"fields name; where id = ({formattedJson});";
                var platformsResult = await iGdb.PostBasicAsync(platformsPostMsg, token, "https://api-v3.igdb.com/platforms");
                platformsValues = platformsResult.Children<JObject>()["name"].Values<string>().ToArray();
            }

            //Cover
            var jsonCover = game.Children<JObject>()["cover"].First().ToString();
            var coverPostMsg = $"fields url; where id = ({jsonCover});";
            var coverResult = await iGdb.PostBasicAsync(coverPostMsg, token, "https://api-v3.igdb.com/covers");
            coverValues = coverResult.Children<JObject>()["url"].Values<string>().ToArray();

            //Website
            var jsonWebsite = game.Children<JObject>()["websites"].First().ToArray();
            formattedJson = GetPostMessage(jsonWebsite);
            var websitePostMsg = $"fields url; where id = ({formattedJson});";
            var websiteResult = await iGdb.PostBasicAsync(websitePostMsg, token, "https://api-v3.igdb.com/websites");
            websiteValues = websiteResult.Children<JObject>()["url"].Values<string>().ToArray();

            //Date released
            var unixTimestamp = game.Children<JObject>()["first_release_date"].First().Value<double>();

            //Finalised variables for game
            var gameName = game.Children<JObject>()["name"].First().ToString();
            DateTime gameReleaseDate = ConvertFromUnixTimestamp(unixTimestamp);
            var gameSummary = game.Children<JObject>()["summary"].First().ToString();
            var gameAgeRating = Enum.GetName(typeof(AgeRatingEnum), ageRatingValues);
            var gameGenres = string.Join(", ", genreValues.ToList());
            var gameCompanies = string.Join(", ", companiesValues);
            var gamePlatforms = string.Join(", ", platformsValues);
            var gameCover = coverValues.First().Replace("thumb", "cover_big");
            var gameWebsite = websiteValues.First();
            var gameId = game.Children<JObject>()["id"].First().Value<int>();

            var gameObj = new Game
            {
                Id = gameId,
                Name = gameName,
                FirstReleaseDate = gameReleaseDate.ToShortDateString(),
                Summary = gameSummary,
                AgeRatingImage = gameAgeRating,
                GenreName = gameGenres,
                InvolvedCompanies = gameCompanies,
                Platforms = gamePlatforms,
                CoverArtUrl = gameCover,
                Websites = gameWebsite
            };

            return gameObj;
        }

        public string GetPostMessage(JToken[] json)
        {
            var jsonToList = json.Values<string>().ToList();
            var formattedJson = string.Join(", ", jsonToList);

            return formattedJson;
        }

        static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }
    }
}