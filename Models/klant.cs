using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectC.Models
{

    [Table("klant", Schema = "public")]
    public class klant
    {
        [Key]
        public int persoonid { get; set; }

        public string voornaam { get; set; }
        public string achternaam { get; set; }
        public string tussenvoegsel { get; set; }
        public string email { get; set; }
    }
}