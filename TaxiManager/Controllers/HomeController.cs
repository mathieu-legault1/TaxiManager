using System;
using System.Web.Mvc;
using Microsoft.Web.Mvc;

namespace TaxiManager.Controllers
{
    public class HomeController : Controller
    {        
        public ActionResult Index()
        {
            ViewBag.Title = "Taxi Manager";

            // The user is already connected.
            if (!HttpContext.User.Identity.IsAuthenticated) return View();
            if(User.IsInRole("Taxi"))
            {
                return this.RedirectToAction<TaxiController>(c => c.Index());
            }
            if(User.IsInRole("Agency"))
            {
                return this.RedirectToAction<AgencyController>(c => c.Index());
            }
            if(User.IsInRole("Admin"))
            {
                throw new NotImplementedException();
                // return this.RedirectToAction<AgencyController>(c => c.Index());
            }

            return View();
        }
    }
}