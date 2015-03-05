namespace TaxiManager.Migrations
{
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;
	using TaxiManager.Models;

	internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = true;
		}

		protected override void Seed(ApplicationDbContext context)
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
                var taxiUser = new ApplicationUser { UserName = "Taxi" };
                var agencyUser = new ApplicationUser { UserName = "Agency" };

                manager.Create(taxiUser, "Taxi123[");
                manager.AddToRole(taxiUser.Id, "Taxi");

                manager.Create(agencyUser, "Agency123[");
                manager.AddToRole(agencyUser.Id, "Agency");
            }
		}
	}
}
