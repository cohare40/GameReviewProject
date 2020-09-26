using System.Threading.Tasks;
using System.Web.Mvc;
using GameReview.Models;
using GameReview.Modules;

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
            var topRatedGameIds = gameService.GetTopGames();

            var topRatedGames = await igdbService.GetTopRatedGamesAsync(topRatedGameIds);


            return View("Index",topRatedGames);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}