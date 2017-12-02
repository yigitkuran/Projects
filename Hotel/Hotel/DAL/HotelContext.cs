using Hotel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Hotel.DAL
{
    public class HotelContext : DbContext
    {
        public HotelContext() : base("Hotel") { }

        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<Rooms> Room { get; set; }
        public DbSet<Price> Price { get; set; }
        public DbSet<Photo> Photo { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet MyProperty { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}