using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MailChimp.Net.Models;
using ProjectC.DataContext;
using ProjectC.Models;
using System.Threading.Tasks;
using MailChimp.Net;

namespace ProjectC.Controllers
{
    public class ContactPersonController : Controller
    {
        private static MailChimpManager Manager = new MailChimpManager("e8453349ccde9693bf86d9760787356f-us7");
        private ApplicationDBContext db = new ApplicationDBContext();

        // GET: ContactPerson
        public ActionResult Index()
        {
            return View(db.ContactPersons.ToList());
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
        public async Task<ActionResult> Create([Bind(Include = "persoonid,voornaam,achternaam,tussenvoegsel,email")] ContactPerson contactPerson)
        {
            if (ModelState.IsValid)
            {
                //db.ContactPersons.Add(contactPerson);
                //db.SaveChanges();
                Dictionary<string, object> dictionaries = new Dictionary<string, object>
                {
                    { "FNAME", "Foo" },
                    { "LNAME", "Bar" }
                };
                var member = new Member
                {
                    EmailAddress = contactPerson.email,
                    Status = Status.Pending,
                    EmailType = "html",
                    TimestampSignup = DateTime.UtcNow.ToString(),
                    MergeFields = dictionaries,
                };


                var result = await Manager.Members.AddOrUpdateAsync("14d2abf97d", member);
                return View("Index");

            }
            else
                return View("Index");


           
            
        }

        // GET: ContactPerson/Edit/5

        [HttpPost]
        public ActionResult Verwijderen(int id)
        {
            ContactPerson contactperson = db.ContactPersons.Where(x => x.persoonid == id).FirstOrDefault();
            db.ContactPersons.Remove(contactperson);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
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
        public async Task<ActionResult> Edit([Bind(Include = "persoonid,voornaam,achternaam,tussenvoegsel,email")] ContactPerson contactPerson)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contactPerson).State = EntityState.Modified;
                db.SaveChanges();
                Dictionary<string, object> dictionaries = new Dictionary<string, object>
                {
                    { "FNAME", "Foo" },
                    { "LNAME", "Bar" }
                };
                var member = new Member
                {
                    EmailAddress = contactPerson.email,
                    Status = Status.Subscribed,
                    EmailType = "html",
                    //TimestampSignup = "12/10/20 4:28PM",
                    
                    MergeFields = dictionaries,
                };

                Tags tags = new Tags();
                tags.MemberTags.Add(new Tag() { Name = "Customer", Status = "inactive" });
                await Manager.Members.AddTagsAsync("14d2abf97d", contactPerson.email, tags);
                var result = await Manager.Members.AddOrUpdateAsync("14d2abf97d", member);
                return RedirectToAction("Index");
            }
            return View(contactPerson);
        }

        // GET: ContactPerson/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ContactPerson contactPerson = db.ContactPersons.Find(id);
        //    if (contactPerson == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(contactPerson);
        //}

        // POST: ContactPerson/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    ContactPerson contactPerson = db.ContactPersons.Find(id);
        //    db.ContactPersons.Remove(contactPerson);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
