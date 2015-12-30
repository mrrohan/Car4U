﻿using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace Car4U.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public const string NameClaimType = "Name";
        public string Name { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string License { get; set; }
        public string BI { get; set; }


        public int CountryID { get; set; }


        public virtual Country Country { get; set; }
        public virtual ICollection<Car> cars { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim(NameClaimType, Name));
            return userIdentity;
        }
    }

   
}