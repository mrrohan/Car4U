namespace Car4U.Migrations
{
    using Car4U.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
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


            var user = new ApplicationUser { UserName = "SuperAdmin@Super.com",  Name = "MasterAdmin", Email = "SuperAdmin@Super.com" };

            if (userManager.FindByName("SuperAdmin@Super.com") == null)
            {
                var result = userManager.Create(user, "password");

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
            }
        }
    }
}
