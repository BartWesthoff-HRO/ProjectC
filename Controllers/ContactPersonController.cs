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
        private List<label> cplabels = new List<label>();
        public ContactpersoonViewModel vm = new ContactpersoonViewModel();

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

        public async Task<ActionResult> Terug()
        {
            return RedirectToAction("Index");
        }



        // GET: ContactPerson/Create
        public ActionResult Create()
        {
            HttpContext.Session["Labels"] = new List<label>();
            vm.labels = db.labels.ToList();
            vm.klanten = db.klants.ToList();
            this.vm.kenmerken = new List<label>();
            return View(vm);
        }

        // POST: ContactPerson/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ContactpersoonViewModel contact, int[] labels)
        {
            if (!ModelState.IsValid && contact.persoon.achternaam != null && contact.persoon.voornaam != null && contact.persoon.email != null)
            {
                db.ContactPersons.Add(contact.persoon);
                db.SaveChanges();
                var searchid = contact.persoon;
                var person = db.ContactPersons.Where(x => x.achternaam == searchid.achternaam && x.voornaam == searchid.voornaam && x.email == searchid.email && x.bedrijfsid != searchid.bedrijfsid);

                var temp = new Kenmerk();
                temp.contactpersoonid = searchid.contactpersoonid;
                foreach(var kenmerk in labels)
                {
                    temp.labelid = kenmerk;
                    db.kenmerk.Add(temp);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");

            }
            else
                return RedirectToAction("Create");
        }

        // GET: ContactPerson/Edit/5

        [HttpPost]
        public ActionResult Verwijderen(int? pid)
        {
            pid = ViewBag.CID;
            db.ContactPersons.Remove(db.ContactPersons.Where(x => x.contactpersoonid == pid).FirstOrDefault());
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
        public async Task<ActionResult> Edit([Bind(Include = "contactpersoonid,voornaam,achternaam,tussenvoegsel,email")] ContactPerson contactPerson)
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
