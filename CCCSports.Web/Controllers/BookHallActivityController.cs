using CCCSports.Data;
using CCCSports.Data.FacilityClasses;
using CCCSports.Web.Extensions;
using CCCSports.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace CCCSports.Web.Controllers
{
    public class BookHallActivityController : BaseController
    {
        // GET: BookHallActivity
        public ActionResult MakeActivityBooking()
        {
            return View("~/Views/HallBookings/MakeActivityBooking.cshtml");
        }

        /// <summary>
        /// Allows an activity booking to be created
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MakeActivityBooking(HallActivityBookingInputModel model, ApplicationDbContext context)
        {
            var userId = User.Identity.GetUserId();
            var customerUser = db.Users.Find(userId);

            //Because the Booking Start/End Time attribute is a TimeSpan and only takes in a time;
            //This sets a new end DateTime for the end date/time so it can be read on the calender
            DateTime bookingStart = model.BookingDate + model.BookingStartTime;
            DateTime bookingEnd = model.BookingDate + model.BookingEndTime;

            if (model != null && ModelState.IsValid)
            {
                ViewBag.Message = "Make Booking";

                var newBooking = new HallActivityBooking()
                {
                    CustomerUser = customerUser,
                    CustomerName= customerUser.FirstName + " " + customerUser.LastName,
                    BookingDate = model.BookingDate,
                    BookingStartTime = bookingStart,
                    BookingEndTime = bookingEnd,
                    Activity = model.Activity,
                    BookCourt1 = model.BookCourt1,
                    BookCourt2 = model.BookCourt2,
                    BookCourt3 = model.BookCourt3,
                    BookCourt4 = model.BookCourt4,                               
                };


                //Validation

                if (ValidateBooking(newBooking) == true)
                {
                    //Display notification message and redirect
                    this.AddNotification("Error: There is already a booking in the selected courts and time slot", NotificationType.ERROR);
                    return RedirectToAction("MakeActivityBooking", "BookHallActivity");
                }
                else
                {
                    //Add booking to database if the booking slot is available
                    db.HallBookings.Add(newBooking);
                    db.SaveChanges();

                    //Display notification message and redirect
                    this.AddNotification("You're booking has been made! You receive an email shortly", NotificationType.INFO);
                    return RedirectToAction("ActivityHallDetails", "Facilities");
                }


            }

            return View("~/Views/HallBookings/MakeActivityBooking.cshtml", model);
        }


        /// <summary>
        /// Booking validation to prevent bookings made at the same time and in the same courts
        /// </summary>
        /// <param name="model"></param>
        /// <param name="newBooking"></param>
        /// <returns></returns>
        public bool ValidateBooking(HallActivityBooking newBooking)
            {
            //LINQ Expression to find if occupied
            var currentBooking = db.HallBookings
                .Where(x => (x.BookCourt1 == newBooking.BookCourt1) || (x.BookCourt2 == newBooking.BookCourt2) || (x.BookCourt3 == newBooking.BookCourt3) || (x.BookCourt4 == newBooking.BookCourt4))
                       .Select(b => (b.BookingStartTime <= newBooking.BookingStartTime && b.BookingEndTime >= newBooking.BookingEndTime) ||
                             (b.BookingStartTime < newBooking.BookingStartTime && b.BookingEndTime >= newBooking.BookingEndTime) ||
                             (newBooking.BookingStartTime <= b.BookingStartTime && newBooking.BookingEndTime >= b.BookingEndTime)
                       )
                       .FirstOrDefault();


            return currentBooking;
        }



        /// <summary>
        /// Deletes the selected booking
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteHallBooking(int id, HallActivityBookingInputModel model)
        {           
            var itemToDelete = LoadBooking(id);

            if (itemToDelete == null)
            {
                this.AddNotification("Cannot delete booking #" + id + " it does not exist.", NotificationType.ERROR);
                return RedirectToAction("Bookings", "Bookings");
            }

            db.HallBookings.Remove(itemToDelete);
            db.SaveChanges();
            this.AddNotification("Booking #" + id + " canceled.", NotificationType.INFO);
            return RedirectToAction("Bookings", "Bookings");

        }



        /// <summary>
        /// GET edit item and some validation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EditHallBooking(int id)
        {
            var itemToEdit = this.LoadBooking(id);

            if (itemToEdit == null)
            {
                this.AddNotification("Cannot Edit item #" + id, NotificationType.ERROR);
                return RedirectToAction("Bookings", "Bookings");
            }

            //Initialise model with the loaded hall booking
            var model = new HallActivityBookingInputModel()
            {
                CustomerUser = itemToEdit.CustomerUser,             
                Activity = itemToEdit.Activity,
                BookingDate = itemToEdit.BookingDate,
                BookingStartTime = itemToEdit.BookingStartTime.Subtract(itemToEdit.BookingDate),
                BookingEndTime = itemToEdit.BookingEndTime.Subtract(itemToEdit.BookingDate),
                BookCourt1 = itemToEdit.BookCourt1,
                BookCourt2 = itemToEdit.BookCourt2,
                BookCourt3 = itemToEdit.BookCourt3,
                BookCourt4 = itemToEdit.BookCourt4,
            };

            return View("~/Views/HallBookings/EditHallBooking.cshtml",model);
        }

        /// <summary>
        /// Allows the slected hall booking to be edited
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditHallBooking(int id, HallActivityBookingInputModel model)
        {
            var itemToEdit = LoadBooking(id);

            //Because the Booking Start/End Time attribute is a TimeSpan and only takes in a time;
            //This sets a new end DateTime for the end date/time so it can be read on the calender
            DateTime bookingStart = model.BookingDate + model.BookingStartTime;
            DateTime bookingEnd = model.BookingDate + model.BookingEndTime;

            if (itemToEdit == null)
            {
                this.AddNotification("Cannot edit item #" + id, NotificationType.ERROR);
                return RedirectToAction("Bookings", "Bookings");
            }

            if (model != null && ModelState.IsValid)
            {
                itemToEdit.Activity = model.Activity;
                itemToEdit.BookingDate = model.BookingDate;
                itemToEdit.BookingStartTime = bookingStart;
                itemToEdit.BookingEndTime = bookingEnd;
                itemToEdit.BookCourt1 = model.BookCourt1;
                itemToEdit.BookCourt2 = model.BookCourt2;
                itemToEdit.BookCourt3 = model.BookCourt3;
                itemToEdit.BookCourt4 = model.BookCourt4;

                //Validation
                if (ValidateBooking(itemToEdit) == true)
                {
                    //Display notification message and redirect
                    this.AddNotification("Error: There is already a booking in the selected courts and time slot", NotificationType.ERROR);
                    return RedirectToAction("Bookings", "Bookings");
                }
                else
                {
                //Update booking, display message and redirect
                db.SaveChanges();

                this.AddNotification("Changes saved.", NotificationType.INFO);
                return RedirectToAction("Bookings", "Bookings");
                }

            }

            return View("~/Views/HallBookings/EditHallBooking.cshtml", model);
        }



        /// <summary>
        /// Get's the item to be used in other methods
        /// </summary>
        /// <param name="id">Item ID</param>
        /// <returns>Target Item</returns>
        private HallActivityBooking LoadBooking(int id)
        {
            var userId = User.Identity.GetUserId();
            var currentUser = db.Users.Find(userId);

            var itemToEdit = db.HallBookings.Where(x => x.ActivityBookingId == id).FirstOrDefault(x => x.CustomerUser.Id == currentUser.Id);

            return itemToEdit;
        }

    }
}