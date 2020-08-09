using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GameReview.Models;
using GameReview.ViewModels;

namespace GameReview.Controllers
{
    public class GenreController : Controller
    {
        // GET: Genre
        public async Task<ActionResult> Index()
        {

            var igdbService = new IGDBService();

            var genres = await igdbService.GetAllGenresAsync();

            var viewModel = new GenreIndexViewModel
            {
                Genres = genres
            };


            return View(viewModel);
        }
    }
}