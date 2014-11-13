using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FootballManager.Models;
using System.IO;
using System.Data.Entity.Validation;

namespace FootballManager.Controllers
{
    [Authorize]
    public class ChampionshipsController : Controller
    {
        private FMDBEntities db = new FMDBEntities();

        // GET: Championships
        public ActionResult Index()
        {
            var championship = db.Championship.Include(c => c.EntityManager).Include(c => c.Country);
            return View(championship.ToList());
        }

        // GET: Championships/Details/5
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
            if (championship.Logo != null) {
                ViewBag.Logo =  Utils.EncodeFile(Server.MapPath(championship.Logo.ToString()));
            }
            return View(championship);
        }

        // GET: Championships/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.EntityMngID = new SelectList(db.EntityManager, "ID", "Name");
            ViewBag.CountryID = new SelectList(db.Country, "ID", "Name");
            return View();
        }

        // POST: Championships/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "ID,Name,LogoPath,FoudationDate,CountryID,EntityMngID")] Championship championship)
        {           
            if (championship.LogoPath != null && championship.LogoPath.ContentLength > 0) {
                string filename = DateTime.Now.Ticks + Path.GetExtension(championship.LogoPath.FileName);
                
                var imagePath = Path.Combine(Server.MapPath(Utils.UPLOAD), filename);
                var imageUrl = Path.Combine(Utils.UPLOAD, filename);
                championship.LogoPath.SaveAs(imagePath);

                championship.Logo = String.Concat(Utils.UPLOAD, "/", filename);

            }

            if (ModelState.IsValid) {
                try { 
                db.Championship.Add(championship);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = championship.ID });


                } catch (DbEntityValidationException dbEx) {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {                            
                            System.Diagnostics.Trace.TraceInformation("\n\rProperty: {0}\n\r Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                            return RedirectToAction("Create");
                        }
                    }
                }
            }

            ViewBag.EntityMngID = new SelectList(db.EntityManager, "ID", "Name", championship.EntityMngID);
            ViewBag.CountryID = new SelectList(db.Country, "ID", "Name", championship.CountryID);
            return View(championship);
        }

        // GET: Championships/Edit/5
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
            if (championship.Logo != null) {
                ViewBag.Logo = Utils.EncodeFile(Server.MapPath(championship.Logo.ToString()));
            }
            ViewBag.EntityMngID = new SelectList(db.EntityManager, "ID", "Name", championship.EntityMngID);
            ViewBag.CountryID = new SelectList(db.Country, "ID", "Name", championship.CountryID);
            return View(championship);
        }

        // POST: Championships/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,LogoPath,FoudationDate,CountryID,EntityMngID")] Championship championship)
        {
            if (championship.LogoPath != null && 
            championship.LogoPath.ContentLength > 0 &&
            championship.LogoPath.FileName != Path.GetFileName(championship.Logo) ) {

                string filename = Path.GetFileName(championship.Logo);

                var imagePath = Path.Combine(Server.MapPath(Utils.UPLOAD), filename);
                var imageUrl = Path.Combine(Utils.UPLOAD, filename);
                championship.LogoPath.SaveAs(imagePath);

                championship.Logo = String.Concat(Utils.UPLOAD, "/", filename);

            }

            if (ModelState.IsValid) {
                try {
                    db.Entry(championship).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Details", new { id = championship.ID });


                } catch (DbEntityValidationException dbEx) {
                    foreach (var validationErrors in dbEx.EntityValidationErrors) {
                        foreach (var validationError in validationErrors.ValidationErrors) {
                            System.Diagnostics.Trace.TraceInformation("\n\rProperty: {0}\n\r Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                            return RedirectToAction("Edit", new { id = championship.ID });
                        }
                    }
                }
            }

            /*if (ModelState.IsValid)
            {
                db.Entry(championship).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }*/
            ViewBag.EntityMngID = new SelectList(db.EntityManager, "ID", "Name", championship.EntityMngID);
            ViewBag.CountryID = new SelectList(db.Country, "ID", "Name", championship.CountryID);
            return View(championship);
        }

        // GET: Championships/Delete/5
        [Authorize(Roles = "Admin")]
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

        // POST: Championships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
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
