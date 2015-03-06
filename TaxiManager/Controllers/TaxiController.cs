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
    public class TaxiController : Controller
    {
        private TaxiContext db = new TaxiContext();

        // GET: Taxi
        public ActionResult Index()
        {
            Session["Customers"] = JsonConvert.SerializeObject(db.Customers.ToList<Customer>());

            return View();
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