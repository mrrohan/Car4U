using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Car4U.Models
{
    public class Car
    {
        public string ID { get; set; }
        public string LicensePlate { get; set; }
        public string RegisterDate { get; set; }
        public int NDoors { get; set; }
        public int NLuggage { get; set; }
        public string Engine { get; set; }
        public string HorsePower { get; set; }


        public int FuelTypeID { get; set; }
        public int CarModelID { get; set; }
        public int CarStatusID { get; set; }


        public virtual ICollection<CarStatus> CarStatus { get; set; }
        public virtual FuelType fuelType { get; set; }
        public virtual CarModel carModel { set; get; }
    }
}