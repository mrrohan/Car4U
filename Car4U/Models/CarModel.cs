﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Car4U.Models
{
    public class CarModel
    {
        public int ID { get; set; }

        [Required]        
        public string Description { get; set; }

       
        public int BrandID { get; set; }


        public virtual Brand brand { get; set; }
    }
}