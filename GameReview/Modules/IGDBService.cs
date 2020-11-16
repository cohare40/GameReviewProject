using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GameReview.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json.Linq;

namespace GameReview.Modules
{
    public class IGDBService
    {
        private const string GamesUrl = "https://api.igdb.com/v4/games";
        private const string AgeUrl = "https://api.igdb.com/v4/age_ratings";
        private const string CompaniesBaseUrl = "https://api.igdb.com/v4/companies";
        private const string CompaniesDetailsUrl = "https://api.igdb.com/v4/involved_companies";
        private const string GenreUrl = "https://api.igdb.com/v4/genres";
        private const string CoversUrl = "https://api.igdb.com/v4/covers";

        private const string GetGenreString = "fields *;";
        private const string PlaceholderImageUrl =
            "https://www.cgteam.com/wp-content/uploads/2018/12/img-placeholder.png";

        private const string ClientSecret = "8qk4riamdpboi79hagapbsfdise6fy";
        private const string ClientId = "9kztw06pcdhoib5z0w6ysmjlcgn5q9";

        private readonly ApplicationDbContext _context;
        private readonly IGDB IGDB;

        public IGDBService()
        {
            _context = new ApplicationDbContext();
            IGDB = new IGDB(); //create igdb object to post game info request
        }

        public async Task<Game> GetGameDetailsAsync(int id)
        {
            string[] websiteValues = { };
            var token = new CancellationTokenSource().Token;
            var gameFields =
                $"fields age_ratings, cover, first_release_date, genres, involved_companies, name, platforms, rating, summary, websites, id; where id = {id};";

            var game = await IGDB.PostBasicAsync(gameFields, token, GamesUrl);


            //Finalised variables for game
            var gameName = game
                .Children<JObject>()["name"]
                .First()
                .ToString();

            var gameId = game
                .Children<JObject>()["id"]
                .First()
                .Value<int>();

            var gameSummary = game
                .Children<JObject>()["summary"]
                .FirstOrDefault()
                ?.ToString();

            // Gets dictionary with genre ID as key and genre name as value
            var genreDetails = await GetGenresAsync(game);

            var gameWebsite = websiteValues.FirstOrDefault();

            var gameObj = new Game
            {
                Id = gameId,
                Name = gameName,
                FirstReleaseDate = GetReleaseDate(game).ToString(),
                Summary = gameSummary,
                AgeRatingImage = await GetAgeRatingsAsync(game),
                GenreName = genreDetails.Values.ToList(),
                GenreIds = ListToString(genreDetails.Keys.ToList()),
                InvolvedCompanies = await GetCompaniesAsync(game),
                Platforms = GetPlatforms(game),
                CoverArtUrl = await GetCoverUrl(game),
                Websites = gameWebsite
            };

            return gameObj;
        }

        public async Task<IEnumerable<Genre>> GetAllGenresAsync()
        {
            var token = new CancellationTokenSource().Token;
            
            var iGdb = new IGDB();
            var genreJArray = await iGdb.PostBasicAsync(GetGenreString, token, GenreUrl);


            return (from genreResult in genreJArray
                let genreId = (int) genreResult.SelectToken("id")
                let genreName = genreResult.SelectToken("name").ToString()
                select new Genre {Id = genreId, Name = genreName}).ToList();
        }

        public async Task<List<Game>> GetSearchResultsAsync(string idList, string searchFilter,
            string searchType)
        {
            string[] websiteValues = { };
            var token = new CancellationTokenSource().Token;

            var topRatedSearchString =
                $"fields genres, first_release_date, platforms, cover, name, id; where id = ({idList});";
            var genreSearchString =
                $"fields genres, first_release_date, platforms, cover, name, id; where id = ({idList}) & genres = ({searchFilter});";
            var nameSearchString =
                $"fields genres, first_release_date, platforms, cover, name, id; where id = ({idList}) & name ~ *\"{searchFilter}\"*;";

            var iGdb = new IGDB(); //create igdb object to post game info request
            var game = new JArray();

            //Case for what type of search (genre, name, etc)
            switch (searchType)
            {
                case "genre":
                    game = await iGdb.PostBasicAsync(genreSearchString, token, GamesUrl);
                    break;
                case "name":
                    game = await iGdb.PostBasicAsync(nameSearchString, token, GamesUrl);
                    break;
                case "topRated":
                    game = await iGdb.PostBasicAsync(topRatedSearchString, token, GamesUrl);
                    break;
            }

            var gameList = new List<Game>();
            foreach (var gameJToken in game) //each game has its details assigned to object + added to game list
            {
                var gameName = gameJToken
                    .SelectToken("name")
                    .ToString();

                var gameId = gameJToken
                    .SelectToken("id")
                    .Value<int>();
                // Gets dictionary with genre ID as key and genre name as value
                var genreDetails = await GetGenresAsync(gameJToken);

                var gameWebsite = websiteValues.FirstOrDefault();

                var gameObj = new Game
                {
                    Id = gameId,
                    Name = gameName,
                    FirstReleaseDate = GetReleaseDate(gameJToken).ToString(),
                    GenreName = genreDetails.Values.ToList(),
                    GenreIds = ListToString(genreDetails.Keys.ToList()),
                    Platforms = GetPlatforms(gameJToken),
                    CoverArtUrl = await GetCoverUrl(gameJToken),
                    Websites = gameWebsite
                };
                gameList.Add(gameObj);
            }

            //returns list of games to be iterated over in search results 
            return gameList;
        }

        //Returns all games matching search to be added to db
        public async Task<IEnumerable<Game>> GetGameToAddSearchResultsAsync(string searchFilter, int pageNumber)
        {
            var token = new CancellationTokenSource().Token;
            var nameSearchString =
                $"fields cover, name, id; where name ~ *\"{searchFilter}\"*; limit 10; offset {pageNumber * 10};";

            var iGdb = new IGDB(); //create igdb object to post game info request
            var game = await iGdb.PostBasicAsync(nameSearchString, token, GamesUrl);

            var gameList = new List<Game>();
            foreach (var gameJToken in game) //each game has its details assigned to object + added to game list
            {
                var gameId = gameJToken
                    .SelectToken("id")
                    .Value<int>();

                var gameName = gameJToken
                    .SelectToken("name")
                    .Value<string>();

                var basicGameDetails = new Game
                {
                    Id = gameId,
                    Name = gameName,
                    CoverArtUrl = await GetCoverUrl(gameJToken)
                };
                 gameList.Add(basicGameDetails);
            }

            //returns list of games to be iterated over in search results 
            return gameList;
        }

        //Get "x" number of games, used for getting top rated games
        public async Task<IEnumerable<Game>> GetGamesIdAndPicAsync(IEnumerable<int> idList)
        {
            var token = new CancellationTokenSource().Token;

            var topRatedSearchString =
                $"fields cover, id; where id = ({string.Join(", ", idList)});";

            var iGdb = new IGDB(); //create igdb object to post game info request
            var game = await iGdb.PostBasicAsync(topRatedSearchString, token, GamesUrl);

            var gameList = new List<Game>();
            foreach (JToken gameJToken in game.Children()) //each game has its details assigned to object + added to game list
            {
                var gameId = gameJToken
                    .Value<int>("id");

                var basicGameDetails = new Game
                {
                    Id = gameId,
                    CoverArtUrl = await GetCoverUrl(gameJToken)
                };
                gameList.Add(basicGameDetails);
            }
        

            //returns list of games to be iterated over in search results 
            return gameList;
        }

        public string GetPostMessage(JToken[] json) //Formats the values to search for 
        {
            var jsonToList = json.Values<string>().ToList();
            var formattedJson = string.Join(", ", jsonToList);

            return formattedJson;
        }

        private async Task<List<string>> GetAgeRatingsAsync(JToken game)
        {
            //Age Rating
            var jsonAgeRating = game.Children<JObject>()["age_ratings"];

            var ageRatingValues = new List<string>();

            if (jsonAgeRating.Any())
            {
                var formattedString = GetPostMessage(jsonAgeRating.First().ToArray());
                var token = new CancellationTokenSource().Token;

                var ageRatingPostMsg = $"fields rating; where id = ({formattedString});";
                var ageRatingResult =
                    await IGDB.PostBasicAsync(ageRatingPostMsg, token, AgeUrl);

                if (ageRatingResult.Children<JObject>()["rating"].Any())
                {
                    var returnedRating = ageRatingResult.Children<JObject>()["rating"];
                    var values = returnedRating.Values<int>();
                    // LOOK HERE

                    ageRatingValues.AddRange(from rating in values
                        select _context.AgeRatings.SingleOrDefault(r => r.AgeRatingApiLink == rating)
                        into ageRatingInDb
                        where ageRatingInDb != null
                        select ageRatingInDb.ImageAddress);
                }
            }

            return ageRatingValues;
        }

        private async Task<Dictionary<string, string>> GetGenresAsync(JToken game)
        {
            var jsonGenres = game
                .Children<JObject>()["genres"]
                .FirstOrDefault();

            var genreDetails =
                new Dictionary<string, string>();

            if (jsonGenres != null)
            {
                var token = new CancellationTokenSource().Token;

                var formattedJson = GetPostMessage(jsonGenres.ToArray());
                var genrePostMsg = $"fields name; where id = ({formattedJson});";
                var genreResult = await IGDB.PostBasicAsync(genrePostMsg, token, GenreUrl);

                var genreIds = genreResult
                    .Children<JObject>()["id"]
                    .Values<string>()
                    .ToList();

                var genreValues = genreResult
                    .Children<JObject>()["name"]
                    .Values<string>()
                    .ToList();

                for (var i = 0; i < genreResult.Count(); i++)
                    genreDetails.Add(genreIds[i], genreValues[i]);
            }

            return genreDetails;
        }

        private async Task<List<string>> GetCompaniesAsync(JToken game)
        {
            var jsonCompanies = game.Children<JObject>()["involved_companies"];

            var companiesValues = new List<string>();

            if (jsonCompanies != null)
            {
                var formattedString = GetPostMessage(jsonCompanies.ToArray());
                var token = new CancellationTokenSource().Token;

                var involvedCompaniesPostMessage = $"fields company; where id = ({formattedString});";

                var involvedCompaniesResult = await IGDB.PostBasicAsync(involvedCompaniesPostMessage, token,
                    CompaniesDetailsUrl);

                var companiesIds = involvedCompaniesResult
                    .Children<JObject>()["company"]
                    .Values<string>()
                    .ToList();

                var companyJson = ListToString(companiesIds);
                var companiesPostMsg = $"fields name; where id = ({companyJson});";
                var companiesResult = await IGDB.PostBasicAsync(companiesPostMsg, token, CompaniesBaseUrl);

                companiesValues = companiesResult
                    .Children<JObject>()["name"]
                    .Values<string>()
                    .ToList();
            }

            return companiesValues;
        }

        private List<int> GetPlatforms(JToken game)
        {
            var platformsValues = new List<int>();

            var jsonPlatforms = game
                .Children<JObject>()["platforms"]
                .Values();

            if (jsonPlatforms.Any())
                platformsValues.AddRange(jsonPlatforms
                    .Select(platform => platform
                        .Value<int>()));

            return platformsValues;
        }

        private async Task<string> GetCoverUrl(JToken game)
        {
            var cover = PlaceholderImageUrl;
            int? coverId = null;
            if (game.Type == JTokenType.Array)
            {
              coverId = (int?) game.Children<JObject>()["cover"].FirstOrDefault();
                if (coverId != null)
                {
                   cover = await GetCoverUrlFromApi(coverId);
                }
            }
            else
            {
                coverId = game.Count() == 1 ? coverId = null : game.Value<int?>("cover");
                if (coverId != null)
                {
                   cover = await GetCoverUrlFromApi(coverId);
                }
            }

            return cover;
        }

        private async Task<string> GetCoverUrlFromApi(int? coverId)
        {
            var token = new CancellationTokenSource().Token;

            var coverPostMsg = $"fields url; where id = ({coverId});";
            var coverResult = await IGDB.PostBasicAsync(coverPostMsg, token, CoversUrl);

           return coverResult
                .Children<JObject>()["url"]
                .Single()
                .Value<string>()
                .Replace("thumb", "cover_big");
        }

        private int GetReleaseDate(JToken game)
        {
            var unixTimestamp = new double();
            if (game.Type == JTokenType.Array)
            {
                var jsonTimestamp = game.Children<JObject>()["first_release_date"].FirstOrDefault();

                if (jsonTimestamp != null)
                    unixTimestamp = jsonTimestamp.Value<double>();
            }
            else
            {
                unixTimestamp = game
                        .SelectToken("first_release_date")
                        .Value<double>();
            }
            
            

            return ConvertFromUnixTimestamp(unixTimestamp);
        }

        private string ListToString(IEnumerable<string> strings) //Formats the values to search for 
        {
            var formattedString = string.Join(", ", strings);

            return formattedString;
        }

        private int ConvertFromUnixTimestamp(double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp).Year;
        }
    }
}

//Website
//var jsonWebsite = game.Children<JObject>()["websites"];
//if (jsonWebsite.Any())
//{
//    formattedJson = GetPostMessage(jsonWebsite.First().ToArray());
//    var websitePostMsg = $"fields url; where id = ({formattedJson});";
//    var websiteResult =
//        await iGdb.PostBasicAsync(websitePostMsg, token, "https://api-v3.igdb.com/websites");
//    websiteValues = websiteResult.Children<JObject>()["url"].Values<string>().ToArray();
//}