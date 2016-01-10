using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Car4U.Models
{
    public class ExtraModelType
    {
        public int ID { get; set; }

        [Required]
        [Display (Name = "Nome")]
        public string Description { get; set; }


        public virtual ICollection<ExtraModel> IdExtraModel { get; set; }
    }
}