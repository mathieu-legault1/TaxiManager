using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Xml;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using TaxiManager.Models;

namespace TaxiManager.Controllers
{
    public class BaseController : Controller
    {
        public TaxiContext db;
        public UserManager<ApplicationUser> mgr;

        public BaseController()
        {
            db = new TaxiContext();
            mgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        public void SetCustomersInSession()
        {
            var customers = db.Customers.ToList();

            foreach (var customer in customers)
            {
                try
                {
                    var coordinates = LatLngFromAddress(customer.Adress);

                    customer.Latitude = coordinates.Lat;
                    customer.Longitude = coordinates.Lng;
                }
                catch (Exception)
                {
                }
            }

            // Add customers to session 
            Session["Customers"] = JsonConvert.SerializeObject(customers);
        }

        public LatLng LatLngFromAddress(string address)
        {
            var requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false", Uri.EscapeDataString(address));

            var request = WebRequest.Create(requestUri);
            var response = request.GetResponse();
            var xdoc = new XmlDocument();
            xdoc.Load(response.GetResponseStream());

            var resp = xdoc["GeocodeResponse"];
            var result = resp["result"];
            var geo = result["geometry"];
            var loc = geo["location"];
            var lat = loc["lat"].InnerText;
            var lng = loc["lng"].InnerText;

            return new LatLng
            {
                Lat = lat,
                Lng = lng
            };
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