using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Car4U.Models
{
    public class Promotion
    {
        public string id { get; set; }
        public string Description { get; set; }
        public int Percentage { get; set; }
        public int Days { get; set; }

        public virtual ICollection<Promotion_Reservation> Promotion_reservations { get; set; }
    }
}