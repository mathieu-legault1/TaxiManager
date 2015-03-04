using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaxiManager.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TaxiManager.Controllers
{
    [Authorize(Roles = "Taxi")]
    public class HomeController : Controller
    {
        //protected UserManager<ApplicationUser> UserManager { get; set; }
        //protected ApplicationDbContext Context { get; set; }

        public HomeController()
        {
            //this.Context = new ApplicationDbContext();
            //this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.Context));
        }

        public ActionResult Index()
        { 
            //var user = UserManager.FindByName(User.Identity.GetUserName());
            //var cust = Context.Customers.FirstOrDefault();

            //Context.Routes.Add(new Route()
            //{
            //    ApplicationUser = user,
            //    Customer = cust
            //});

            //Context.SaveChanges();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}