using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectC.Models
{
        [Table("label", Schema = "public")]
        public class labels
        {
            [Key]
            public int labelid { get; set; }
            public string labelname { get; set; }
        }
    }