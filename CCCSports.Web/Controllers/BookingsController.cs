using CCCSports.Data;
using CCCSports.Data.FacilityClasses;
using CCCSports.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CCCSports.Web.Controllers
{
    public class BookingsController : BaseController
    {
        // GET: Bookings
        /// <summary>
        /// Gets all hall bookings FOR CURRENT USER adds them to all bookings hall list, gets all room bookings adds them to all bookings room list.
        /// </summary>
        /// <returns></returns>
        public ActionResult Bookings()
        {

            //Get current user
            var userId = User.Identity.GetUserId();
            var currentUser = db.Users.Find(userId);

            AllBookingsViewModel model = new AllBookingsViewModel();
            {
                model.HallBookingsList = db.HallBookings.Where(x => x.CustomerUser.Id == currentUser.Id);
                model.RoomBookingsList = db.RoomBookings.Where(x => x.CustomerUser.Id == currentUser.Id);
            };

            return View(model);
        }


        /// <summary>
        /// Does the same as Bookings method but returns all bookings. This is only used for staff roles
        /// </summary>
        /// <returns></returns>
        public ActionResult AllBookings()
        {

            AllBookingsViewModel model = new AllBookingsViewModel();
            {
                model.HallBookingsList = db.HallBookings;
                model.RoomBookingsList = db.RoomBookings;
            };

            return View(model);
        }




    }
}