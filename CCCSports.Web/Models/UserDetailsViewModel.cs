using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CCCSports.Web.Models
{
    public class UserDetailsViewModel
    {
        
        public string Id { get; set; }

        public IList<string> Roles { get; set; }
        public string Username { get; set; }
        public string InputUsername { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public int Age { get; set; }   
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }

        public string HouseNumber { get; set; }
        public string Street { get; set; }
        public string Town { get; set; }
        public string PostCode { get; set; }

        public bool IsMember { get; set; }
        public string MemberType { get; set; }




    }
}