using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Car4U.Models
{
    public class Car
    {
        public int ID { get; set; }
        public string LicensePlate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string RegisterDate { get; set; }
        public int NDoors { get; set; }
        public int NLuggage { get; set; }
        public string Engine { get; set; }
        public string HorsePower { get; set; }


        public string GearID { get; set; }
        public int CategoryID { get; set; }
        public int FuelTypeID { get; set; }
        public int CarModelID { get; set; }
        //public string UserID { get; set; }


        public virtual ICollection<CarStatus> CarStatus { get; set; }
        public virtual FuelType fuelType { get; set; }
        public virtual CarModel carModel { set; get; }
        public virtual Category category { set; get; }
        public virtual ICollection<ApplicationUser> users { get; set; }
        public virtual ICollection<FilePath> FilePaths { get; set; }
        public virtual Gear gear { get; set; }
    }
}