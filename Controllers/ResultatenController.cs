using ProjectC.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectC.Controllers
{
    public class ResultatenController : Controller
    {
        private ApplicationDBContext db = new ApplicationDBContext();
        // GET: Resultaten
        public ActionResult Index()
        {
            return View(db.surveys.ToList());
        }
    }
}