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
using MailChimp.Net.Core;

namespace ProjectC.Controllers
{
    public class ContactPersonController : Controller
    {
        private static MailChimpManager Manager = new MailChimpManager("c21b2291eb1b26c7ea4b1a86908dcc78-us2");
        private ApplicationDBContext db = new ApplicationDBContext();

        // GET: ContactPerson
        public async Task<ActionResult> Index(string id = null)
        {
            ViewBag.Lists = await Manager.Lists.GetAllAsync();
            List<string> allEmailList = new List<string>();
            if (id == null)
            {
                List<string> allId = new List<string>();
                foreach (List item in await Manager.Lists.GetAllAsync())
                {
                    allId.Add(item.Id);
                }

                foreach (string listid in allId)
                {
                    foreach (Member member in await Manager.Members.GetAllAsync(listid,new MemberRequest { Status = Status.Subscribed }))
                    {
                        allEmailList.Add(member.EmailAddress);
                    }
                }

            }
            else
            {
                allEmailList.Clear();
                IEnumerable<Member> x = await Manager.Members.GetAllAsync(id, new MemberRequest { Status = Status.Subscribed });
                foreach (Member member in x.ToList())
                {
                    allEmailList.Add(member.EmailAddress);
                }
            }
            var matchedMembers = db.ContactPersons.Where(persoon => allEmailList.Contains(persoon.email));
            return View(matchedMembers);
         

        }

        public async Task<ActionResult> mailview()
        {
            try
            {
                ViewBag.ListId = "3a0f93041f";
                var model = await Manager.Members.GetAllAsync("b8ddf89ff8");
                return View(model);
            }
            catch (MailChimpException mce)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadGateway, mce.Message);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable, ex.Message);
            }
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
                db.ContactPersons.Add(contactPerson);
                db.SaveChanges();

                var member = new Member
                {
                    EmailAddress = contactPerson.email,
                    Status = Status.Subscribed,
                    EmailType = "html",
                    MergeFields = new Dictionary<string, object>
                {
                    { "FNAME", "ditispending" },
                    { "LNAME", "pendinglocal" }
                }
                };


                await Manager.Members.AddOrUpdateAsync("b8ddf89ff8", member);
                return RedirectToAction("Index");

            }
            else
                return RedirectToAction("Index");




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

                //Tags tags = new Tags();
                //tags.MemberTags.Add(new Tag() { Name = "Customer", Status = "active" });
                //await Manager.Members.AddTagsAsync("b8ddf89ff8", contactPerson.email, tags);
                await Manager.Members.AddOrUpdateAsync("b8ddf89ff8", member);
                return RedirectToAction("Index");
            }
            return View(contactPerson);
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
