using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Car4U.Models
{
    public class MomentReturn
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Observation { get; set; }


        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}