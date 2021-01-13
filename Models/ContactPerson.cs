using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using System.ComponentModel;
using System.Data.Entity;

namespace ProjectC.Models
{
    [Table("contactpersoon", Schema = "dbo")]
    public class ContactPerson
    {
        [Key]
        public int contactpersoonid { get; set; }
        public string voornaam { get; set; }
        public string achternaam { get; set; }
        public string tussenvoegsel { get; set; }
        public string email { get; set; }
        public int? bedrijfsid { get; set; }
    }
}