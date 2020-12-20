using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectC.Models
{

    [Table("Persoon", Schema = "dbo")]
    public class Persoon
    {
        [Key]
        public int persoonid { get;}

        public string voornaam { get; set; }
        public string achternaam { get; set; }
        public string tussenvoegsel { get; set; }
        public string email { get; set; }
        
    }
}