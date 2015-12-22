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
                new Category { ID = 1, CategoryName = "Category A", Price = 0, Warranty = 0,  },
                new Category { ID = 2, CategoryName = "Category b", Price = 0, Warranty = 0,  },
            };
            category.ForEach(s => context.Categories.Add(s));
            context.SaveChanges();
        }
    }
}