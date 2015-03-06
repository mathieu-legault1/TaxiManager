using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using TaxiManager.Models;

namespace TaxiManager.Controllers
{
    [Authorize(Roles = "Agency")]
    public class AgencyController : Controller
    {
        private TaxiContext db = new TaxiContext();

        // GET: Agency
        public ActionResult Index()
        {
            // Add customers to session 
            Session["Customers"] = JsonConvert.SerializeObject(db.Customers.ToList<Customer>());

            return View();
        }

        [HttpPost]
        public ActionResult NewCustomer([Bind(Include = "CustomerID, Adress, FirstName, LastName")] Customer model)
        {            
            if (ModelState.IsValid)
            {
                db.Customers.Add(model);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}