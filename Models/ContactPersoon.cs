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
    [Table("Contactpersoon", Schema = "dbo")]
    public class ContactPersoon(Persoon)
    {
        [ForeignKey]
        public int persoonid { get;}
        public string bedrijfsnaam {get; set;}
    }
}