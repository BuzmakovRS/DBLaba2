using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DBLaba2.Models
{
    public class WarehouseContext : DbContext
    {
        public WarehouseContext()
            : base("WarehouseContext")
        {

            //   this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Shelf> Shelfs { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // PostgreSQL uses the public schema by default - not dbo.
            modelBuilder.HasDefaultSchema("public");
 //           modelBuilder.Entity<Product>().Property(p => p.Length).HasPrecision(15, 2);
 //           modelBuilder.Entity<Product>().Property(p => p.Width).HasPrecision(15, 2);
            modelBuilder.Entity<Product>().HasMany(p => p.Locations).WithRequired(p => p.Product);


            base.OnModelCreating(modelBuilder);


        }
    }
}