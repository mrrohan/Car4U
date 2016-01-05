using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Car4U.Models
{
    public class ExtraItem
    {
        public int ID { get; set; }
        public bool InUse  { get; set; }
        
        public int ExtraModelID { get; set; }


        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ExtraModel ExtraModel { get; set; }
    }
}