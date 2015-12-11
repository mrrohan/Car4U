using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Car4U.Models
{
    public class CarStatus
    {
        public string ID { get; set; }
        public string Observation { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime FinishDate { get; set; }
   

        public virtual Status Status { get; set; }
        public virtual Car Car { get; set; }
    }
}