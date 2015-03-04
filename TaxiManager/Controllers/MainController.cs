using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace TaxiManager.Controllers
{
    public class MainController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Views/Taxi/Index.cshtml");
        }
    }
}