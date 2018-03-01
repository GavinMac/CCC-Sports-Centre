using CCCSports.Data;
using CCCSports.Data.FacilityClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CCCSports.Web.Models
{
    public class AllBookingsViewModel
    {

        public IQueryable<HallActivityBooking> HallBookingsList { get; set; }

        public IQueryable<RoomBooking> RoomBookingsList { get; set; }


        


    }
}