using Car4U.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Car4U.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<CarStatus> CarStatus { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<ExtraItem> ExtraItems { get; set; }
        public DbSet<ExtraModel> ExtraModels { get; set; }
        public DbSet<ExtraModelType> ExtraModelTypes { get; set; }
        public DbSet<FuelType> FuelTypes { get; set; }
        public DbSet<MeetingPoint> MeetingPoints { get; set; }
        public DbSet<MomentDelivery> MomentDeliveries { get; set; }
        public DbSet<MomentReturn> MomentReturns { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Promotion_Reservation> Promotion_Reservations { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<FilePath> FilePaths { get; set; }
       
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Reservation>()
                .HasRequired(c => c.MPDelivery)
                .WithMany()
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Reservation>()
                .HasRequired(c => c.MPReturn)
                .WithMany()
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<MeetingPoint>()
            //    .HasRequired(s => s.Reservations)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);
        }
    }
}