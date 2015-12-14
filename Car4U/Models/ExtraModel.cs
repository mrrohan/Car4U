using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Car4U.Models
{
    public class ExtraModel
    {
        public int ID { get; set; }
        public string Model { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }


        public int ExtraModelTypeID { get; set; }


        public virtual ExtraModelType ExtraModelType { get; set; }
        public virtual ICollection<ExtraItem> ExtraItems { get; set; }
    }
}