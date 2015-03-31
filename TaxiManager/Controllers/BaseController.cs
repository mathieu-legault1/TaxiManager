using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Xml;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using TaxiManager.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;

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

        public void SetTaxiInSession()
        {
            //for now so I can test things...
            List<ApplicationUser> taxiUsers = new List<ApplicationUser>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "Select * From AspNetUsers WHERE [Discriminator] = N'ApplicationUser'";
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        using (SqlCommand commandGetRole = new SqlCommand())
                        {
                            commandGetRole.Connection = connection;
                            commandGetRole.CommandText = "Select * From aspNetUserRoles Where [UserId] = @UserId and [RoleId] = N'5644ca88-4f79-4386-b69e-ec0774202aad'";
                            commandGetRole.Parameters.AddWithValue("@UserId", (string)reader["Id"]);
                            SqlDataReader results = commandGetRole.ExecuteReader();
                            if (results.HasRows)
                            {
                                // c'est un taxi
                                ApplicationUser user = new ApplicationUser();
                                user.Adress = reader["Adress"].ToString();
                                user.CurrentAdress = reader["CurrentAdress"].ToString();
                                user.CurrentStatus = (Status)reader["CurrentStatus"];
                                user.Id = reader["Id"].ToString();
                                user.UserName = reader["UserName"].ToString();
                                var requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false", Uri.EscapeDataString(user.Adress));

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
                                user.Latitude = lat;
                                user.Longitude = lng;
                                taxiUsers.Add(user);
                            }
                        }
                    }
                }
                connection.Close();
                Session["Taxis"] = JsonConvert.SerializeObject(taxiUsers);
            }
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