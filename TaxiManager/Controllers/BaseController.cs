using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using TaxiManager.Models;

namespace TaxiManager.Controllers
{
    public class BaseController : Controller
    {
        public TaxiContext db = new TaxiContext();

        public void SetCustomersInSession()
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
        }

        public void SetTaxiInSession()
        {
            var taxis = db.Users.ToList();
            IdentityUserRole taxiIdentityRole  = new IdentityUserRole();
            taxiIdentityRole.Role = new IdentityRole("Taxi");
            taxis = taxis.Where(item => item.Roles.Contains(taxiIdentityRole)).ToList();
            foreach (var taxi in taxis)
            {
                try
                {
                    // Get latitude and longitude for all current customers
                    // We need these to call the API
                    var requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false", Uri.EscapeDataString(taxi.CurrentAdress));

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
                    taxi.Latitude = lat;
                    taxi.Longitude = lng;
                }

                catch (Exception)
                {
                }
            }

            // Add customers to session 
            Session["Taxis"] = JsonConvert.SerializeObject(taxis);
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
