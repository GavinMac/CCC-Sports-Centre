using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CCCSports.Web.Models
{
    public class UserDetailsInputModel
    {

        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Display(Name ="Surname")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Display(Name = "House number")]
        public string HouseNumber { get; set; }

        [Display(Name = "Street")]
        public string Street { get; set; }

        [Display(Name = "Town")]
        public string Town { get; set; }

        [Display(Name = "Post Code")]
        public string PostCode { get; set; }

    }
}