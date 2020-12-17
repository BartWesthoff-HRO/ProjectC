using MailChimp.Net;
using MailChimp.Net.Core;
using MailChimp.Net.Models;
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

        public async Task<ActionResult> Duplicate()
        {
            await Manager.Campaigns.ReplicateCampaignAsync("b1dfc83cb9");
            return View("Drafts");
        }
        public async Task<ActionResult> Send(string id)
        {
            await Manager.Campaigns.SendAsync(id);
            return RedirectToAction("Drafts");
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
            ViewBag.Lists = await Manager.Lists.GetAllAsync();

            //ViewBag.Clicks = GetOpens(id).Result;
            //14d2abf97d
            var model = await Manager.Campaigns.GetAllAsync(options);

            return View(model);

        }

        private async Task<int> GetOpens(string id)
        {
            var opens = await Manager.Reports.GetUnsubscribesCountAsync(id);
            return opens;
        }

        public async Task<ActionResult> Create()
        {
            string listID = "b8ddf89ff8";
            int segmentID = 0;
            var segmentAwaitable = Manager.ListSegments.GetAllAsync(listID).ConfigureAwait(false);
            var segment = segmentAwaitable.GetAwaiter().GetResult();
            foreach (var seg in segment)
            {
                string segmentNameTmp = seg.Name;
                int segmentIdTmp = seg.Id;
                if (segmentNameTmp.Equals("bigB"))
                {
                    segmentID = (segmentIdTmp);
                }
            }

            var sending = new Campaign
            {
                Id = "newid",
                Settings = new Setting
                {
                    AutoFooter = false,
                    FromName = "0991807@hr.nl",
                    Title = "new title",
                },
                Recipients = new Recipient
                {
                    ListId = listID,
                    SegmentOptions = new SegmentOptions
                    {
                        SavedSegmentId = segmentID,
                        
                    }
                },
                Type = CampaignType.Regular
            };
            await Manager.Campaigns.AddAsync(sending);
            return RedirectToAction("Sent");


        }
    }
}