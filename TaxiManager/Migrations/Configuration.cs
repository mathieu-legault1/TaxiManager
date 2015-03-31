namespace TaxiManager.Migrations
{
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;
	using TaxiManager.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TaxiContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
		}

        protected override void Seed(TaxiContext context)
		{
            // Roles
            if (!context.Roles.Any(r => r.Name == "Taxi"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);

                manager.Create(new IdentityRole { Name = "Taxi" });
                manager.Create(new IdentityRole { Name = "Agency" });
            }

            // Users
            if (!context.Users.Any(u => u.UserName == "Taxi"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var taxiUser = new ApplicationUser { UserName = "Taxi", Adress = "204 boulevard longpré, laval, QC, H7L 3C9" };
                var taxiUser2 = new ApplicationUser { UserName = "Taxi2", Adress = "625 Boulevard Curé-Labelle, Laval, QC H7L 5R7" };
                var agencyUser = new ApplicationUser { UserName = "Agency" };

                manager.Create(taxiUser, "Taxi123[");
                manager.AddToRole(taxiUser.Id, "Taxi");

                manager.Create(taxiUser2, "Taxi123[");
                manager.AddToRole(taxiUser2.Id, "Taxi");

                manager.Create(agencyUser, "Agency123[");
                manager.AddToRole(agencyUser.Id, "Agency");
            }
		}
	}
}
