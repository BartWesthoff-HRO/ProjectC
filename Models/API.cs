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

        public async Task<IEnumerable<MailChimp.Net.Models.Campaign>> draftAsync(int limit)
        {
            var options = new CampaignRequest
            {
                Status = CampaignStatus.Save,
                SortOrder = CampaignSortOrder.DESC,
                Limit = limit
            };

            return await McM.Campaigns.GetAllAsync(options);
        }

        public async void sendAsync(string id)
        {
            await McM.Campaigns.SendAsync(id);
        }

        public async Task<IEnumerable<MailChimp.Net.Models.Campaign>> send(string id, int limit)
        {
            var options = new CampaignRequest
            {
                ListId = id,
                Status = CampaignStatus.Save,
                SortOrder = CampaignSortOrder.DESC,
                Limit = limit
            };

            return await McM.Campaigns.GetAllAsync(options);
        }

    }
}