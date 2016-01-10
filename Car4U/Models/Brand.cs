using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Car4U.Models
{
    public class Brand
    {
        public int ID { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual ICollection<CarModel> carModel { get; set; }
    }
}