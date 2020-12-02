using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjectC.DataContext;
using ProjectC.Models;

namespace ProjectC.Controllers
{
    public class medewerkerController : Controller
    {
        private ApplicationDBContext db = new ApplicationDBContext();

        // GET: medewerker
        public ActionResult Index()
        {
            return View(db.Medewerkers.ToList());
        }

        // GET: medewerker/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medewerker medewerker = db.Medewerkers.Find(id);
            if (medewerker == null)
            {
                return HttpNotFound();
            }
            return View(medewerker);
        }

        // GET: medewerker/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: medewerker/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "medewerkerid,gebruikersnaam,wachtwoord")] Medewerker medewerker)
        {
            if (ModelState.IsValid)
            {
                db.Medewerkers.Add(medewerker);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(medewerker);
        }

        // GET: medewerker/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medewerker medewerker = db.Medewerkers.Find(id);
            if (medewerker == null)
            {
                return HttpNotFound();
            }
            return View(medewerker);
        }

        // POST: medewerker/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "medewerkerid,gebruikersnaam,wachtwoord")] Medewerker medewerker)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medewerker).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(medewerker);
        }

        // GET: medewerker/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medewerker medewerker = db.Medewerkers.Find(id);
            if (medewerker == null)
            {
                return HttpNotFound();
            }
            return View(medewerker);
        }

        // POST: medewerker/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Medewerker medewerker = db.Medewerkers.Find(id);
            db.Medewerkers.Remove(medewerker);
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
