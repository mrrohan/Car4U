using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Car4U.Models
{
    public class Gear
    {
        public int ID { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Car> cars { get; set; }

    }
}