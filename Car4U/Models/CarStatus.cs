using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Car4U.Models
{
    public class CarStatus
    {
        public int ID { get; set; }
        public string Observation { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BeginDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FinishDate { get; set; }


        public int CarID { get; set; }
        public int StatusID { get; set; }


        public virtual Status Status { get; set; }
        public virtual Car Car { get; set; }
    }
}