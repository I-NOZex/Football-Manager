using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FootballManager.Models;

namespace FootballManager.Controllers
{
    public class MatchGoalsController : Controller
    {
        private FMDBEntities db = new FMDBEntities();

        // GET: MatchGoals
        public ActionResult Index()
        {
            var matchGoals = db.MatchGoals.Include(m => m.Match).Include(m => m.Player).Include(m => m.Team);
            return View(matchGoals.ToList());
        }

        // GET: MatchGoals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MatchGoals matchGoals = db.MatchGoals.Find(id);
            if (matchGoals == null)
            {
                return HttpNotFound();
            }
            return View(matchGoals);
        }

        // GET: MatchGoals/Create
        public ActionResult Create()
        {
            ViewBag.MatchID = new SelectList(db.Match, "ID", "ID");
            ViewBag.PlayerID = new SelectList(db.Player, "ID", "Name");
            ViewBag.TeamID = new SelectList(db.Team, "ID", "Name");
            return View();
        }

        // POST: MatchGoals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,MatchID,TeamID,PlayerID,Time")] MatchGoals matchGoals)
        {
            if (ModelState.IsValid)
            {
                db.MatchGoals.Add(matchGoals);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MatchID = new SelectList(db.Match, "ID", "ID", matchGoals.MatchID);
            ViewBag.PlayerID = new SelectList(db.Player, "ID", "Name", matchGoals.PlayerID);
            ViewBag.TeamID = new SelectList(db.Team, "ID", "Name", matchGoals.TeamID);
            return View(matchGoals);
        }

        // GET: MatchGoals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MatchGoals matchGoals = db.MatchGoals.Find(id);
            if (matchGoals == null)
            {
                return HttpNotFound();
            }
            ViewBag.MatchID = new SelectList(db.Match, "ID", "ID", matchGoals.MatchID);
            ViewBag.PlayerID = new SelectList(db.Player, "ID", "Name", matchGoals.PlayerID);
            ViewBag.TeamID = new SelectList(db.Team, "ID", "Name", matchGoals.TeamID);
            return View(matchGoals);
        }

        // POST: MatchGoals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,MatchID,TeamID,PlayerID,Time")] MatchGoals matchGoals)
        {
            if (ModelState.IsValid)
            {
                db.Entry(matchGoals).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MatchID = new SelectList(db.Match, "ID", "ID", matchGoals.MatchID);
            ViewBag.PlayerID = new SelectList(db.Player, "ID", "Name", matchGoals.PlayerID);
            ViewBag.TeamID = new SelectList(db.Team, "ID", "Name", matchGoals.TeamID);
            return View(matchGoals);
        }

        // GET: MatchGoals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MatchGoals matchGoals = db.MatchGoals.Find(id);
            if (matchGoals == null)
            {
                return HttpNotFound();
            }
            return View(matchGoals);
        }

        // POST: MatchGoals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MatchGoals matchGoals = db.MatchGoals.Find(id);
            db.MatchGoals.Remove(matchGoals);
            db.SaveChanges();
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
