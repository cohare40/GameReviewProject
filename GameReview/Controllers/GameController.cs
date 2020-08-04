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

        [Route("game/details/{id}")]
        public async Task<ActionResult> Details(int id)
        {
            var igdbService = new IGDBService();
            var game = await igdbService.GetGameDetailsAsync(id);


            return View(game);
        }

        

    }
}

