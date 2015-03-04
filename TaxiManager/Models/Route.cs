using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaxiManager.Models
{
    public class Route
    {
        public Route()
        {
            DepartureDate = DateTime.Now;
        }

        [Key]
        public int RouteID { get; set; }

        [DisplayName("Date d'arrivé du client")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<DateTime> ArrivalDate { get; set; }

        [DisplayName("Date de départ du taxi")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DepartureDate { get; set; }

        [DisplayName("Date de récupération du client")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<DateTime> RecoveryDate { get; set; }

        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}