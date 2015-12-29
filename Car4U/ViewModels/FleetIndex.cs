using Car4U.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Car4U.ViewModels
{
    public class FleetIndex
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Car> Cars { get; set; }
    }



}