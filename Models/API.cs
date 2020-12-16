using MailChimp.Net;
using MailChimp.Net.Core;
using ProjectC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProjectC.Models
{
    public class API
    {
        //api key
        private static string key = "c21b2291eb1b26c7ea4b1a86908dcc78-us2";
        //api manager
        private MailChimpManager McM = new MailChimpManager(key);

        public async Task<MailChimp.Net.Models.Campaign> draftAsync(int limit=10)
        {
            var options = new CampaignRequest
            {
                Status = CampaignStatus.Save,
                SortOrder = CampaignSortOrder.DESC,
                Limit = limit
            };

            return (await McM.Campaigns.GetAllAsync(options)).FirstOrDefault();
        }

        public async Task<MailChimp.Net.Models.Campaign> sentAsync(string id = "b8ddf89ff8", int limit = 10)
        {
            var options = new CampaignRequest
            {
                ListId = id,
                Status = CampaignStatus.Save,
                SortOrder = CampaignSortOrder.DESC,
                Limit = limit
            };

            return (await McM.Campaigns.GetAllAsync(options)).FirstOrDefault();
        }

    }
}