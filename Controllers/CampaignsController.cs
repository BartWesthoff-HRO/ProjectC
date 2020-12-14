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

        private static MailChimpManager Manager = new MailChimpManager("c21b2291eb1b26c7ea4b1a86908dcc78-us2");

        public async Task<ActionResult> Drafts()
        {
            var options = new CampaignRequest
            {
                //ListId = "14d2abf97d",
                Status = CampaignStatus.Save,
                SortOrder = CampaignSortOrder.DESC,
                
                Limit = 10
            };
            ViewBag.ListId = "14d2abf97d";



            var model = await Manager.Campaigns.GetAllAsync(options);
            return View(model);

        }

        public async Task<ActionResult> Send(string id)
        {

            await Manager.Campaigns.SendAsync(id);
            return RedirectToAction("index");
        }

        public async Task<ActionResult> Sent(string id = "b8ddf89ff8")
        {
            var options = new CampaignRequest
            {
                ListId = id,
                Status = CampaignStatus.Sent,
                SortOrder = CampaignSortOrder.DESC,
                Limit = 10
            };
            ViewBag.Teachers = await Manager.Lists.GetAllAsync();

            //14d2abf97d
            var model = await Manager.Campaigns.GetAllAsync(options);
            return View(model);

        }
    }
}