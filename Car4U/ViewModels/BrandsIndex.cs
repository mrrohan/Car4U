using Car4U.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Car4U.ViewModels
{
    public class BrandsIndex
    {
        public IEnumerable<Brand> Brands { get; set; }
        public IEnumerable<CarModel> CarModels { get; set; }
    }
}