using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using System.ComponentModel;

namespace ProjectC.Models
{
    [Table("persoon", Schema = "public")]
    public class ContactPerson
    {
        [Key]
        public int persoonid { get; set; }
        
        public string voornaam { get; set; }
        public string achternaam { get; set; }
        public string tussenvoegsel { get; set; }
        public string email { get; set; }

    }
}