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
    public class MatchesController : Controller
    {
        private FMDBEntities db = new FMDBEntities();

        // GET: /Matches/
        public ActionResult Index()
        {
            var match = db.Match.Include(m => m.Team).Include(m => m.Team1);
            return View(match.ToList());
        }

        // GET: /Matches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Match match = db.Match.Find(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            return View(match);
        }

        // GET: /Matches/Create
        public ActionResult Create()
        {
            ViewBag.GuestTeamID = new SelectList(db.Team, "ID", "Name");
            ViewBag.VisitorTeamID = new SelectList(db.Team, "ID", "Name");
            return View();
        }

        // POST: /Matches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,JourneyID,Date,KickoffTime,VisitorTeamID,GuestTeamID")] Match match)
        {
            if (ModelState.IsValid)
            {
                db.Match.Add(match);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GuestTeamID = new SelectList(db.Team, "ID", "Name", match.GuestTeamID);
            ViewBag.VisitorTeamID = new SelectList(db.Team, "ID", "Name", match.VisitorTeamID);
            return View(match);
        }

        // GET: /Matches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Match match = db.Match.Find(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            ViewBag.GuestTeamID = new SelectList(db.Team, "ID", "Name", match.GuestTeamID);
            ViewBag.VisitorTeamID = new SelectList(db.Team, "ID", "Name", match.VisitorTeamID);
            return View(match);
        }

        // POST: /Matches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,JourneyID,Date,KickoffTime,VisitorTeamID,GuestTeamID")] Match match)
        {
            if (ModelState.IsValid)
            {
                db.Entry(match).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GuestTeamID = new SelectList(db.Team, "ID", "Name", match.GuestTeamID);
            ViewBag.VisitorTeamID = new SelectList(db.Team, "ID", "Name", match.VisitorTeamID);
            return View(match);
        }

        // GET: /Matches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Match match = db.Match.Find(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            return View(match);
        }

        // POST: /Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Match match = db.Match.Find(id);
            db.Match.Remove(match);
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
