using MailChimp.Net;
using MailChimp.Net.Core;
using ProjectC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace ProjectC.Controllers
{
    public class CampaingsController : Controller
    {
        API mails = new API();

        public async Task<ActionResult> Drafts(int limit = 10)
        {
            return View(await mails.draftAsync(limit));

        }

        public async Task<ActionResult> Send(string id)
        {
            mails.sendAsync(id);
            return RedirectToAction("index");
        }

        public async Task<ActionResult> Sent(string id = "b8ddf89ff8", int limit = 10)
        {
            return View(await mails.send(id, limit));

        }
    }
}