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
    public class TeamPointsController : Controller
    {
        private FMDBEntities db = new FMDBEntities();

        // GET: TeamPoints
        public ActionResult Index()
        {
            var teamPoints = db.TeamPoints.Include(t => t.Championship).Include(t => t.Team);
            return View(teamPoints.ToList());
        }

        // GET: TeamPoints/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamPoints teamPoints = db.TeamPoints.Find(id);
            if (teamPoints == null)
            {
                return HttpNotFound();
            }
            return View(teamPoints);
        }

        // GET: TeamPoints/Create
        public ActionResult Create()
        {
            ViewBag.ChampshipID = new SelectList(db.Championship, "ID", "Name");
            ViewBag.TeamID = new SelectList(db.Team, "ID", "Name");
            return View();
        }

        // POST: TeamPoints/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeamID,ChampshipID,Points")] TeamPoints teamPoints)
        {
            if (ModelState.IsValid)
            {
                db.TeamPoints.Add(teamPoints);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ChampshipID = new SelectList(db.Championship, "ID", "Name", teamPoints.ChampshipID);
            ViewBag.TeamID = new SelectList(db.Team, "ID", "Name", teamPoints.TeamID);
            return View(teamPoints);
        }

        // GET: TeamPoints/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamPoints teamPoints = db.TeamPoints.Find(id);
            if (teamPoints == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChampshipID = new SelectList(db.Championship, "ID", "Name", teamPoints.ChampshipID);
            ViewBag.TeamID = new SelectList(db.Team, "ID", "Name", teamPoints.TeamID);
            return View(teamPoints);
        }

        // POST: TeamPoints/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeamID,ChampshipID,Points")] TeamPoints teamPoints)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teamPoints).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ChampshipID = new SelectList(db.Championship, "ID", "Name", teamPoints.ChampshipID);
            ViewBag.TeamID = new SelectList(db.Team, "ID", "Name", teamPoints.TeamID);
            return View(teamPoints);
        }

        // GET: TeamPoints/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamPoints teamPoints = db.TeamPoints.Find(id);
            if (teamPoints == null)
            {
                return HttpNotFound();
            }
            return View(teamPoints);
        }

        // POST: TeamPoints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TeamPoints teamPoints = db.TeamPoints.Find(id);
            db.TeamPoints.Remove(teamPoints);
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
