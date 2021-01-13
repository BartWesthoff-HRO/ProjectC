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

        public async Task<ActionResult> Drafts(string id = null)
        {
            if (Session["username"] != null)
            {

                var options = new CampaignRequest
                {
                    ListId = id,
                    Status = CampaignStatus.Save,
                    SortOrder = CampaignSortOrder.DESC,
                    Limit = 10,
                };
                ViewBag.Lists = await Manager.Lists.GetAllAsync();

                var sortedList = await Manager.Campaigns.GetAllAsync(options);

                var newCampaignList = new List<CCampaign>();
                foreach (var Campaign in sortedList)
                {
                    var newCampaign = new CCampaign
                    {
                        CampaignName = Campaign.Settings.Title,
                        //newCampaign.ClickRate = Clicks(Campaign.Id).Result;
                        OpenRate = await Manager.Reports.GetCampaignOpenReportCountAsync(Campaign.Id),
                        Id = Campaign.Id,
                        //newCampaign.UnsubRate = await Manager.Reports.GetUnsubscribesCountAsync(id);
                        ListName = Campaign.Recipients.ListName,
                        DateCreated = Campaign.CreateTime.ToString("dd MMMM yyyy"),
                        URL = Campaign.ArchiveUrl,
                        EmailsSent = Campaign.EmailsSent,
                        ListId = Campaign.Recipients.ListId
                    };
                    newCampaignList.Add(newCampaign);
                }
                newCampaignList.OrderBy(camp => camp.DateCreated);
                Audience audience = new Audience
                { CampaignList = newCampaignList };
                ViewBag.Lists = await Manager.Lists.GetAllAsync();
                return View(audience);
            }
            else
            {
                return Redirect("/Login");
            }
        }

   
        public async Task<ActionResult> Send(string id)
        {
            await Manager.Campaigns.SendAsync(id);
            return RedirectToAction("Drafts");
        }

        public async Task<ActionResult> Sent(string id = null)
        {
            if (Session["username"] != null)
            {

                var options = new CampaignRequest
                {
                    ListId = id,
                    Status = CampaignStatus.Sent,
                    SortOrder = CampaignSortOrder.DESC,
                    Limit = 10,
                };
                ViewBag.Lists = await Manager.Lists.GetAllAsync();


                var sortedList = await Manager.Campaigns.GetAllAsync(options);

                var newCampaignList = new List<CCampaign>();
                foreach (var Campaign in sortedList)
                {
                    var newCampaign = new CCampaign
                    {
                        CampaignName = Campaign.Settings.Title,
                        OpenRate = ((await Manager.Reports.GetCampaignOpenReportCountAsync(Campaign.Id)) / (float)Campaign.EmailsSent) * 100.0,
                        Id = Campaign.Id,
                        ListName = Campaign.Recipients.ListName,
                        DateCreated = Campaign.CreateTime.ToString("dd MMMM yyyy"),
                        URL = Campaign.ArchiveUrl,
                        EmailsSent = Campaign.EmailsSent
                    };
                    newCampaignList.Add(newCampaign);
                }
                newCampaignList.OrderBy(camp => camp.DateCreated);
                Audience audience = new Audience
                { CampaignList = newCampaignList };
                ViewBag.Lists = await Manager.Lists.GetAllAsync();
                return View(audience);
            }
            else
            {
                return Redirect("/Login");
            }
        }


    }
}