using CCCSports.Data;
using CCCSports.Data.FacilityClasses;
using CCCSports.Web.Extensions;
using CCCSports.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CCCSports.Web.Controllers
{
    public class RoomBookingController : BaseController
    {
        // GET: RoomBooking
        public ActionResult MakeRoomBooking()
        {
            return View("~/Views/RoomBookings/MakeRoomBooking.cshtml");
        }


        /// <summary>
        /// Allows an activity booking to be created
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MakeRoomBooking(RoomBookingInputModel model)
        {
            var userId = User.Identity.GetUserId();
            var customerUser = db.Users.Find(userId);

            //Because the FunctionEndTime attribute is a TimeSpan and only takes in a time;
            //This sets a new end DateTime for the end date/time.
            DateTime bookingStart = model.FunctionDate + model.FunctionStartTime;
            DateTime bookingEnd = model.FunctionDate + model.FunctionEndTime;

            if (model != null && ModelState.IsValid)
            {
                ViewBag.Message = "Make Booking";

                var newBooking = new RoomBooking()
                {                                     
                    CustomerUser = customerUser,
                    CustomerName = customerUser.FirstName + " " + customerUser.LastName,
                    FunctionName = model.FunctionName,
                    FunctionDescription = model.FunctionDescription,
                    BookingDate = model.FunctionDate,
                    StartTime = bookingStart,
                    EndTime = bookingEnd,                   
                                     
                };


                //Validation

                if (ValidateBooking(newBooking) == true)
                {
                    //Display notification message and redirect
                    this.AddNotification("Error: There is already a booking in the selected courts and time slot", NotificationType.ERROR);
                    return RedirectToAction("MakeRoomBooking", "RoomBooking");
                }
                else
                {
                    db.RoomBookings.Add(newBooking);

                    db.SaveChanges();

                    //Display notification message "Item Created"
                    this.AddNotification("You're booking has been made! You receive an email shortly", NotificationType.INFO);
                    return RedirectToAction("FunctionRoomDetails", "Facilities");
                }

            }

            return View("~/Views/RoomBookings/MakeRoomBooking.cshtml", model);
        }


        /// <summary>
        /// Deletes the selected booking
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DeleteRoomBooking(int id, RoomBookingInputModel model)
        {
            var bookingToDelete = LoadBooking(id);

            if (bookingToDelete == null)
            {
                this.AddNotification("Cannot delete booking #" + id + " it does not exist.", NotificationType.ERROR);
                return RedirectToAction("Bookings", "Bookings");
            }

            db.RoomBookings.Remove(bookingToDelete);
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
        public ActionResult EditRoomBooking(int id)
        {

            var userId = User.Identity.GetUserId();
            var customerUser = db.Users.Find(userId);

            var itemToEdit = this.LoadBooking(id);

            if (itemToEdit == null)
            {
                this.AddNotification("Cannot Edit booking #" + id, NotificationType.ERROR);
                return RedirectToAction("Bookings", "Bookings");
            }

            //Initialise model
            var model = new RoomBookingInputModel()
            {
                CustomerUser = itemToEdit.CustomerUser,
                FunctionName = itemToEdit.FunctionName,
                FunctionDescription = itemToEdit.FunctionDescription,
                FunctionDate = itemToEdit.BookingDate,
                FunctionStartTime = itemToEdit.StartTime.Subtract(itemToEdit.BookingDate),
                FunctionEndTime = itemToEdit.EndTime.Subtract(itemToEdit.BookingDate),              
            };

            return View("~/Views/RoomBookings/EditRoomBooking.cshtml", model);
        }

        /// <summary>
        /// Allows the slected post to be edited, saves the booking with new data to database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditRoomBooking(int id, RoomBookingInputModel model)
        {
            var itemToEdit = LoadBooking(id);

            var userId = User.Identity.GetUserId();
            var customerUser = db.Users.Find(userId);

            //Because the Booking Start/End Time attribute is a TimeSpan and only takes in a time;
            //This sets a new end DateTime for the end date/time so it can be read on the calender
            DateTime bookingStart = model.FunctionDate + model.FunctionStartTime;
            DateTime bookingEnd = model.FunctionDate + model.FunctionEndTime;

            if (itemToEdit == null)
            {
                this.AddNotification("Cannot edit item #" + id, NotificationType.ERROR);
                return RedirectToAction("Bookings", "Bookings");
            }

            if (model != null && ModelState.IsValid)
            {
                itemToEdit.FunctionName = model.FunctionName;
                itemToEdit.BookingDate = model.FunctionDate;
                itemToEdit.StartTime = bookingStart;
                itemToEdit.EndTime = bookingEnd;

                //Validation

                if (ValidateBooking(itemToEdit) == true)
                {
                    //Display notification message and redirect
                    this.AddNotification("Error: There is already a booking in the selected time slot", NotificationType.ERROR);
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

            return View("~/Views/RoomBookings/EditRoomBooking.cshtml", model);
        }





        /// <summary>
        /// Booking validation to prevent bookings made at the same time and in the same courts
        /// </summary>
        /// <param name="model"></param>
        /// <param name="newBooking"></param>
        /// <returns></returns>
        public bool ValidateBooking(RoomBooking newBooking)
        {
            //LINQ Expression to find if occupied
            var currentBooking = db.RoomBookings
                .Where(x => (x.BookingDate == newBooking.BookingDate))
                       .Select(b => (b.StartTime <= newBooking.StartTime && b.EndTime >= newBooking.EndTime) ||
                             (b.StartTime < newBooking.StartTime && b.EndTime >= newBooking.EndTime) ||
                             (newBooking.StartTime <= b.StartTime && newBooking.EndTime >= b.EndTime)
                       )
                       .FirstOrDefault();

            return currentBooking;
        }



        /// <summary>
        /// Get's the item to be used in other methods
        /// </summary>
        /// <param name="id">Item ID</param>
        /// <returns>Target Item</returns>
        private RoomBooking LoadBooking(int id)
        {
            var userId = User.Identity.GetUserId();
            var currentUser = db.Users.Find(userId);

            var bookingToEdit = db.RoomBookings.Where(x => x.BookingId == id).FirstOrDefault(x => x.CustomerUser.Id == currentUser.Id);

            return bookingToEdit;
        }


    }
}