using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaxiManager.Models;

namespace TaxiManager.Controllers
{
    [Authorize(Roles="Taxi")]
    public class TaxiController : BaseController
    {

        // GET: Taxi
        public ActionResult Index()
        {
            SetCustomersInSession();

            return View();
        }

        public ActionResult CustomerAction(int customerID, string submitAction)
        {
            switch (submitAction)
            {
                case "Accepter":

                    return (Route(customerID));
                case "Refuser":

                    return (Delete(customerID));
            }

            return View("Index");
        }

        public ActionResult Route(int id)
        {
            var currentUser = mgr.FindByName<ApplicationUser>(User.Identity.Name);
            var customer = db.Customers.Find(id);

            if(currentUser != null && customer != null)
            {
                Route route = new Route
                {
                    ApplicationUser = currentUser,
                    DepartureDate = DateTime.Now,
                    Customer = db.Customers.Find(id)         
                };

                var model = new RouteViewModel
                {
                    CustomerLatLng = LatLngFromAddress(customer.Adress),
                    TaxiLatLng = LatLngFromAddress(currentUser.Adress)
                };

                db.Customers.Remove(customer);
                db.SaveChanges();

                return View("Route", model);
            }

            return View();
        }

        public ActionResult Delete(int id)
        {
            Customer customer = db.Customers.Find(id);
            if(customer != null)
            {
                db.Customers.Remove(customer);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}