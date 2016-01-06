namespace Car4U.Migrations
{
    using Car4U.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Car4U.DAL.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "Car4U.DAL.ApplicationDbContext";
        }

        protected override void Seed(Car4U.DAL.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (!roleManager.RoleExists("Admin"))
            {
                roleManager.Create(new IdentityRole("Admin"));
            }

            var cars = new List<Brand>
            {
                new Brand { ID = 1, Description = "Alfa Romeo"},
                new Brand { ID = 2, Description = "Audi"},
                new Brand { ID = 3, Description = "BMW"  },
                new Brand { ID = 4, Description = "Citroen"},
                new Brand { ID = 5, Description = "Fiat"},
                new Brand { ID = 6, Description = "Ford"},
                new Brand { ID = 7, Description = "Kia"},
                new Brand { ID = 8, Description = "Mercedes-Benz"},
                new Brand { ID = 9, Description = "Nissan"},
                new Brand { ID = 10, Description = "Opel"},
                new Brand { ID = 11, Description = "Peugeot"},
                new Brand { ID = 12, Description = "Renault"},
                new Brand { ID = 13, Description = "Seat"},
                new Brand { ID = 14, Description = "Skoda"},
                new Brand { ID = 15, Description = "Suzuki"},
                new Brand { ID = 16, Description = "Toyota"},
                new Brand { ID = 17, Description = "Volkswagen"},
                new Brand { ID = 18, Description = "Volvo"},
            };
            cars.ForEach(s => context.Brands.AddOrUpdate(p => p.Description, s));
            context.SaveChanges();

            var carmodel = new List<CarModel>
            {
                new CarModel { BrandID = 1, Description = "Giulietta"},
                new CarModel { BrandID = 2, Description = "A1"},
                new CarModel { BrandID = 2, Description = "A3"},
                new CarModel { BrandID = 2, Description = "A4"},

                new CarModel { BrandID = 3, Description = "Série 1"},
                new CarModel { BrandID = 3, Description = "Série 3"},
                new CarModel { BrandID = 3, Description = "Série 5"},

                new CarModel { BrandID = 4, Description = "C1"},
                new CarModel { BrandID = 4, Description = "C3"},
                new CarModel { BrandID = 4, Description = "C4"},

                new CarModel { BrandID = 5, Description = "Punto"},
                new CarModel { BrandID = 5, Description = "500"},

                new CarModel { BrandID = 6, Description = "Fiesta"},
                new CarModel { BrandID = 6, Description = "Focus"},
                new CarModel { BrandID = 6, Description = "Focus SW"},
                new CarModel { BrandID = 6, Description = "S-Max"},

                new CarModel { BrandID = 7, Description = "Ceed"},

                new CarModel { BrandID = 8, Description = "Classe A"},
                new CarModel { BrandID = 8, Description = "Classe B"},
                new CarModel { BrandID = 8, Description = "Classe C"},
                new CarModel { BrandID = 8, Description = "Vito"},

                new CarModel { BrandID = 9, Description = "Note"},
                new CarModel { BrandID = 9, Description = "Juke"},
                new CarModel { BrandID = 9, Description = "Pulsar"},

                new CarModel { BrandID = 10, Description = "Corsa"},
                new CarModel { BrandID = 10, Description = "Astra"},
                new CarModel { BrandID = 10, Description = "Astra SW"},

                new CarModel { BrandID = 11, Description = "108"},
                new CarModel { BrandID = 11, Description = "208"},
                new CarModel { BrandID = 11, Description = "308"},
                new CarModel { BrandID = 11, Description = "308 SW"},
                new CarModel { BrandID = 11, Description = "508"},

                new CarModel { BrandID = 12, Description = "Twingo"},
                new CarModel { BrandID = 12, Description = "Clio"},
                new CarModel { BrandID = 12, Description = "Megane"},
                new CarModel { BrandID = 12, Description = "Megane SW"},
                new CarModel { BrandID = 12, Description = "Scenic"},

                new CarModel { BrandID = 13, Description = "Ibiza"},
                new CarModel { BrandID = 13, Description = "Leon"},
                new CarModel { BrandID = 13, Description = "Leon SW"},

                new CarModel { BrandID = 14, Description = "Fabia"},
                new CarModel { BrandID = 14, Description = "Octavia"},

                new CarModel { BrandID = 15, Description = "Swift"},

                new CarModel { BrandID = 16, Description = "Yaris"},
                new CarModel { BrandID = 16, Description = "Auris"},

                new CarModel { BrandID = 17, Description = "Up"},
                new CarModel { BrandID = 17, Description = "Polo"},
                new CarModel { BrandID = 17, Description = "Golf"},
                new CarModel { BrandID = 17, Description = "Sharan"},

                new CarModel { BrandID = 18, Description = "S60"},
                new CarModel { BrandID = 18, Description = "V40"},
                new CarModel { BrandID = 18, Description = "XC90"},
                new CarModel { BrandID = 18, Description = "v60"},
            };
            carmodel.ForEach(s => context.CarModels.AddOrUpdate(p => p.Description, s));
            context.SaveChanges();

            var country = new List<Country>
            {
                new Country { Name = "Alemanha",   },
                new Country { Name = "Áustria",   },
                new Country { Name = "Bélgica",   },
                new Country { Name = "Bulgaria",   },
                new Country { Name = "Chipre",   },
                new Country { Name = "Croácia",   },
                new Country { Name = "Dinamarca",   },
                new Country { Name = "Eslováquia",   },
                new Country { Name = "Eslovénia",   },
                new Country { Name = "Espanha",   },
                new Country { Name = "Estónia",   },
                new Country { Name = "Finlândia",   },
                new Country { Name = "França",   },
                new Country { Name = "Grécia",   },
                new Country { Name = "Hungria",   },
                new Country { Name = "Irlanda",   },
                new Country { Name = "Itália",   },
                new Country { Name = "Letónia",   },
                new Country { Name = "Lituânia",   },
                new Country { Name = "Luxemburgo",   },
                new Country { Name = "Malta",   },
                new Country { Name = "Países Baixos",   },
                new Country { Name = "Polónia",   },
                new Country { Name = "Portugal",   },
                new Country { Name = "Reino Unido",   },
                new Country { Name = "República Checa",   },
                new Country { Name = "Roméia",   },
                new Country { Name = "Suécia",   },
            };
            country.ForEach(s => context.Countries.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var mp = new List<MeetingPoint>
            {
                new MeetingPoint { ID = 1, Place = "Aeroporto Francisco Sá Carneiro (Porto)" },
                new MeetingPoint { ID = 2, Place = "Aeroporto da Portela (Lisboa)" },
                new MeetingPoint { ID = 3, Place = "Estação comboio (Aveiro)" }
            };            
            mp.ForEach(s => context.MeetingPoints.AddOrUpdate(p => p.Place, s));
            context.SaveChanges();

            var fuel = new List<FuelType>
            {
                new FuelType { ID = 1, Description = "Diesel" },
                new FuelType { ID = 2, Description = "Gasolina" }
            };
            fuel.ForEach(s => context.FuelTypes.AddOrUpdate(p=> p.Description,s));
            context.SaveChanges();

            var mtypes = new List<ExtraModelType>
            {
                new ExtraModelType {ID = 1, Description = "GPS" },
                new ExtraModelType {ID = 2, Description = "Car Seat for Baby" }
            };
            mtypes.ForEach(s => context.ExtraModelTypes.AddOrUpdate(p=>p.Description,s));
            context.SaveChanges();

            var mod = new List<ExtraModel>
            {
                new ExtraModel {ID = 1, ExtraModelTypeID = 1, Model = "GPS OP", Price = 50, Stock=5 },
                new ExtraModel {ID = 2, ExtraModelTypeID = 2, Model="Safe Seat", Price = 20, Stock=3 }
            };
            mod.ForEach(s => context.ExtraModels.AddOrUpdate(p=> p.ExtraModelTypeID ,s));
            context.SaveChanges();

            var item = new List<ExtraItem>
            {
                new ExtraItem {ID = 1, ExtraModelID = 1 },
                new ExtraItem {ID = 1, ExtraModelID = 2 }
            };
            item.ForEach(s => context.ExtraItems.AddOrUpdate(p=>p.ExtraModelID,s));
            context.SaveChanges();

            var prom = new List<Promotion>
            {
                new Promotion {ID = 1, Description = "Promotion 10%", Percentage=10, Days=10 },
                new Promotion {ID = 1, Description = "Promotion 15%", Percentage=15, Days=15 },
                new Promotion {ID = 1, Description = "Promotion 20%", Percentage=20, Days=20  }
            };
            prom.ForEach(s => context.Promotions.AddOrUpdate(p => p.ID, s));
            context.SaveChanges();

            var user = new ApplicationUser { UserName = "SuperAdmin@Super.com",  Name = "MasterAdmin", Email = "SuperAdmin@Super.com", CountryID=1 };

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
            category.ForEach(s => context.Categories.AddOrUpdate(p => p.CategoryName, s));
            context.SaveChanges();
        }
    }
}
