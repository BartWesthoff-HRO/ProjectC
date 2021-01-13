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
    [Table("Kenmerk", Schema = "dbo")]
    public class Kenmerk
    {
        [Key]
        public int contactpersoonid { get; set; }
        public int labelid { get; set; }
    }
}