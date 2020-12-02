using ProjectC.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectC.Models;

namespace ProjectC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Autherize(Medewerker employee)
        {
            using (ApplicationDBContext db = new ApplicationDBContext())
            {
                Medewerker userDetails = db.Medewerkers.Where(x => x.gebruikersnaam == employee.gebruikersnaam && x.wachtwoord == employee.wachtwoord).FirstOrDefault();
                if (userDetails == null)
                {
                    
                    return View("Index", employee);
                }
                else
                {
                    Session["userid"] = userDetails.medewerkerid;
                    Session["username"] = userDetails.gebruikersnaam;
                    return RedirectToAction("Index", "Home");
                }
            }
     
        }
    }
}