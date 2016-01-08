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

            var carsbrand = new List<Brand>
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
            carsbrand.ForEach(s => context.Brands.AddOrUpdate(p => p.Description, s));
            context.SaveChanges();

            var carmodel = new List<CarModel>
            {
                new CarModel { ID = 1, BrandID = 1, Description = "Mito"},
                new CarModel { ID = 2, BrandID = 1, Description = "Giulietta"},

                new CarModel { ID = 3, BrandID = 2, Description = "A1"},
                new CarModel { ID = 4, BrandID = 2, Description = "A3"},
                new CarModel { ID = 5, BrandID = 2, Description = "A4"},

                new CarModel { ID = 6, BrandID = 3, Description = "Série 1"},
                new CarModel { ID = 7, BrandID = 3, Description = "Série 3"},
                new CarModel { ID = 8, BrandID = 3, Description = "Série 5"},

                new CarModel { ID = 9, BrandID = 4, Description = "C1"},
                new CarModel { ID = 10, BrandID = 4, Description = "C3"},
                new CarModel { ID = 11, BrandID = 4, Description = "C4"},

                new CarModel { ID = 12, BrandID = 5, Description = "Punto"},
                new CarModel { ID = 13, BrandID = 5, Description = "500"},

                new CarModel { ID = 14, BrandID = 6, Description = "Fiesta"},
                new CarModel { ID = 15, BrandID = 6, Description = "Focus"},
                new CarModel { ID = 16, BrandID = 6, Description = "Focus SW"},
                new CarModel { ID = 17, BrandID = 6, Description = "S-Max"},

                new CarModel { ID = 18, BrandID = 7, Description = "Ceed"},

                new CarModel { ID = 19, BrandID = 8, Description = "Classe A"},
                new CarModel { ID = 20, BrandID = 8, Description = "Classe B"},
                new CarModel { ID = 21, BrandID = 8, Description = "Classe C"},
                new CarModel { ID = 22, BrandID = 8, Description = "Vito"},

                new CarModel { ID = 23, BrandID = 9, Description = "Note"},
                new CarModel { ID = 24, BrandID = 9, Description = "Juke"},
                new CarModel { ID = 25, BrandID = 9, Description = "Pulsar"},

                new CarModel { ID = 26, BrandID = 10, Description = "Corsa"},
                new CarModel { ID = 27, BrandID = 10, Description = "Astra"},
                new CarModel { ID = 28, BrandID = 10, Description = "Astra SW"},

                new CarModel { ID = 29, BrandID = 11, Description = "108"},
                new CarModel { ID = 30, BrandID = 11, Description = "208"},
                new CarModel { ID = 31, BrandID = 11, Description = "308"},
                new CarModel { ID = 32, BrandID = 11, Description = "308 SW"},
                new CarModel { ID = 33, BrandID = 11, Description = "508"},

                new CarModel { ID = 34, BrandID = 12, Description = "Twingo"},
                new CarModel { ID = 35, BrandID = 12, Description = "Clio"},
                new CarModel { ID = 36, BrandID = 12, Description = "Megane"},
                new CarModel { ID = 37, BrandID = 12, Description = "Megane SW"},
                new CarModel { ID = 38, BrandID = 12, Description = "Scenic"},

                new CarModel { ID = 39, BrandID = 13, Description = "Ibiza"},
                new CarModel { ID = 40, BrandID = 13, Description = "Leon"},
                new CarModel { ID = 41, BrandID = 13, Description = "Leon SW"},

                new CarModel { ID = 42, BrandID = 14, Description = "Fabia"},
                new CarModel { ID = 43, BrandID = 14, Description = "Octavia"},

                new CarModel { ID = 44, BrandID = 15, Description = "Swift"},

                new CarModel { ID = 45, BrandID = 16, Description = "Yaris"},
                new CarModel { ID = 46, BrandID = 16, Description = "Auris"},

                new CarModel { ID = 47, BrandID = 17, Description = "Up"},
                new CarModel { ID = 48, BrandID = 17, Description = "Polo"},
                new CarModel { ID = 49, BrandID = 17, Description = "Golf"},
                new CarModel { ID = 50, BrandID = 17, Description = "Sharan"},

                new CarModel { ID = 51, BrandID = 18, Description = "S60"},
                new CarModel { ID = 52, BrandID = 18, Description = "V40"},
                new CarModel { ID = 53, BrandID = 18, Description = "XC90"},
                new CarModel { ID = 54, BrandID = 18, Description = "v60"},
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
                new FuelType { ID = 2, Description = "Gasolina" },
                new FuelType { ID = 3, Description = "Eletricidade" },
            };
            fuel.ForEach(s => context.FuelTypes.AddOrUpdate(p=> p.Description,s));
            context.SaveChanges();

            var mtypes = new List<ExtraModelType>
            {
                new ExtraModelType {ID = 1, Description = "GPS" },
                new ExtraModelType {ID = 2, Description = "Car Seat for Baby" },
                new ExtraModelType {ID = 3, Description = "Via Verde" }
            };
            mtypes.ForEach(s => context.ExtraModelTypes.AddOrUpdate(p=>p.Description,s));
            context.SaveChanges();

            var mod = new List<ExtraModel>
            {
                new ExtraModel {ID = 1, ExtraModelTypeID = 1, Model = "GPS TPSI 1", Price = 50, Stock=5 },
                new ExtraModel {ID = 2, ExtraModelTypeID = 2, Model = "Safe Seat 1", Price = 20, Stock=3 },
                new ExtraModel {ID = 3, ExtraModelTypeID = 1, Model = "GPS TPSI 2", Price = 40, Stock=8 },
                new ExtraModel {ID = 4, ExtraModelTypeID = 1, Model = "GPS TPSI 3", Price = 35, Stock=3 },
                new ExtraModel {ID = 5, ExtraModelTypeID = 1, Model = "GPS TPSI 4", Price = 10, Stock=7 },
                new ExtraModel {ID = 6, ExtraModelTypeID = 2, Model = "Safe Seat 2", Price = 20, Stock=3 },
                new ExtraModel {ID = 7, ExtraModelTypeID = 2, Model = "Safe Seat 3", Price = 20, Stock=3 },
                new ExtraModel {ID = 8, ExtraModelTypeID = 3, Model = "Via Verde", Price = 10, Stock=8 },
               
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
                new Category { ID = 1, CategoryName = "Económico", Price = 20, Warranty = 1000,  },
                new Category { ID = 2, CategoryName = "Compacto", Price = 30, Warranty = 1350 },
                new Category { ID = 3, CategoryName = "Familiar", Price = 40, Warranty = 1750,  },
                new Category { ID = 4, CategoryName = "Luxo", Price = 100, Warranty = 2000,  },
                new Category { ID = 5, CategoryName = "7 Lugares", Price = 95, Warranty = 2500,  },
                new Category { ID = 6, CategoryName = "9 Lugares", Price = 110, Warranty = 2500,  },
            };
            category.ForEach(s => context.Categories.AddOrUpdate(p => p.CategoryName, s));
            context.SaveChanges();

            var gears = new List<Gear>
            {
                new Gear { ID = 1, Description = "Manual" },
                new Gear { ID = 2, Description = "Automático" },
            };
            gears.ForEach(s => context.Gears.AddOrUpdate(p => p.Description, s));
            context.SaveChanges();

            var cars = new List<Car>
            {
                new Car { ID = 1, LicensePlate = "00-RA-01", RegisterDate = "2016-01-06", NDoors = 3, NLuggage = 1, Engine = "1400", HorsePower = "95cv", CategoryID = 1, FuelTypeID = 2, GearID = 1, CarModelID = 1},
              new Car { ID = 2, LicensePlate = "00-RA-02", RegisterDate = "2016-01-06", NDoors = 5, NLuggage = 2, Engine = "1600", HorsePower = "105cv", CategoryID = 3, FuelTypeID = 1, GearID = 1, CarModelID = 2},
              new Car { ID = 3, LicensePlate = "00-RA-03", RegisterDate = "2016-01-06", NDoors = 3, NLuggage = 1, Engine = "1200", HorsePower = "85cv", CategoryID = 1, FuelTypeID = 2, GearID = 1, CarModelID = 3},
              new Car { ID = 4, LicensePlate = "00-RA-04", RegisterDate = "2016-01-06", NDoors = 5, NLuggage = 2, Engine = "1600", HorsePower = "100cv", CategoryID = 2, FuelTypeID = 1, GearID = 1, CarModelID = 4},
              new Car { ID = 5, LicensePlate = "00-RA-05", RegisterDate = "2016-01-06", NDoors = 5, NLuggage = 2, Engine = "2000", HorsePower = "150cv", CategoryID = 4, FuelTypeID = 1, GearID = 2, CarModelID = 5},
              new Car { ID = 6, LicensePlate = "00-RA-06", RegisterDate = "2016-01-06", NDoors = 3, NLuggage = 1, Engine = "1200", HorsePower = "85cv", CategoryID = 1, FuelTypeID = 2, GearID = 1, CarModelID = 6},
              new Car { ID = 7, LicensePlate = "00-RA-07", RegisterDate = "2016-01-06", NDoors = 5, NLuggage = 2, Engine = "1600", HorsePower = "100cv", CategoryID = 2, FuelTypeID = 1, GearID = 1, CarModelID = 7},
              new Car { ID = 8, LicensePlate = "00-RA-08", RegisterDate = "2016-01-06", NDoors = 5, NLuggage = 2, Engine = "2000", HorsePower = "150cv", CategoryID = 4, FuelTypeID = 1, GearID = 1, CarModelID = 8},
              new Car { ID = 9, LicensePlate = "00-RA-09", RegisterDate = "2016-01-06", NDoors = 5, NLuggage = 1, Engine = "1200", HorsePower = "85cv", CategoryID = 1, FuelTypeID = 2, GearID = 1, CarModelID = 9},
              new Car { ID = 10, LicensePlate = "00-RA-10", RegisterDate = "2016-01-06", NDoors = 5, NLuggage = 2, Engine = "1600", HorsePower = "100cv", CategoryID = 2, FuelTypeID = 2, GearID = 1, CarModelID = 10},
              new Car { ID = 11, LicensePlate = "00-RA-11", RegisterDate = "2016-01-06", NDoors = 5, NLuggage = 2, Engine = "2000", HorsePower = "150cv", CategoryID = 2, FuelTypeID = 1, GearID = 2, CarModelID = 11},
              new Car { ID = 12, LicensePlate = "00-RA-12", RegisterDate = "2016-01-06", NDoors = 3, NLuggage = 1, Engine = "1200", HorsePower = "85cv", CategoryID = 1, FuelTypeID = 2, GearID = 1, CarModelID = 12},
              new Car { ID = 13, LicensePlate = "00-RA-13", RegisterDate = "2016-01-06", NDoors = 5, NLuggage = 2, Engine = "1600", HorsePower = "90cv", CategoryID = 2, FuelTypeID = 1, GearID = 1, CarModelID = 13},
              new Car { ID = 14, LicensePlate = "00-RA-14", RegisterDate = "2016-01-06", NDoors = 5, NLuggage = 1, Engine = "1200", HorsePower = "85cv", CategoryID = 1, FuelTypeID = 2, GearID = 1, CarModelID = 14},
              new Car { ID = 15, LicensePlate = "00-RA-15", RegisterDate = "2016-01-06", NDoors = 5, NLuggage = 2, Engine = "1600", HorsePower = "100cv", CategoryID = 2, FuelTypeID = 1, GearID = 1, CarModelID = 15},
              new Car { ID = 16, LicensePlate = "00-RA-16", RegisterDate = "2016-01-06", NDoors = 5, NLuggage = 3, Engine = "1600", HorsePower = "110cv", CategoryID = 3, FuelTypeID = 1, GearID = 1, CarModelID = 16},
              new Car { ID = 17, LicensePlate = "00-RA-17", RegisterDate = "2016-01-06", NDoors = 5, NLuggage = 4, Engine = "2000", HorsePower = "140cv", CategoryID = 5, FuelTypeID = 1, GearID = 1, CarModelID = 17},
              new Car { ID = 18, LicensePlate = "00-RA-18", RegisterDate = "2016-01-06", NDoors = 5, NLuggage = 2, Engine = "1600", HorsePower = "100cv", CategoryID = 2, FuelTypeID = 2, GearID = 1, CarModelID = 18},
              new Car { ID = 19, LicensePlate = "00-RA-19", RegisterDate = "2016-01-06", NDoors = 5, NLuggage = 2, Engine = "1600", HorsePower = "100cv", CategoryID = 2, FuelTypeID = 1, GearID = 1, CarModelID = 19},
              new Car { ID = 20, LicensePlate = "00-RA-20", RegisterDate = "2016-01-06", NDoors = 5, NLuggage = 2, Engine = "1900", HorsePower = "140cv", CategoryID = 4, FuelTypeID = 1, GearID = 2, CarModelID = 20},
              new Car { ID = 21, LicensePlate = "00-RA-21", RegisterDate = "2016-01-06", NDoors = 5, NLuggage = 2, Engine = "2000", HorsePower = "150cv", CategoryID = 4, FuelTypeID = 1, GearID = 2, CarModelID = 21},
              new Car { ID = 22, LicensePlate = "00-RA-22", RegisterDate = "2016-01-06", NDoors = 5, NLuggage = 5, Engine = "2000", HorsePower = "150cv", CategoryID = 6, FuelTypeID = 1, GearID = 1, CarModelID = 22},
              new Car { ID = 23, LicensePlate = "00-RA-23", RegisterDate = "2016-01-06", NDoors = 5, NLuggage = 1, Engine = "1200", HorsePower = "85cv", CategoryID = 1, FuelTypeID = 2, GearID = 1, CarModelID = 23},
              new Car { ID = 24, LicensePlate = "00-RA-24", RegisterDate = "2016-01-06", NDoors = 5, NLuggage = 2, Engine = "1600", HorsePower = "100cv", CategoryID = 2, FuelTypeID = 1, GearID = 1, CarModelID = 24},
              new Car { ID = 25, LicensePlate = "00-RA-25", RegisterDate = "2016-01-06", NDoors = 5, NLuggage = 2, Engine = "1900", HorsePower = "110cv", CategoryID = 2, FuelTypeID = 1, GearID = 2, CarModelID = 25},
              new Car { ID = 26, LicensePlate = "00-RA-26", RegisterDate = "2016-01-06", NDoors = 5, NLuggage = 1, Engine = "1200", HorsePower = "85cv", CategoryID = 1, FuelTypeID = 2, GearID = 1, CarModelID = 26},
              new Car { ID = 27, LicensePlate = "00-RA-27", RegisterDate = "2016-01-06", NDoors = 5, NLuggage = 2, Engine = "1600", HorsePower = "100cv", CategoryID = 2, FuelTypeID = 1, GearID = 1, CarModelID = 27},
              new Car { ID = 28, LicensePlate = "00-RA-28", RegisterDate = "2016-01-06", NDoors = 3, NLuggage = 3, Engine = "1600", HorsePower = "110cv", CategoryID = 3, FuelTypeID = 1, GearID = 1, CarModelID = 28},
              new Car { ID = 29, LicensePlate = "00-RA-29", RegisterDate = "2016-01-06", NDoors = 3, NLuggage = 1, Engine = "1000", HorsePower = "90cv", CategoryID = 1, FuelTypeID = 2, GearID = 1, CarModelID = 29},
              new Car { ID = 30, LicensePlate = "00-RA-30", RegisterDate = "2016-01-06", NDoors = 5, NLuggage = 1, Engine = "1200", HorsePower = "85cv", CategoryID = 1, FuelTypeID = 2, GearID = 1, CarModelID = 30},
              new Car { ID = 31, LicensePlate = "00-RA-31", RegisterDate = "2016-01-06", NDoors = 5, NLuggage = 2, Engine = "1600", HorsePower = "100cv", CategoryID = 2, FuelTypeID = 1, GearID = 1, CarModelID = 31},
              new Car { ID = 32, LicensePlate = "00-RA-32", RegisterDate = "2016-01-06", NDoors = 5, NLuggage = 3, Engine = "1600", HorsePower = "110cv", CategoryID = 3, FuelTypeID = 1, GearID = 1, CarModelID = 32},
              new Car { ID = 33, LicensePlate = "00-RA-33", RegisterDate = "2016-01-06", NDoors = 5, NLuggage = 2, Engine = "2000", HorsePower = "140cv", CategoryID = 4, FuelTypeID = 1, GearID = 2, CarModelID = 33},
              new Car { ID = 34, LicensePlate = "00-RA-34", RegisterDate = "2016-01-06", NDoors = 3, NLuggage = 1, Engine = "1000", HorsePower = "90cv", CategoryID = 1, FuelTypeID = 2, GearID = 1, CarModelID = 34},
              new Car { ID = 35, LicensePlate = "00-RA-35", RegisterDate = "2016-01-06", NDoors = 5, NLuggage = 1, Engine = "1200", HorsePower = "85cv", CategoryID = 1, FuelTypeID = 2, GearID = 1, CarModelID = 35},
              new Car { ID = 36, LicensePlate = "00-RA-36", RegisterDate = "2016-01-06", NDoors = 5, NLuggage = 2, Engine = "1600", HorsePower = "100cv", CategoryID = 2, FuelTypeID = 1, GearID = 1, CarModelID = 36},
              new Car { ID = 37, LicensePlate = "00-RA-37", RegisterDate = "2016-01-06", NDoors = 5, NLuggage = 3, Engine = "1600", HorsePower = "110cv", CategoryID = 3, FuelTypeID = 1, GearID = 1, CarModelID = 37},
              new Car { ID = 38, LicensePlate = "00-RA-38", RegisterDate = "2016-01-06", NDoors = 5, NLuggage = 4, Engine = "2000", HorsePower = "140cv", CategoryID = 5, FuelTypeID = 1, GearID = 2, CarModelID = 38},
              new Car { ID = 39, LicensePlate = "00-RA-39", RegisterDate = "2016-01-06", NDoors = 5, NLuggage = 1, Engine = "1200", HorsePower = "85cv", CategoryID = 1, FuelTypeID = 2, GearID = 1, CarModelID = 39},
              new Car { ID = 40, LicensePlate = "00-RA-40", RegisterDate = "2016-01-06", NDoors = 5, NLuggage = 2, Engine = "1600", HorsePower = "100cv", CategoryID = 2, FuelTypeID = 1, GearID = 1, CarModelID = 40},
              new Car { ID = 41, LicensePlate = "00-RA-41", RegisterDate = "2016-01-06", NDoors = 5, NLuggage = 3, Engine = "1600", HorsePower = "110cv", CategoryID = 3, FuelTypeID = 1, GearID = 1, CarModelID = 41},
            };
            cars.ForEach(s => context.Cars.AddOrUpdate(p => p.LicensePlate, s));
            context.SaveChanges();

           var filepath = new List<FilePath>
            {
                new FilePath { CarID = 1, FileName = "AlfaRomeo_Mito.jpg", FilePathId = 1, FileType = FileType.Photo },
                new FilePath { CarID = 2, FileName = "AlfaRomeo_Giulietta.jpg", FilePathId = 2, FileType = FileType.Photo},
                new FilePath { CarID = 3, FileName = "Audi_A1.jpg", FilePathId = 2, FileType = FileType.Photo},
                new FilePath { CarID = 4, FileName = "Audi_A3.jpg", FilePathId = 2, FileType = FileType.Photo},
                new FilePath { CarID = 5, FileName = "Audi_A4.jpg", FilePathId = 2, FileType = FileType.Photo},
                new FilePath { CarID = 6, FileName = "BMW_Serie1.jpg", FilePathId = 2, FileType = FileType.Photo},
                new FilePath { CarID = 7, FileName = "BMW_Serie3.jpg", FilePathId = 2, FileType = FileType.Photo},
                new FilePath { CarID = 8, FileName = "BMW_Serie5.jpg", FilePathId = 2, FileType = FileType.Photo},
                new FilePath { CarID = 9, FileName = "Citroen_C1.jpg", FilePathId = 2, FileType = FileType.Photo},
                new FilePath { CarID = 10, FileName = "Citroen_C3.jpg", FilePathId = 2, FileType = FileType.Photo},
                new FilePath { CarID = 11, FileName = "Citroen_C4.jpg", FilePathId = 2, FileType = FileType.Photo},
                new FilePath { CarID = 12, FileName = "Fiat_500.jpg", FilePathId = 2, FileType = FileType.Photo},
                new FilePath { CarID = 13, FileName = "Fiat_Punto.jpg", FilePathId = 2, FileType = FileType.Photo},
                new FilePath { CarID = 14, FileName = "Ford_Fiesta.jpg", FilePathId = 2, FileType = FileType.Photo},
                new FilePath { CarID = 15, FileName = "Ford_Focus.jpg", FilePathId = 2, FileType = FileType.Photo},
                new FilePath { CarID = 16, FileName = "Ford_FocusSW.jpg", FilePathId = 2, FileType = FileType.Photo},
                new FilePath { CarID = 17, FileName = "Ford_SMax.jpg", FilePathId = 2, FileType = FileType.Photo},
                new FilePath { CarID = 18, FileName = "Kia_Ceed.jpg", FilePathId = 2, FileType = FileType.Photo},
                new FilePath { CarID = 19, FileName = "Mercedes_ClasseA.jpg", FilePathId = 2, FileType = FileType.Photo},
                new FilePath { CarID = 20, FileName = "Mercedes_ClasseB.jpg", FilePathId = 2, FileType = FileType.Photo},
                new FilePath { CarID = 21, FileName = "Mercedes_ClasseC.jpg", FilePathId = 2, FileType = FileType.Photo},
                new FilePath { CarID = 22, FileName = "Mercedes_Vito.jpg", FilePathId = 2, FileType = FileType.Photo},
                new FilePath { CarID = 23, FileName = "Nissan_Note.jpg", FilePathId = 2, FileType = FileType.Photo},
                new FilePath { CarID = 24, FileName = "Nissan_Juke.jpg", FilePathId = 2, FileType = FileType.Photo},
                new FilePath { CarID = 25, FileName = "Nissan_Pulsar.jpg", FilePathId = 2, FileType = FileType.Photo},
                new FilePath { CarID = 26, FileName = "Opel_Corsa.jpg", FilePathId = 2, FileType = FileType.Photo},
                new FilePath { CarID = 27, FileName = "Opel_Astra.jpg", FilePathId = 2, FileType = FileType.Photo},
                new FilePath { CarID = 28, FileName = "Opel_AstraSW.jpg", FilePathId = 2, FileType = FileType.Photo},
                new FilePath { CarID = 29, FileName = "Peugeot_108.jpg", FilePathId = 2, FileType = FileType.Photo},
                new FilePath { CarID = 30, FileName = "Peugeot_208.jpg", FilePathId = 2, FileType = FileType.Photo},
                new FilePath { CarID = 31, FileName = "Peugeot_308.jpg", FilePathId = 2, FileType = FileType.Photo},
                new FilePath { CarID = 32, FileName = "Peugeot_308SW.jpg", FilePathId = 2, FileType = FileType.Photo},
                new FilePath { CarID = 33, FileName = "Peugeot_508.jpg", FilePathId = 2, FileType = FileType.Photo},
                new FilePath { CarID = 34, FileName = "Renault_Twingo.jpg", FilePathId = 2, FileType = FileType.Photo},
                new FilePath { CarID = 35, FileName = "Renault_Clio.jpg", FilePathId = 2, FileType = FileType.Photo},
                new FilePath { CarID = 36, FileName = "Renault_Megane.jpg", FilePathId = 2, FileType = FileType.Photo},
                new FilePath { CarID = 37, FileName = "Renault_MeganeSW.jpg", FilePathId = 2, FileType = FileType.Photo},
                new FilePath { CarID = 38, FileName = "Renault_Scenic.jpg", FilePathId = 2, FileType = FileType.Photo},
                new FilePath { CarID = 39, FileName = "Seat_Ibiza.jpg", FilePathId = 2, FileType = FileType.Photo},
                new FilePath { CarID = 40, FileName = "Seat_Leon.jpg", FilePathId = 2, FileType = FileType.Photo},
                new FilePath { CarID = 41, FileName = "Seat_LeonSW.jpg", FilePathId = 2, FileType = FileType.Photo},
            };
            filepath.ForEach(s => context.FilePaths.AddOrUpdate(p => p.FileName, s));
            context.SaveChanges();

            var status = new List<Status>
            {
                new Status { ID = 1, Description = "Venda" },
                new Status { ID = 2, Description = "Oficina" },
                new Status { ID = 3, Description = "Disponivel" },
                new Status { ID = 4, Description = "Lavagem" },
                new Status { ID = 5, Description = "Reservado" },
            };
            status.ForEach(s => context.Status.AddOrUpdate(p => p.Description, s));
            context.SaveChanges();
        }
    }
}
