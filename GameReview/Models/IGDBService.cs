﻿using System;
using System.Collections;
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
                    ageRatingValues = ageRatingResult.Children<JObject>()["rating"].FirstOrDefault().Value<int>();
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
            var jsonWebsite = game.Children<JObject>()["websites"];
            if (jsonWebsite.Any())
            {
                formattedJson = GetPostMessage(jsonWebsite.First().ToArray());
                var websitePostMsg = $"fields url; where id = ({formattedJson});";
                var websiteResult =
                    await iGdb.PostBasicAsync(websitePostMsg, token, "https://api-v3.igdb.com/websites");
                websiteValues = websiteResult.Children<JObject>()["url"].Values<string>().ToArray();
            }

            //Date released
            var unixTimestamp = game.Children<JObject>()["first_release_date"].First().Value<double>();

            //Finalised variables for game
            var gameName = game.Children<JObject>()["name"].First().ToString();
            var gameReleaseDate = ConvertFromUnixTimestamp(unixTimestamp);
            var gameSummary = game.Children<JObject>()["summary"].First().ToString();
            var gameAgeRating = Enum.GetName(typeof(AgeRatingEnum), ageRatingValues);
            var gameGenres = string.Join(", ", genreValues.ToList());
            var gameCompanies = string.Join(", ", companiesValues);
            var gamePlatforms = string.Join(", ", platformsValues);
            var gameCover = coverValues.First().Replace("thumb", "cover_big");
            var gameWebsite = websiteValues.FirstOrDefault();
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

        public async Task<IEnumerable<Genre>> GetAllGenresAsync()
        {
            var source = new CancellationTokenSource();
            var token = source.Token;
            var postContent = $"fields *;";//*

            var iGdb = new IGDB();
            var genreJArray = await iGdb.PostBasicAsync(postContent, token, "https://api-v3.igdb.com/genres");

            var genreList = new List<Genre>();
            foreach (var genreResult in genreJArray)//each game has its details assigned to object + added to game list
            {
                var genreId = (int)genreResult.SelectToken("id");
                var genreName = genreResult.SelectToken("name").ToString();

                var genre = new Genre
                {
                    Id = genreId,
                    Name = genreName
                };
                genreList.Add(genre);
            }
            //var genreList = genreJArray.Children<JObject>()["name"].Values<string>().ToList(); //Only returns string name of genres as a list


            return genreList;
        }

        public async Task<IEnumerable<Game>> GetSearchResultsAsync(string idList, string searchFilter, string searchType) //only returns 2 details about game & ID for search results 
        {
            var source = new CancellationTokenSource();
            var token = source.Token;
            var genreSearchString =
                $"fields cover, name, id; where id = ({idList}) & genres = ({searchFilter});";
            var nameSearchString =
                $"fields cover, name, id; where id = ({idList}) & name = *\"{searchFilter}\"*;";

            var iGdb = new IGDB(); //create igdb object to post game info request
            var game = new JArray();

            //Case for either name or genre search type
            switch(searchType) 
            {
                case "genre":
                     game = await iGdb.PostBasicAsync(genreSearchString, token, "https://api-v3.igdb.com/games");
                    break;
                case "name":
                    game = await iGdb.PostBasicAsync(nameSearchString, token, "https://api-v3.igdb.com/games");
                    break;
            }

            var gameList = new List<Game>();
            foreach (var gameJToken in game)//each game has its details assigned to object + added to game list
            {
                var jsonCover = gameJToken.SelectToken("cover");
                var coverPostMsg = $"fields url; where id = ({jsonCover});";
                var coverResult = await iGdb.PostBasicAsync(coverPostMsg, token, "https://api-v3.igdb.com/covers");
                var coverValues = coverResult.Children<JObject>()["url"].Values<string>().ToArray();
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

        public string GetPostMessage(JToken[] json) //Formats the values to search for 
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

