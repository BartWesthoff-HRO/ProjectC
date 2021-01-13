using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using System.ComponentModel;
using System.Data.Entity;
using ProjectC.Models;

namespace ProjectC.Models
{
    public class ContactpersoonViewModel
    {
        public List<label> labels { get; set; }
        public List<klant> klanten { get; set; }
        public List<label> kenmerken { get; set; }
        public ContactPerson persoon { get; set; }

    }
}