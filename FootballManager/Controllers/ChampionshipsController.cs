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
    public class ChampionshipsController : Controller
    {
        private FMDBEntities db = new FMDBEntities();

        // GET: /Championship/
        public ActionResult Index()
        {
            var championship = db.Championship.Include(c => c.EntityManager).Include(c => c.Country);
            return View(championship.ToList());
        }

        // GET: /Championship/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Championship championship = db.Championship.Find(id);
            if (championship == null)
            {
                return HttpNotFound();
            }
            return View(championship);
        }

        // GET: /Championship/Create
        public ActionResult Create()
        {
            ViewBag.EntityMngID = new SelectList(db.EntityManager, "ID", "Name");
            ViewBag.CountryID = new SelectList(db.Country, "ID", "Name");
            return View();
        }

        // POST: /Championship/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Name,Logo,FoudationDate,CountryID,EntityMngID")] Championship championship)
        {
            if (ModelState.IsValid)
            {
                db.Championship.Add(championship);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EntityMngID = new SelectList(db.EntityManager, "ID", "Name", championship.EntityMngID);
            ViewBag.CountryID = new SelectList(db.Country, "ID", "Name", championship.CountryID);
            return View(championship);
        }

        // GET: /Championship/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Championship championship = db.Championship.Find(id);
            if (championship == null)
            {
                return HttpNotFound();
            }
            ViewBag.EntityMngID = new SelectList(db.EntityManager, "ID", "Name", championship.EntityMngID);
            ViewBag.CountryID = new SelectList(db.Country, "ID", "Name", championship.CountryID);
            return View(championship);
        }

        // POST: /Championship/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Name,Logo,FoudationDate,CountryID,EntityMngID")] Championship championship)
        {
            if (ModelState.IsValid)
            {
                db.Entry(championship).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EntityMngID = new SelectList(db.EntityManager, "ID", "Name", championship.EntityMngID);
            ViewBag.CountryID = new SelectList(db.Country, "ID", "Name", championship.CountryID);
            return View(championship);
        }

        // GET: /Championship/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Championship championship = db.Championship.Find(id);
            if (championship == null)
            {
                return HttpNotFound();
            }
            return View(championship);
        }

        // POST: /Championship/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Championship championship = db.Championship.Find(id);
            db.Championship.Remove(championship);
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
