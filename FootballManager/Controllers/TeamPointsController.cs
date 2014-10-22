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

        // GET: /TeamPoints/
        public ActionResult Index()
        {
            var teampoints = db.TeamPoints.Include(t => t.Championship).Include(t => t.Team);
            return View(teampoints.ToList());
        }

        // GET: /TeamPoints/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamPoints teampoints = db.TeamPoints.Find(id);
            if (teampoints == null)
            {
                return HttpNotFound();
            }
            return View(teampoints);
        }

        // GET: /TeamPoints/Create
        public ActionResult Create()
        {
            ViewBag.ChampshipID = new SelectList(db.Championship, "ID", "Name");
            ViewBag.TeamID = new SelectList(db.Team, "ID", "Name");
            return View();
        }

        // POST: /TeamPoints/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="TeamID,ChampshipID,Points")] TeamPoints teampoints)
        {
            if (ModelState.IsValid)
            {
                db.TeamPoints.Add(teampoints);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ChampshipID = new SelectList(db.Championship, "ID", "Name", teampoints.ChampshipID);
            ViewBag.TeamID = new SelectList(db.Team, "ID", "Name", teampoints.TeamID);
            return View(teampoints);
        }

        // GET: /TeamPoints/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamPoints teampoints = db.TeamPoints.Find(id);
            if (teampoints == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChampshipID = new SelectList(db.Championship, "ID", "Name", teampoints.ChampshipID);
            ViewBag.TeamID = new SelectList(db.Team, "ID", "Name", teampoints.TeamID);
            return View(teampoints);
        }

        // POST: /TeamPoints/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="TeamID,ChampshipID,Points")] TeamPoints teampoints)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teampoints).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ChampshipID = new SelectList(db.Championship, "ID", "Name", teampoints.ChampshipID);
            ViewBag.TeamID = new SelectList(db.Team, "ID", "Name", teampoints.TeamID);
            return View(teampoints);
        }

        // GET: /TeamPoints/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeamPoints teampoints = db.TeamPoints.Find(id);
            if (teampoints == null)
            {
                return HttpNotFound();
            }
            return View(teampoints);
        }

        // POST: /TeamPoints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TeamPoints teampoints = db.TeamPoints.Find(id);
            db.TeamPoints.Remove(teampoints);
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
