using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GameReview.Models;

namespace GameReview.Controllers
{
    public class GameApiLinkController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Games
        public async Task<ActionResult> Index()
        {
            return View(await db.Games.ToListAsync());
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

        // POST: Games/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,GameIdentifier,Name")] GameApiLink gameApiLink)
        {
            if (ModelState.IsValid)
            {
                db.Games.Add(gameApiLink);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(gameApiLink);
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
        public async Task<ActionResult> Edit([Bind(Include = "Id,GameIdentifier")] GameApiLink gameApiLink)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gameApiLink).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gameApiLink);
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
