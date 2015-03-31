using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TaxiManager.Models
{

    public class Taxi
    {
        public Taxi()
        {
            Status = Status.OnHold;
        }

        [Key]
        public int TaxiID { get; set; }

        //[Required]
        public Status Status { get; set; }

        [Required(ErrorMessage = "L'adresse courante du taxi")]
        [Display(Name = "Location")]
        public string currentLocation { get; set; }

        [Display(Name = "Prénom")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Le prénom ne doit pas dépasser 25 caractères et doit avoir au minimum 3 caractères ")]
        public string FirstName { get; set; }

        [Display(Name = "Nom")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Le nom ne doit pas dépasser 25 caractères et doit avoir au minimum 3 caractères ")]
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
}