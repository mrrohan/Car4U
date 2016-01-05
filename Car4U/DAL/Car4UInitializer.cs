using Car4U.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Car4U.DAL
{
    public class Car4UInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext> 
    {
        protected override void Seed(ApplicationDbContext context)
        {

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Admin"))
            {
                roleManager.Create(new IdentityRole("Admin"));
            }

            var country = new List<Country>
            {
                new Country { ID = 1, Name = "Portugal",   },
            };
            country.ForEach(s => context.Countries.Add(s));
            context.SaveChanges();

            var mp = new List<MeetingPoint>
            {
                new MeetingPoint { ID = 1, Place = "Aeroporto Francisco Sá Carneiro (Porto)" },
                new MeetingPoint { ID = 2, Place = "Aeroporto da Portela (Lisboa)" },
                new MeetingPoint { ID = 3, Place = "Estação Comboios (Aveiro)" }

            };
            mp.ForEach(s => context.MeetingPoints.Add(s));
            context.SaveChanges();

            var fuel = new List<FuelType>
            {
                new FuelType { ID = 1, Description = "Diesel" },
                new FuelType { ID = 2, Description = "Gasolina" }
            };
            fuel.ForEach(s => context.FuelTypes.Add(s));
            context.SaveChanges();

            var mtypes = new List<ExtraModelType>
            {
                new ExtraModelType {ID = 1, Description = "GPS" },
                new ExtraModelType {ID = 2, Description = "Car Seat for Baby" }
            };
            mtypes.ForEach(s => context.ExtraModelTypes.Add(s));
            context.SaveChanges();

            var mod = new List<ExtraModel>
            {
                new ExtraModel {ID = 1, ExtraModelTypeID = 1,Model="GPS OP", Price = 50, Stock=5 },
                new ExtraModel {ID = 2, ExtraModelTypeID = 2,Model="Safe Seat", Price = 20, Stock=3 }
            };
            mod.ForEach(s => context.ExtraModels.Add(s));
            context.SaveChanges();

            var item = new List<ExtraItem>
            {
                new ExtraItem {ID = 1, ExtraModelID = 1 },
                new ExtraItem {ID = 1, ExtraModelID = 2 }
            };
            item.ForEach(s => context.ExtraItems.Add(s));
            context.SaveChanges();

            var prom = new List<Promotion>
            {
                new Promotion {ID = 1, Description = "Promotion 10%", Percentage=10, Days=10 },
                new Promotion {ID = 1, Description = "Promotion 15%", Percentage=15, Days=15 },
                new Promotion {ID = 1, Description = "Promotion 20%", Percentage=20, Days=20  }
            };
            prom.ForEach(s => context.Promotions.Add(s));
            context.SaveChanges();

            var user = new ApplicationUser { UserName = "SuperAdmin@Super.com", Name = "MasterAdmin", Email = "SuperAdmin@Super.com", CountryID = 1 };

            if (userManager.FindByName("SuperAdmin@Super.com") == null)
            {
                var result = userManager.Create(user, "password");

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
            }

            var category = new List<Category>
            {
                new Category { ID = 1, CategoryName = "Category A - Económico", Price = 10, Warranty = 0,  },
                new Category { ID = 2, CategoryName = "Category B - Familiar", Price = 20, Warranty = 0,  },
            };
            category.ForEach(s => context.Categories.Add(s));
            context.SaveChanges();
        }
    }
}