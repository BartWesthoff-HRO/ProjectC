using MailChimp.Net.Models;
using ProjectC.DataContext;
using ProjectC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ProjectC.Controllers
{
    public class SurveyController : Controller
    {

        private ApplicationDBContext db = new ApplicationDBContext();
        // GET: Survey

        public ActionResult Index([Bind(Include = "QuestionID,Answer,Email")] Survey survey)
        {

            return View(survey);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save([Bind(Include = "ID,Email,Answer1,Answer2,Answer3,Answer4")] Survey survey)
        {
            if (ModelState.IsValid)
            {

                ContactPerson contactpersoongegevens = db.ContactPersons.Where(persoon => persoon.email == survey.Email).FirstOrDefault();
                // zit niet in de contactpersonenlijst
                if (contactpersoongegevens == null)
                {
                    return RedirectToAction("Index");
                }
                // zit wel in de lijst en de gegevens komen overeen
                else if (contactpersoongegevens.email == survey.Email && !(db.surveys.Where(x => x.Email == survey.Email).FirstOrDefault().Email == survey.Email))
                {

                    survey.Answer2.ToString();
                    survey.Answer3.ToString();
                    db.surveys.Add(survey);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }// op het moment dat de persoon al iets heeft ingevuld gaan we naar index
                return RedirectToAction("Index");

            }
            // moment dat het niet is ingevuld rederict naar dezelfde pagina met de antwoorden die wel zijn ingevuld
            return View("Index");
        }








    }
}