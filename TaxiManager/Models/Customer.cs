using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaxiManager.Models
{
    public enum Status
    {
        OnHold, // En attente de la confirmation d'un taxi
        Accepted, // Le client a été accepté par un taxi
        Denied, // Le client a été refusé par un taxi
        OnTheWay, // Le client est dans le taxi
        Arrived, // Le client a été reconduit.
    }

    public class Customer
    {
        public Customer()
        {
            Status = Status.OnHold;
        }

        [Key]
        public int CustomerID { get; set; }
        
        [Required]
        public Status Status { get; set; }

        [Required]
        public string Adress { get; set; }

        [Display(Name = "Prénom", Prompt = "...")]
        [StringLength(25, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Display(Name="Nom", Prompt="...")]
        [StringLength(25, MinimumLength = 3)]
        public string LastName { get; set; }
        
        public string FullName
        {
            get
            {
                return LastName + ", " + FirstName;
            }
        }
    }
}