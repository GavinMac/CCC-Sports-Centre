using CCCSports.Data;
using CCCSports.Data.FacilityClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace CCCSports.Web.Models
{
    public class FacilityViewModel
    {

        public int FacilityId { get; set; }

        public string FacilityName { get; set; }

        public string FacilityStatus { get; set; }

        public bool CanBook { get; set; }

        public bool IsBooked { get; set; }


    }
}