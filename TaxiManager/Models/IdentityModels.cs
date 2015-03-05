using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;

namespace TaxiManager.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string Adress { get; set; }
    }

    public class TaxiContext : IdentityDbContext<ApplicationUser>
    {
        public TaxiContext()
            : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
    }
}