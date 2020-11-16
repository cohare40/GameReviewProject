using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using GameReview.Models;
using GameReview.Modules;


namespace GameReview.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GameApiLinkController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Games
        public async Task<ActionResult> Index()
        {
            return View(await db.Games.ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult> SearchGamesToAddAsync(string searchFilter, int pageNumber)
        {
            var igdbService = new IGDBService();
            var games = await igdbService.GetGameToAddSearchResultsAsync(searchFilter, pageNumber);

            return PartialView("ListOfGamesToAdd", games);
        }


        public async Task<ActionResult> AddGameToDb(int id)
        {
            var igdbService = new IGDBService();
                var game = await igdbService.GetGameDetailsAsync(id);
                var gameApiLink = new GameApiLink
                {
                    Name = game.Name, GameIdentifier = game.Id, GenreIds = game.GenreIds
                };


                db.Games.Add(gameApiLink);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
        }

        // GET: Games/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameApiLink gameApiLink = await db.Games.FindAsync(id);
            if (gameApiLink == null)
            {
                return HttpNotFound();
            }
            return View(gameApiLink);
        }

        // GET: Games/Create
        public ActionResult Create()
        {
            return View();
        }


        // GET: Games/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameApiLink gameApiLink = await db.Games.FindAsync(id);
            if (gameApiLink == null)
            {
                return HttpNotFound();
            }
            return View(gameApiLink);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditGameDetails([Bind(Include = "Id,GameIdentifier,Name")] GameApiLink gameApiLink)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gameApiLink).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("Edit", gameApiLink);
        }

        // GET: Games/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameApiLink gameApiLink = await db.Games.FindAsync(id);
            if (gameApiLink == null)
            {
                return HttpNotFound();
            }
            return View(gameApiLink);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            GameApiLink gameApiLink = await db.Games.FindAsync(id);
            db.Games.Remove(gameApiLink);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
