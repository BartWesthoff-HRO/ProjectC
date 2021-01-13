using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProjectC.Models
{
    public class CCampaign
    {
      
        public string CampaignName { get; set; }
        public string Id { get; set; }
        public int ClickRate { get; set; }
        public double OpenRate { get; set; }
        public int UnsubRate { get; set; }
        public List<CCampaign> Searches { get; set; }
        public string ListName { get; set; }
        public string URL { get; set; }
        public int? EmailsSent { get; set; }
        public string DateCreated { get; set; }
        public string ListId { get; set; }
    }
}
