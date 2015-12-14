using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Car4U.Models
{
    public class Reservation
    {
        //Atributos
        public int ID { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public double FinalPrice { get; set; }

        //Dropboxs
        public int CategoryID { get; set; }
        public int MPDeliveryID { get; set; }
        public int MPReturnID { get; set; }
        public int ExtraItemsID { get; set; }
        public int MomentDeliveryID { get; set; }
        public int MomentReturnID { get; set; }



        //Virtual stuff
        public virtual MeetingPoint MPDelivery { get; set; }
        public virtual MeetingPoint MPReturn { get; set; }
        public virtual ICollection<ExtraItem> ExtraItems { get; set; }
        public virtual MomentDelivery MomentDelivery { get; set; }
        public virtual MomentReturn MomentReturn { get; set; }
        public virtual Category Category { get; set; }
        public virtual ApplicationUser user { get; set; }
        public virtual ICollection<Promotion_Reservation> Promotion_Reservations { get; set; }
    }
}