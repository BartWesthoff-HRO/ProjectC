using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using System.ComponentModel;

namespace ProjectC.Models
{

        [Table("medewerker", Schema = "dbo")]
        public class Medewerker
        {
        [Key]
        public int medewerkerid { get;}
        [DisplayName("User Name")]
        [Required(ErrorMessage = "Vul een gebruikersnaam in")]
        public string gebruikersnaam { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Vul je wachtwoord in")]
        public string wachtwoord { get; set; }
        }
}
