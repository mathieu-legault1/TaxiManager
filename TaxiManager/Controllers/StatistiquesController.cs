using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaxiManager.Models;

namespace TaxiManager.Controllers
{
    public class StatistiquesController : BaseController
    {

        // GET: Statistiques
        public ActionResult Index()
        {
            var model = db.Routes.Select(x => new RoutesViewModel
            {
                Route = x,
                Taxi = db.Users.FirstOrDefault(u => u.Id == x.ApplicationUserID) ?? null
            }).ToList();

            return View(model);
        }

    }
}
