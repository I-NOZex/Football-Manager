using FootballManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballManager.Controllers {
    public class HomeController : Controller {

        private FMDBEntities db = new FMDBEntities();
        public ActionResult Index() {
            ViewBag.LastChamp = db.Championship.ToList().LastOrDefault();
            ViewBag.Matches = db.Match.ToList();
            return View();
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}