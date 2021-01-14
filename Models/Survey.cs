using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjectC.Models
{



    [Table("Table", Schema = "dbo")]
    public class Survey
    {

        [Key]
        public int ID { get; set; }
        public string Email { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }

        public string Answer3 { get; set; }
        public string Answer4 { get; set; }


    }
    public enum Option
    {
        Labels,
        Emails,
        Contantactpersonen,
        Klanten
    }
    public enum Option1
    {
        Labels,
        Emails,
        Contantactpersonen,
        Klanten
    }


}