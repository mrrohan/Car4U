﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Car4U.Models
{
    public class FuelType
    {
        public string Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Car> car { get; set; }
    }
}