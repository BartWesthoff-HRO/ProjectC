using MailChimp.Net;
using MailChimp.Net.Core;
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

        private static MailChimpManager Manager = new MailChimpManager("e8453349ccde9693bf86d9760787356f-us7");

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

            //await Manager.Campaigns.SendAsync(id);
            return RedirectToAction("index");
        }

        public async Task<ActionResult> Sent()
        {
            var options = new CampaignRequest
            {
                ListId = "14d2abf97d",
                Status = CampaignStatus.Sent,
                SortOrder = CampaignSortOrder.DESC,
                Limit = 10
            };

            ViewBag.ListId = "14d2abf97d";
            ViewBag.ListName = "Project C";


            var model = await Manager.Campaigns.GetAllAsync(options);
            return View(model);

        }
    }
}