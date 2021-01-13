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
    public class IndexViewModel
    {
        public List<ContactPerson> people { get; set; }
        public List<label> labels { get; set; }
        public List<Kenmerk> kenmerken { get; set; }
        public List<klant> klanten { get; set; }
    }
}