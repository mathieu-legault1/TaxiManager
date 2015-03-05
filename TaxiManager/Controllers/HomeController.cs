using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Web.Mvc;
using TaxiManager.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Security;

namespace TaxiManager.Controllers
{
    public class HomeController : Controller
    {        
        public ActionResult Index()
        {
            // The user is already connected.
            if(HttpContext.User.Identity.IsAuthenticated)
            {
                if(User.IsInRole("Taxi"))
                {
                    return this.RedirectToAction<TaxiController>(c => c.Index());
                }
                else if(User.IsInRole("Agency"))
                {
                    return this.RedirectToAction<AgencyController>(c => c.Index());
                }
                else if(User.IsInRole("Admin"))
                {
                    throw new NotImplementedException();
                    // return this.RedirectToAction<AgencyController>(c => c.Index());
                }
            }

            return View();
        }
    }
}