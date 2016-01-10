using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Car4U.Models
{
    public class FuelType
    {
        public int ID { get; set; }

        [Required]
        [Display (Name = "Nome do Combustivel")]
        public string Description { get; set; }

        public virtual ICollection<Car> car { get; set; }
    }
}