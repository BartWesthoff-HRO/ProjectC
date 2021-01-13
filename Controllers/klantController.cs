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
    public class klantController : Controller
    {
        private ApplicationDBContext db = new ApplicationDBContext();

        // GET: klant
        public ActionResult Index()
        {
            if (Session["username"] != null)
            {
                return View(db.klants.ToList());
            }
            else { return Redirect("/Login"); }
        }


        //GET: klant/Create
        public ActionResult Create()
        {
            if (Session["username"] != null)
            {
                return View();
            }
            else { return Redirect("/Login"); }

        }


        [HttpPost]
        public ActionResult Verwijderen(int id)
        {
            klant klant = db.klants.Where(x => x.bedrijfsid == id).FirstOrDefault();
            db.klants.Remove(klant);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: klant/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "bedrijfsid,bedrijfsnaam,bedrijfsemail,bedrijfsnummer")] klant klant)
        {
            if (Session["username"] != null)
            {if (Session["username"] != null)
                {
                    if (ModelState.IsValid)
                    {
                        var temp = klant;
                        temp.bedrijfsid = 1;
                        db.klants.Add(temp);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    return View(klant);
                }
                else
                {
                    return Redirect("/Login");
                }
            }
            else { return Redirect("/Login"); }

        }

        // GET: klant/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["username"] != null)
            {if (Session["username"] != null)
                {if (Session["username"] != null)
                    {
                        if (id == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                        }
                        klant klant = db.klants.Find(id);
                        if (klant == null)
                        {
                            return HttpNotFound();
                        }
                        return View(klant);
                    }
                    else
                    {
                        return Redirect("/Login");
                    }
                }
                else
                {
                    return Redirect("/Login");
                }
            }
            else { return Redirect("/Login"); }
        }

        // POST: klant/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "bedrijfsid,bedrijfsnaam,bedrijfsemail,bedrijfsnummer")] klant klant)
        {
            if (Session["username"] != null)
            {if (Session["username"] != null)
                {if (Session["username"] != null)
                    {
                        if (ModelState.IsValid)
                        {
                            db.Entry(klant).State = EntityState.Modified;
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        return View(klant);
                    }
                    else
                    {
                        return Redirect("/Login");
                    }
                }
                else
                {
                    return Redirect("/Login");
                }
            }
            else { return Redirect("/Login"); }
        }

        // GET: klant/Delete/5
        public ActionResult Delete(int? id)
        {if (Session["username"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                klant klant = db.klants.Find(id);
                if (klant == null)
                {
                    return HttpNotFound();
                }
                return View(klant);
            }
            else
            {
                return Redirect("/Login");
            }
        }

        // POST: klant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            klant klant = db.klants.Find(id);
            db.klants.Remove(klant);
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
