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

                    return (NewRoute(customerID));
                case "Refuser":

                    return (Delete(customerID));
            }

            return View("Index");
        }

        public ActionResult NewRoute(int id)
        {


            return RedirectToAction("Index");
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