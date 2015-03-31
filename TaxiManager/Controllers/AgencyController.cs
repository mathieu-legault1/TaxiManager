using System.Web.Mvc;
using TaxiManager.Models;

namespace TaxiManager.Controllers
{
    //[Authorize(Roles = "Agency")]
    public class AgencyController : BaseController
    {
        // GET: Agency
        public ActionResult Index()
        {
            SetCustomersInSession();
            SetTaxiInSession();
            return View();
        }

        [HttpPost]
        public ActionResult NewCustomer([Bind(Include = "CustomerID, Adress, FirstName, LastName")] Customer model)
        {            
            if (ModelState.IsValid)
            {
                db.Customers.Add(model);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View("Index");
        }
    }
}