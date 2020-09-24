using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using GameReview.Models;

namespace GameReview.Controllers
{
    public class GenreController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GenreController()
        {
            _context = new ApplicationDbContext();
        }

        public async Task<ActionResult> Index()
        {
            var genreInDbList = await _context.Genres.ToListAsync();

            return View(genreInDbList);
        }
    }
}