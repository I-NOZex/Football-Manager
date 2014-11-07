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
    public class EntityManagersController : Controller
    {
        private FMDBEntities db = new FMDBEntities();

        // GET: EntityManagers
        public ActionResult Index()
        {
            return View(db.EntityManager.ToList());
        }

        // GET: EntityManagers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntityManager entityManager = db.EntityManager.Find(id);
            if (entityManager == null)
            {
                return HttpNotFound();
            }
            return View(entityManager);
        }

        // GET: EntityManagers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EntityManagers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,UserID")] EntityManager entityManager)
        {
            if (ModelState.IsValid)
            {
                db.EntityManager.Add(entityManager);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(entityManager);
        }

        // GET: EntityManagers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntityManager entityManager = db.EntityManager.Find(id);
            if (entityManager == null)
            {
                return HttpNotFound();
            }
            return View(entityManager);
        }

        // POST: EntityManagers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,UserID")] EntityManager entityManager)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entityManager).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(entityManager);
        }

        // GET: EntityManagers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EntityManager entityManager = db.EntityManager.Find(id);
            if (entityManager == null)
            {
                return HttpNotFound();
            }
            return View(entityManager);
        }

        // POST: EntityManagers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EntityManager entityManager = db.EntityManager.Find(id);
            db.EntityManager.Remove(entityManager);
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
