using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using GameReview.Models;
using GameReview.Modules;
using GameReview.ViewModels;

namespace GameReview.Controllers
{
    public class GameController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GameController()
        {
            _context = new ApplicationDbContext();
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return Content("No game ID has been provided.");

            var igdbService = new IGDBService();
            var game = await igdbService.GetGameDetailsAsync((int) id);

            return View("GameDetails", new GameDetailsViewModel(game));
        }

        [ActionName("Search")]
        public ActionResult SearchAsync(string searchFilter, string searchType)
        {


            return View("SearchResult");
        }

        [HttpGet]
        [ActionName("Results")]
        public async Task<ActionResult> SearchGameItemAsync(string searchFilter, string searchType, int pageNumber)
        {
            var gameService = new DbService();

            //get every id in db table
            //format to query api
            var formattedString = string.Join(", ", gameService.GetGamesInDatabase());

            var igdbService = new IGDBService();
            //returns list of games which match criteria in params
            var gameList = await igdbService.GetSearchResultsAsync(formattedString, searchFilter, searchType);

            var test = new List<GameItemViewModel>();
            foreach (var game in gameList)
            {
                test.Add(new GameItemViewModel(game, _context));
            }
            

            return PartialView("GameItem", test);
        }
    }
}