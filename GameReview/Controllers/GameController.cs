using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using GameReview.Models;
using GameReview.Models.Extensions;
using GameReview.ViewModels;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebGrease.Css.Extensions;


namespace GameReview.Controllers
{
    public class GameController : Controller
    {
        private ApplicationDbContext _context;

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
            var game = await igdbService.GetGameDetailsAsync((int)id);

            var reviewList = _context.Reviews.Where(r => r.gameId == game.Id).Include(r => r.User).ToList();
            

            List<ApplicationUser> userList = new List<ApplicationUser>();

            foreach (var review in reviewList)
            {
                var userAccountId = review.UserId;
                ApplicationUser user = _context.Users.Distinct().Single(u => u.Id == userAccountId);
                userList.Add(user);
            }

            


            var viewModel = new GameDetailsViewModel
            {
                Game = game,
                Review1 = {},
                Review = reviewList,
                User = userList
            };

            return View(viewModel);
        }

        

    }
}

