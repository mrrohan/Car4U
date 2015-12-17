using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Car4U.Models
{
    public class Promotion_Reservation
    {
        public int ID { get; set; }


        public virtual Reservation Reservation { get; set; }
        public virtual Promotion Promotion { get; set; }
    }
}