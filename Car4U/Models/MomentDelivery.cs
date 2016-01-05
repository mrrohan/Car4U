using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Car4U.Models
{
    public class MomentDelivery
    {
        public int ID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public string Observation { get; set; }


        public int ReservationID { get; set; }


        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}