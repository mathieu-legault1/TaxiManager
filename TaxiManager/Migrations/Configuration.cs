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
			var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
			var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

			context.Roles.AddOrUpdate(new IdentityRole[] {                
				new IdentityRole("Agency"),
				new IdentityRole("Taxi")
			});

			// Add default users            
            if (UserManager.FindByName("Agency") == null)
			{
                var agence = new ApplicationUser() { UserName = "Agency" };

                UserManager.Create(agence, "Agency123[");
                UserManager.AddToRole(agence.Id, "Agency");
			}

			if (UserManager.FindByName("Taxi") == null)
			{
				var taxi = new ApplicationUser() { UserName = "Taxi" };

				UserManager.Create(taxi, "Taxi123[");
				UserManager.AddToRole(taxi.Id, "Taxi");
			}     

			// Add default clients
			context.Customers.AddOrUpdate(new Customer[] {
				new Customer() {
					FirstName = "John",
					LastName = "Doe",
					Status = Status.Denied,
				},
				new Customer() {
					FirstName = "Jane",
					LastName = "Doe",
					Status = Status.Accepted,
				},
				new Customer() {
					FirstName = "Mat",
					LastName = "Doe",
					Adress = "",
					Status = Status.OnHold
				}            
			});
			
			base.Seed(context);
		}
	}
}
