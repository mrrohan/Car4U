using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Car4U.Models
{
    public class Reservation
    {
        //Atributos
        public int ID { get; set; }
        //user
        public string Name { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string License { get; set; }
        public string BI { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        //reserva
        public DateTime ReservationDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public double FinalPrice { get; set; }

        //Dropboxs
        public int CountryID { get; set; }
        public int CategoryID { get; set; }
        public int MPDeliveryID { get; set; }
        public int MPReturnID { get; set; }
        public int ExtraItemsID { get; set; }
        public int MomentDeliveryID { get; set; }
        public int MomentReturnID { get; set; }



        //Virtual stuff
        public virtual Country Country { get; set; }
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