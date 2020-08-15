using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using GameReview.Models;
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

        // GET: Game
        public ActionResult Index()
        {
            return View();
        }

        [Route("game/details/{id?}")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return Content("No game id has been provided.");

            var igdbService = new IGDBService();
            var game = await igdbService.GetGameDetailsAsync((int) id);

            var reviewList = _context.Reviews
                .Where(r => r.gameId == game.Id)
                .Include(r => r.User)
                .ToList();


            var userList = reviewList.Select(review => review.UserId)
                .Select(userAccountId => _context.Users.Distinct()
                    .Single(u => u.Id == userAccountId))
                .ToList();

            var viewModel = new GameDetailsViewModel
            {
                Game = game,
                Review = reviewList,
                User = userList
            };

            return View("GameDetails");
        }

        //Returns all games that are in ApiLink db table + match the genre ID from api
        /**[ActionName("Search")]
        public async Task<ActionResult> GenreSearchResultAsync(int? id, string name)
        {
            var gameInDbIdList = await _context.Games
                .Select(gameInDb => gameInDb.GameIdentifier)
                .ToListAsync();

            //get every id in db table
            //format to query api
            var formattedString = GetPostMessage(gameInDbIdList);

            var igdbService = new IGDBService();
            //returns list of games which match criteria in params
            var gameList = await igdbService.GetSearchResultsAsync(formattedString, id);


            return View("GenreSearchResult", gameList);
        }**/

        [ActionName("Search")]
        public async Task<ActionResult> SearchAsync(string searchFilter, string searchType)
        {
            var gameInDbIdList = await _context.Games
                .Select(gameInDb => gameInDb.GameIdentifier)
                .ToListAsync();

            //get every id in db table
            //format to query api
            var formattedString = GetPostMessage(gameInDbIdList);

            var igdbService = new IGDBService();
            //returns list of games which match criteria in params
            var gameList = await igdbService.GetSearchResultsAsync(formattedString, searchFilter, searchType);


            return View("SearchResult", gameList);
        }

        public string GetPostMessage(List<int> gameIdList) //Formats the values to search for 
        {
            var formattedString = string.Join(", ", gameIdList);

            return formattedString;
        }
    }
}