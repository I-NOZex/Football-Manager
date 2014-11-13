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
    [Authorize]
    public class JourneysController : Controller
    {
        private FMDBEntities db = new FMDBEntities();

        // GET: Journeys
        public ActionResult Index()
        {
            var journey = db.Journey.Include(j => j.Championship);
            return View(journey.ToList());
        }

        // GET: Journeys/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Journey journey = db.Journey.Find(id);
            if (journey == null)
            {
                return HttpNotFound();
            }
            return View(journey);
        }

        // GET: Journeys/Create
        public ActionResult Create()
        {
            ViewBag.ChampshipID = new SelectList(db.Championship, "ID", "Name");
            return View();
        }

        // POST: Journeys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ChampshipID,DateBegin,DateEnd")] Journey journey)
        {
            if (ModelState.IsValid)
            {
                db.Journey.Add(journey);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ChampshipID = new SelectList(db.Championship, "ID", "Name", journey.ChampshipID);
            return View(journey);
        }

        // GET: Journeys/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Journey journey = db.Journey.Find(id);
            if (journey == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChampshipID = new SelectList(db.Championship, "ID", "Name", journey.ChampshipID);
            return View(journey);
        }

        // POST: Journeys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ChampshipID,DateBegin,DateEnd")] Journey journey)
        {
            if (ModelState.IsValid)
            {
                db.Entry(journey).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ChampshipID = new SelectList(db.Championship, "ID", "Name", journey.ChampshipID);
            return View(journey);
        }

        // GET: Journeys/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Journey journey = db.Journey.Find(id);
            if (journey == null)
            {
                return HttpNotFound();
            }
            return View(journey);
        }

        // POST: Journeys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Journey journey = db.Journey.Find(id);
            db.Journey.Remove(journey);
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
