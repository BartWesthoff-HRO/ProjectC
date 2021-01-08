using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectC.Models
{

    [Table("klant", Schema = "dbo")]
    public class klant
    {
        [Key]
        public int bedrijfsid { get; set; }
        public string bedrijfsnaam { get; set; }
        public string bedrijfsemail { get; set; }
        public string bedrijfsnummer { get; set; }
        
    }
}