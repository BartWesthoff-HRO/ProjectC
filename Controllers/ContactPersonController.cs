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
    public class ContactPersonController : Controller
    {
        private ApplicationDBContext db = new ApplicationDBContext();

        // GET: ContactPerson
        public ActionResult Index()
        {
            return View(db.ContactPersons.ToList());
        }

        // GET: ContactPerson/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactPerson contactPerson = db.ContactPersons.Find(id);
            if (contactPerson == null)
            {
                return HttpNotFound();
            }
            return View(contactPerson);
        }

        // GET: ContactPerson/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactPerson/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "persoonid,voornaam,achternaam,tussenvoegsel,email")] ContactPerson contactPerson)
        {
            if (ModelState.IsValid)
            {
                db.ContactPersons.Add(contactPerson);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contactPerson);
        }

        // GET: ContactPerson/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactPerson contactPerson = db.ContactPersons.Find(id);
            if (contactPerson == null)
            {
                return HttpNotFound();
            }
            return View(contactPerson);
        }

        // POST: ContactPerson/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "persoonid,voornaam,achternaam,tussenvoegsel,email")] ContactPerson contactPerson)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contactPerson).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contactPerson);
        }

        // GET: ContactPerson/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactPerson contactPerson = db.ContactPersons.Find(id);
            if (contactPerson == null)
            {
                return HttpNotFound();
            }
            return View(contactPerson);
        }

        // POST: ContactPerson/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContactPerson contactPerson = db.ContactPersons.Find(id);
            db.ContactPersons.Remove(contactPerson);
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
