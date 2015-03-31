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
        Offline, // Le taxi n'est pas disponible.
        OnHold, // En attente de la confirmation d'un taxi
        Accepted, // Le client a été accepté par un taxi
        GetClient, // Le taxi est en train d'aller chercher le client
        Denied, // Le client a été refusé par un taxi
        pickUp, // Le taxi est arrivé au client
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
        
        //[Required]
        public Status Status { get; set; }

        [Required(ErrorMessage="L'adresse est obligatoire")]
        [Display(Name="Adresse")]
        public string Adress { get; set; }

        [Display(Name = "Prénom")]
        [StringLength(25, MinimumLength = 3, ErrorMessage="Le prénom ne doit pas dépasser 25 caractères et doit avoir au minimum 3 caractères ")]
        public string FirstName { get; set; }

        [Display(Name="Nom")]
        [StringLength(25, MinimumLength = 3, ErrorMessage="Le nom ne doit pas dépasser 25 caractères et doit avoir au minimum 3 caractères ")]
        public string LastName { get; set; }

        public string Latitude { get; set; }
        public string Longitude { get; set; }
        
        public string FullName
        {
            get
            {
                return LastName + ", " + FirstName;
            }
        }
    }

    public class CustomerViewModel
    {
        public Customer Customer { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
    }
}