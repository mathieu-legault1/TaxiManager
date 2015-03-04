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
    public class HomeController : Controller
    {        

        public HomeController()
        {
            //
        }

        public ActionResult Index()
        { 
            using (var cont = new ApplicationDbContext())
            {
                cont.Customers.Add(new Customer() {
                    Adress = "adsfsdfad",
                    FirstName = "sdSDsdSS",
                    LastName = "asadsfasdf",
                    Status = Status.OnHold                   
                });

                cont.SaveChanges();
            }

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