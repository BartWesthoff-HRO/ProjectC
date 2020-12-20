using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectC.Models
{
        [Table("Label", Schema = "dbo")]
        public class Label
        {
            [Key]
            public int labelid { get;}
            public string labelname { get; set; }
        }
    }