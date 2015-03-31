using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaxiManager.Models
{
    public class RoutesViewModel
    {
        public ApplicationUser Taxi { get; set; }
        public Route Route { get; set; }
        public Customer Customer { get; set; }
    }
}