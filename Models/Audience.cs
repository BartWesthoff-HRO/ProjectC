using MailChimp.Net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectC.Models
{
    public class Audience
    {
        public string Id { get; set; }
        public List<CCampaign> CampaignList {get;set;}
    }
}