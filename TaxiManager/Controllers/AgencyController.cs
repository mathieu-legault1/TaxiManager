using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            var customers = db.Customers.ToList();

            foreach (var customer in customers)
            {
                try
                {
                    // Get latitude and longitude for all current customers
                    // We need these to call the API
                    var requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false", Uri.EscapeDataString(customer.Adress));

                    var request = WebRequest.Create(requestUri);
                    var response = request.GetResponse();
                    var xdoc = new System.Xml.XmlDocument();
                    xdoc.Load(response.GetResponseStream());

                    var resp = xdoc["GeocodeResponse"];
                    var result = resp["result"];
                    var geo = result["geometry"];
                    var loc = geo["location"];
                    var lat = loc["lat"].InnerText;
                    var lng = loc["lng"].InnerText;

                    customer.Latitude = lat;
                    customer.Longitude = lng;
                }                

                catch (Exception)
                {
                }
            }

            // Add customers to session 
            Session["Customers"] = JsonConvert.SerializeObject(customers);
            
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