using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Car4U.Models
{
    public class MeetingPoint
    {
        public int ID { get; set; }
        //Mudar o nome de Place.
        public string Place { get; set; }


        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}