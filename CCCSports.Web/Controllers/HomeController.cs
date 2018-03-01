using CCCSports.Data;
using DayPilot.Web.Mvc;
using DayPilot.Web.Mvc.Enums;
using DayPilot.Web.Mvc.Events.Calendar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace CCCSports.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact Page";

            return View();
        }



        ///////////////////////////// DayPilot Calenders /////////////////////////////

        // THIS IS DAYPILOT LITE, UNFORTUNATLY A LOT OF FEATURES ARE DISABLED.
        // I was not able to use buttons to change the view types (day, week, month) i can only see day & week.
        // Moving bookings caused issues. But it still works as it should.
        // I should have used FullCalender.io this was much better, i started implementing but had no time.

        //Returns BookingCalendar View
        public ActionResult BookingCalander()
        {
            return View();
        }

        //Calls activty DayPilot class
        public ActionResult ActivityBackend()
        {
            return new Dpc().CallBack(this);
        }

        /// <summary>
        /// DayPilot calendar class for the Activity hall
        /// </summary>
        public class Dpc : DayPilotCalendar
        {
            ApplicationDbContext db = new ApplicationDbContext();

            protected override void OnInit(InitArgs e)
            {
                Update(CallBackUpdateType.Full);
            }

            //Resize event
            protected override void OnEventResize(EventResizeArgs e)
            {
                var toBeResized = (from ev in db.HallBookings where ev.ActivityBookingId.ToString() == (e.Id) select ev).First();
                toBeResized.BookingStartTime = e.NewStart;
                toBeResized.BookingEndTime = e.NewEnd;
                db.SaveChanges();
                Update();
            }

            //Move event
            protected override void OnEventMove(EventMoveArgs e)
            {
                var toBeResized = (from ev in db.HallBookings where ev.ActivityBookingId.ToString() == (e.Id) select ev).First();
                toBeResized.BookingStartTime = e.NewStart;
                toBeResized.BookingEndTime = e.NewEnd;
                db.SaveChanges();
                Update();
            }

            protected override void OnFinish()
            {

                if (UpdateType == CallBackUpdateType.None)
                {
                    return;
                }
          
                Events = from ev in db.HallBookings select ev;               

                DataIdField = "ActivityBookingId";
                DataTextField = "Activity";
                DataStartField = "BookingStartTime";
                DataEndField = "BookingEndTime";

                Update();

            }

            /// <summary>
            /// DayPilot button controls
            /// </summary>
            /// <param name="e"></param>
            protected override void OnCommand(CommandArgs e)
            {
                switch (e.Command)
                {
                    case "halcal-previous":
                        StartDate = StartDate.AddDays(-7);
                        Update(CallBackUpdateType.Full);
                        break;
                    case "halcal-today":
                        StartDate = DateTime.Today;
                        Update(CallBackUpdateType.Full);
                        break;
                    case "halcal-next":
                        StartDate = StartDate.AddDays(7);
                        Update(CallBackUpdateType.Full);
                        break;
                }
            }


        }

        //////////////////////////////////////////////////////////////////////////////////////////////


        //Returns Function room DayPilot calendar class
        public ActionResult RoomBackend()
        {
            return new Dpc2().CallBack(this);
        }

        /// <summary>
        /// DayPilot class for the Function Room calander
        /// </summary>
        public class Dpc2 : DayPilotCalendar
        {
            ApplicationDbContext db = new ApplicationDbContext();

            protected override void OnInit(InitArgs e)
            {

                Update(CallBackUpdateType.Full);
            }


            protected override void OnEventResize(EventResizeArgs e)
            {
                var toBeResized = (from ev in db.RoomBookings where ev.BookingId.ToString() == (e.Id) select ev).First();
                toBeResized.StartTime = e.NewStart;
                toBeResized.EndTime = e.NewEnd;
                db.SaveChanges();
                Update();
            }

            protected override void OnEventMove(EventMoveArgs e)
            {
                var toBeResized = (from ev in db.RoomBookings where ev.BookingId.ToString() == (e.Id) select ev).First();
                toBeResized.StartTime = e.NewStart;
                toBeResized.EndTime = e.NewEnd;
                db.SaveChanges();
                Update();
            }

            protected override void OnFinish()
            {
                if (UpdateType == CallBackUpdateType.None)
                {
                    return;
                }

                Events = from ev in db.RoomBookings select ev;

                DataIdField = "BookingId";
                DataTextField = "FunctionName";
                DataStartField = "StartTime";
                DataEndField = "EndTime";

                Update();

            }

            /// <summary>
            /// DayPilot button controls
            /// </summary>
            /// <param name="e"></param>
            protected override void OnCommand(CommandArgs e)
            {
                switch (e.Command)
                {
                    case "roomcal-previous":
                        StartDate = StartDate.AddDays(-7);
                        Update(CallBackUpdateType.Full);
                        break;
                    case "roomcal-today":
                        StartDate = DateTime.Today;
                        Update(CallBackUpdateType.Full);
                        break;
                    case "roomcal-next":
                        StartDate = StartDate.AddDays(7);
                        Update(CallBackUpdateType.Full);
                        break;

                }
            }

        } 


    }
}