using Car4U.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Car4U.ViewModels
{
    public class CarStatusIndex
    {
        public IEnumerable<Car> Cars1 { get; set; }
        public IEnumerable<Car> Cars2 { get; set; }
    }
}