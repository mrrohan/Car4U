using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity.Infrastructure;

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
        public string Email { get; set; }
        public string License { get; set; }
        public string BI { get; set; }
        public double FinalPrice { get; set; }
        public bool Check { get; set; }//false unchecked by employee, no car associated to reservation.


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        //reserva
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReservationDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReturnDate { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime ReturnHour { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DeliveryDate { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DeliveryHour { get; set; }

        
        

        //Dropboxs
        public int CountryID { get; set; }
        public int CategoryID { get; set; }
        public int MPDeliveryID { get; set; }
        public int MPReturnID { get; set; }
        public int ExtraItemsID { get; set; }      
        public int carID { get; set; } //nao está ligado ao modelo car, é so para inserir o ID do carro.



        //Virtual stuff
        public virtual Country Country { get; set; }
        public virtual MeetingPoint MPDelivery { get; set; }
        public virtual MeetingPoint MPReturn { get; set; }
        public virtual ICollection<ExtraItem> ExtraItems { get; set; }
        public virtual MomentDelivery MomentDelivery { get; set; }
        public virtual MomentReturn MomentReturn { get; set; }
        public virtual Category Category { get; set; }
        public virtual Promotion Promotion { get; set; }
    }
}