using System;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
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
        
        [HttpPost()]
        public ActionResult EndRoute(int routeID)
        {
            var route = db.Routes.Find(routeID);
            route.ArrivalDate = DateTime.Now;

            db.Routes.Add(route);
            db.Customers.Remove(db.Customers.Find(route.CustomerID));

            db.SaveChanges();

            return RedirectToAction("Index");
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

        public JsonResult TaxiArrival(int routeID, string clientAddress)
        {
            var route = db.Routes.Find(routeID);
            route.RecoveryDate = DateTime.Now;
            db.SaveChanges();

            var coordinates = LatLngFromAddress(clientAddress);

            return Json(new {  
                lat=coordinates.Lat, 
                lng=coordinates.Lng 
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Route(int clientId)
        {
            var currentUser = mgr.FindByName(User.Identity.Name);
            var customer = db.Customers.Find(clientId);

            if(currentUser != null && customer != null)
            {
                Route route = new Route
                {
                    ApplicationUserID = currentUser.Id,
                    CustomerID = customer.CustomerID,
                    DepartureDate = DateTime.Now,
                };
                db.Routes.Add(route);
                db.SaveChanges();

                var model = new RouteViewModel
                {
                    RouteID = route.RouteID,
                    CustomerLatLng = LatLngFromAddress(customer.Adress),
                    TaxiLatLng = LatLngFromAddress(currentUser.Adress)
                };

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