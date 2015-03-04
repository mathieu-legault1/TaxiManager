using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaxiManager.Controllers
{
    [Authorize(Roles="Agency")]
    public class AgencyController : Controller
    {
        // GET: Agency
        public ActionResult Index()
        {
            return View();
        }
    }
}