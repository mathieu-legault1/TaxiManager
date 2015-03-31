using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
            //for now so I can test things...
            List<ApplicationUser> taxiUsers = new List<ApplicationUser>();
            using(SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "Select * From AspNetUsers WHERE [Discriminator] = N'ApplicationUser'";
                    SqlDataReader reader = command.ExecuteReader();
                    while(reader.Read())
                    {
                        using(SqlCommand commandGetRole = new SqlCommand())
                        {
                            commandGetRole.Connection = connection;
                            commandGetRole.CommandText = "Select * From aspNetUserRoles Where [UserId] = @UserId and [RoleId] = N'5644ca88-4f79-4386-b69e-ec0774202aad'";
                            commandGetRole.Parameters.AddWithValue("@UserId", (string)reader["Id"]);
                            SqlDataReader results = commandGetRole.ExecuteReader();
                            if(results.HasRows)
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
            /*
            var taxis = db.Users.ToList();
            foreach (var taxi in taxis)
            {
                if (taxi.Roles.Where(item => item.Role.Name != "Taxi").ToList().Count == 0)
                    continue;
                try
                {
                    // Get latitude and longitude for all current customers
                    // We need these to call the API
                    var requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false", Uri.EscapeDataString(taxi.Adress));

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
            Session["Taxis"] = JsonConvert.SerializeObject(taxis);*/
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
