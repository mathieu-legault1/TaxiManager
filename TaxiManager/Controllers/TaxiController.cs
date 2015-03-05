using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaxiManager.Controllers
{
    [Authorize(Roles="Taxi")]
    public class TaxiController : Controller
    {
        // GET: Taxi
        public ActionResult Index()
        {
            return View();
        }
    }
}