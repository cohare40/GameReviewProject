using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using GameReview.Models;
using GameReview.Modules;
using GameReview.ViewModels;

namespace GameReview.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        public async Task<ActionResult> Index()
        {
            var gameService = new DbService();
            var igdbService = new IGDBService();

            //Get list of ids of top rated games in db
            var topRatedGameIds = await gameService.GetTopGamesAsync();
            var topRatedGames = await igdbService.GetGamesIdAndPicAsync(topRatedGameIds);

            foreach (var game in topRatedGames)
            {
                game.AverageRating = gameService.GetAverageRating(game.Id);
            }


            var allGameIdsInDb = gameService.GetGamesInDatabase().ToArray();

            Random rnd = new Random();
            HashSet<int> numbers = new HashSet<int>();
            var discoverGamesIds = new List<int>();
            while (numbers.Count < 8)
                numbers.Add(rnd.Next(1, allGameIdsInDb.Length));

            foreach (var num in numbers)
                discoverGamesIds.Add(allGameIdsInDb[num]);
            
            var discoverGameItems = await igdbService.GetGamesIdAndPicAsync(discoverGamesIds);

            var viewModel = new HomeIndexViewModel
            {
                TopRatedGames = topRatedGames.OrderByDescending(g => g.AverageRating)
                    .Take(6),
                DiscoverGames = discoverGameItems
            };

            return View("Index", viewModel);
        }
    }
}