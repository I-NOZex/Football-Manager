﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FootballManager.Controllers
{
    public class EntityManagersController : Controller
    {
        // GET: EntityManagers
        public ActionResult Index()
        {
            return View();
        }

        // GET: EntityManagers/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EntityManagers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EntityManagers/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: EntityManagers/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EntityManagers/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: EntityManagers/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EntityManagers/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
