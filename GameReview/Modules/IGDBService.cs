using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GameReview.Models;
using Newtonsoft.Json.Linq;

namespace GameReview.Modules
{
    public class IGDBService
    {
        private const string GamesUrl = "https://api-v3.igdb.com/games";
        private const string AgeUrl = "https://api-v3.igdb.com/age_ratings";
        private const string CompaniesBaseUrl = "https://api-v3.igdb.com/companies";
        private const string CompaniesDetailsUrl = "https://api-v3.igdb.com/involved_companies";
        private const string GenreUrl = "https://api-v3.igdb.com/genres";
        private const string CoversUrl = "https://api-v3.igdb.com/covers";

        private readonly ApplicationDbContext _context;

        public IGDBService()
        {
            _context = new ApplicationDbContext();
        }

        public async Task<Game> GetGameDetailsAsync(int id)
        {
            var formattedJson = "";
            var ageRatingValues = new List<string>();
            var genreValues = new List<string>();
            var genreIds = new List<string>();
            var companiesValues = new List<string>();
            var platformsValues = new List<int>();
            var coverValues = "";
            string[] websiteValues = { };
            var source = new CancellationTokenSource();
            var token = source.Token;
            var gameFields =
                $"fields age_ratings, cover, first_release_date, genres, involved_companies, name, platforms, rating, summary, websites, id; where id = {id};";

            var iGdb = new IGDB(); //create igdb object to post game info request
            var game = await iGdb.PostBasicAsync(gameFields, token, GamesUrl);


            //IDs that need more processing
            //Age Rating
            var jsonAgeRating = game.Children<JObject>()["age_ratings"];
            if (jsonAgeRating.Any())
            {
                formattedJson = GetPostMessage(jsonAgeRating.First().ToArray());

                var ageRatingPostMsg = $"fields rating; where id = ({formattedJson});";
                var ageRatingResult =
                    await iGdb.PostBasicAsync(ageRatingPostMsg, token, AgeUrl);
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

            //Genres
            var jsonGenres = game.Children<JObject>()["genres"].FirstOrDefault();
            if (jsonGenres != null)
            {
                formattedJson = GetPostMessage(jsonGenres.ToArray());
                var genrePostMsg = $"fields name; where id = ({formattedJson});";
                var genreResult = await iGdb.PostBasicAsync(genrePostMsg, token, GenreUrl);
                genreValues = genreResult.Children<JObject>()["name"].Values<string>().ToList();
                genreIds = genreResult.Children<JObject>()["id"].Values<string>().ToList();
            }

            //Companies
            var jsonCompanies = game.Children<JObject>()["involved_companies"];
            if (jsonCompanies != null)
            {
                formattedJson = GetPostMessage(jsonCompanies.ToArray());
                var involvedCompaniesPostMessage = $"fields company; where id = ({formattedJson});";
                var involvedCompaniesResult = await iGdb.PostBasicAsync(involvedCompaniesPostMessage, token,
                    CompaniesDetailsUrl);
                var companiesIds = involvedCompaniesResult.Children<JObject>()["company"].Values<string>().ToList();
                var companyJson = ListToString(companiesIds);
                var companiesPostMsg = $"fields name; where id = ({companyJson});";
                var companiesResult =
                    await iGdb.PostBasicAsync(companiesPostMsg, token, CompaniesBaseUrl);
                companiesValues = companiesResult.Children<JObject>()["name"].Values<string>().ToList();
            }

            //Platforms
            var jsonPlatforms = game.Children<JObject>()["platforms"].Values();
            if (jsonPlatforms.Any()) platformsValues.AddRange(jsonPlatforms.Select(platform => platform.Value<int>()));

            //Cover
            var jsonCover = game.Children<JObject>()["cover"];
            if (jsonCover != null)
            {
                var coverId = jsonCover.First().Value<int>();
                var coverPostMsg = $"fields url; where id = ({coverId});";
                var coverResult = await iGdb.PostBasicAsync(coverPostMsg, token, CoversUrl);
                coverValues = coverResult.Children<JObject>()["url"].First().Value<string>();
            }
            else
            {
                coverValues = "https://sisterhoodofstyle.com/wp-content/uploads/2018/02/no-image-1.jpg";
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

            //Date released
            var jsonTimestamp = game.Children<JObject>()["first_release_date"];
            var unixTimestamp = 2.0;
            if (jsonTimestamp.Any())
                unixTimestamp = game.Children<JObject>()["first_release_date"].First().Value<double>();

            //Finalised variables for game
            var gameName = game.Children<JObject>()["name"].First().ToString();
            var gameReleaseDate = ConvertFromUnixTimestamp(unixTimestamp);
            var gameSummary = game.Children<JObject>()["summary"].FirstOrDefault()?.ToString();
            var gameAgeRating = ageRatingValues;
            var gameGenres = genreValues;
            var gameCover = coverValues.Replace("thumb", "cover_big");
            var gameWebsite = websiteValues.FirstOrDefault();
            var gameId = game.Children<JObject>()["id"].First().Value<int>();


            var gameObj = new Game
            {
                Id = gameId,
                Name = gameName,
                FirstReleaseDate = gameReleaseDate.ToString(),
                Summary = gameSummary,
                AgeRatingImage = gameAgeRating,
                GenreName = gameGenres,
                GenreIds = ListToString(genreIds),
                InvolvedCompanies = companiesValues,
                Platforms = platformsValues,
                CoverArtUrl = gameCover,
                Websites = gameWebsite
            };

            return gameObj;
        }

        public async Task<IEnumerable<Genre>> GetAllGenresAsync()
        {
            var source = new CancellationTokenSource();
            var token = source.Token;
            var postContent = "fields *;"; //*

            var iGdb = new IGDB();
            var genreJArray = await iGdb.PostBasicAsync(postContent, token, GenreUrl);

            //var genreList = genreJArray.Children<JObject>()["name"].Values<string>().ToList(); //Only returns string name of genres as a list


            return (from genreResult in genreJArray
                let genreId = (int) genreResult.SelectToken("id")
                let genreName = genreResult.SelectToken("name").ToString()
                select new Genre {Id = genreId, Name = genreName}).ToList();
        }

        public async Task<IEnumerable<Game>> GetSearchResultsAsync(string idList, string searchFilter,
            string searchType) //only returns 2 details about game & ID for search results 
        {
            var source = new CancellationTokenSource();
            var token = source.Token;
            var genreSearchString =
                $"fields genres, first_release_date, platforms, cover, name, id; where id = ({idList}) & genres = ({searchFilter}); limit 500;";
            var nameSearchString =
                $"fields genres, first_release_date, platforms, cover, name, id; where id = ({idList}) & name = *\"{searchFilter}\"*;";

            var iGdb = new IGDB(); //create igdb object to post game info request
            var game = new JArray();

            //Case for either name or genre search type
            switch (searchType)
            {
                case "genre":
                    game = await iGdb.PostBasicAsync(genreSearchString, token, GamesUrl);
                    break;
                case "name":
                    game = await iGdb.PostBasicAsync(nameSearchString, token, GamesUrl);
                    break;
            }

            var gameList = new List<Game>();
            foreach (var gameJToken in game) //each game has its details assigned to object + added to game list
            {
                var genreValues = new List<string>();
                var platformsValues = new List<int>();
                var gameIdValue = 0;

                var jsonCover = gameJToken.SelectToken("cover");
                var coverPostMsg = $"fields url; where id = ({jsonCover});";
                var coverResult = await iGdb.PostBasicAsync(coverPostMsg, token, CoversUrl);
                var coverValues = coverResult.Children<JObject>()["url"].Values<string>().ToArray();

                var gameId = gameJToken.SelectToken("id");
                if (gameId != null)
                    gameIdValue = gameJToken.SelectToken("id").Value<int>();

                var jsonPlatforms = gameJToken.SelectToken("platforms").Values();

                platformsValues.AddRange(jsonPlatforms
                    .Select(platform => platform
                        .Value<int>()));

                var firstReleaseDate = gameJToken.Children<JObject>()["first_release_date"].FirstOrDefault();
                var frdTest = "";

                if (firstReleaseDate != null) frdTest = firstReleaseDate.Value<string>();

                var jsonGenres = gameJToken.SelectToken("genres");
                if (jsonGenres != null)
                {
                    var formattedJson = GetPostMessage(jsonGenres.ToArray());
                    var genrePostMsg = $"fields name; where id = ({formattedJson});";
                    var genreResult = await iGdb.PostBasicAsync(genrePostMsg, token, GenreUrl);
                    genreValues = genreResult.Children<JObject>()["name"].Values<string>().ToList();
                }

                var basicGameDetails = new Game
                {
                    Id = gameIdValue,
                    Name = gameJToken["name"].ToString(),
                    CoverArtUrl = coverValues.First().Replace("thumb", "cover_big"),
                    Platforms = platformsValues,
                    FirstReleaseDate = frdTest,
                    GenreName = genreValues
                };
                gameList.Add(basicGameDetails);
            }

            //returns list of games to be iterated over in search results 
            return gameList;
        }

        //Returns all games matching search to be added to db
        public async Task<IEnumerable<Game>> GetGameToAddSearchResultsAsync(string searchFilter, int pageNumber)
        {
            var source = new CancellationTokenSource();
            var token = source.Token;
            var nameSearchString =
                $"fields cover, name, id; where name = *\"{searchFilter}\"*; limit 10; offset {pageNumber * 10};";

            var iGdb = new IGDB(); //create igdb object to post game info request
            var game = await iGdb.PostBasicAsync(nameSearchString, token, "https://api-v3.igdb.com/games");

            var gameList = new List<Game>();
            foreach (var gameJToken in game) //each game has its details assigned to object + added to game list
            {
                var coverValues = new List<string>();
                var jsonCover = gameJToken.SelectToken("cover");
                if (jsonCover != null)
                {
                    jsonCover = gameJToken.SelectToken("cover");
                    var coverPostMsg = $"fields url; where id = ({jsonCover});";
                    var coverResult = await iGdb.PostBasicAsync(coverPostMsg, token, CoversUrl);
                    coverValues = coverResult.Children<JObject>()["url"].Values<string>().ToList();
                }
                else
                {
                    coverValues.Add("https://sisterhoodofstyle.com/wp-content/uploads/2018/02/no-image-1.jpg");
                }


                var gameId = game.Children<JObject>()["id"].First().Value<int>();

                var basicGameDetails = new Game
                {
                    Id = gameId,
                    Name = gameJToken["name"].ToString(),
                    CoverArtUrl = coverValues.First().Replace("thumb", "cover_big")
                };
                gameList.Add(basicGameDetails);
            }

            //returns list of games to be iterated over in search results 
            return gameList;
        }

        //Get "x" number of games, used for getting top rated games
        public async Task<IEnumerable<Game>> GetTopRatedGamesAsync(IEnumerable<int> idList)
        {
            var source = new CancellationTokenSource();
            var token = source.Token;
            var formattedString = string.Join(", ", idList);

            var topRatedSearchString =
                $"fields cover, id; where id = ({formattedString});";

            var iGdb = new IGDB(); //create igdb object to post game info request
            var game = await iGdb.PostBasicAsync(topRatedSearchString, token, "https://api-v3.igdb.com/games");

            var gameList = new List<Game>();
            foreach (var gameJToken in game) //each game has its details assigned to object + added to game list
            {
                var coverValues = new List<string>();
                var jsonCover = gameJToken.SelectToken("cover");
                if (jsonCover != null)
                {
                    jsonCover = gameJToken.SelectToken("cover");
                    var coverPostMsg = $"fields url; where id = ({jsonCover});";
                    var coverResult = await iGdb.PostBasicAsync(coverPostMsg, token, CoversUrl);
                    coverValues = coverResult.Children<JObject>()["url"].Values<string>().ToList();
                }
                else
                {
                    coverValues.Add("https://sisterhoodofstyle.com/wp-content/uploads/2018/02/no-image-1.jpg");
                }


                var gameId = gameJToken.SelectToken("id").Value<int>();

                var basicGameDetails = new Game
                {
                    Id = gameId,
                    CoverArtUrl = coverValues.First().Replace("thumb", "cover_big")
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

        public static string ListToString(List<string> strings) //Formats the values to search for 
        {
            var formattedString = string.Join(", ", strings);

            return formattedString;
        }

        private static int ConvertFromUnixTimestamp(double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp).Year;
        }
    }
}