using CCCSports.Data;
using CCCSports.Data.FacilityClasses;
using CCCSports.Web.Models.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CCCSports.Web.Controllers
{
    public class ReportsController : BaseController
    {
        // GET: ReportsIndex
        /// <summary>
        /// Returns ReportsIndex view, where a report can be generated.
        /// </summary>
        /// <returns></returns>
        public ActionResult ReportsIndex()
        {
            return View();
        }


        /// <summary>
        /// Returns the BookingReport view as a PDF using Rotativa
        /// </summary>
        /// <returns></returns>
        public ActionResult BookingReport()
        {
            ReportsViewModel report = TodaysReport();
            return new Rotativa.ViewAsPdf("BookingsReport", report) { FileName = "BookingReport" + DateTime.Now.ToShortDateString() + ".pdf" };
        }


        /// <summary>
        /// Calculates all booking/membership counts and membership totals
        /// </summary>
        /// <returns></returns>
        public ReportsViewModel TodaysReport()
        {

            int hallBookings = 0;
            int roomBookings = 0;
            //int equipmentBookings = 0;

            int memberSignUps = 0;
            int memberAdultTakings = 0;
            int memberJuvTakings = 0;
            int memberFamTakings = 0;

            bool wasError = false;
            string errorText = "Error";

            List<HallActivityBooking> HallBookings = db.HallBookings.ToList();
            List<RoomBooking> RoomBookings = db.RoomBookings.ToList();
            List<ApplicationUser> MemberUsers = db.Users.Where(x => x.IsMember == true).ToList();

            foreach(HallActivityBooking h in HallBookings)
            {
                hallBookings = hallBookings + 1;
            }

            foreach (RoomBooking r in RoomBookings)
            {
                roomBookings = roomBookings + 1;
            }

            //Foreach loop for all member users. Calculates each member takings based on their price
            foreach (ApplicationUser u in MemberUsers)
            {
                memberSignUps = memberSignUps + 1;

                if (u.MemberType == "Adult")
                {
                    memberAdultTakings = memberSignUps * 30;
                }
                else if (u.MemberType =="Juvenile")
                {
                    memberJuvTakings = memberSignUps * 15;
                }
                else if (u.MemberType == "Family")
                {
                    memberFamTakings = memberSignUps * 75;
                }
                else
                {
                    wasError = true;
                    memberSignUps = memberSignUps + 0;
                    errorText = "Error finding member type";
                }
                    
            }

            //Returns all data for the report
            ReportsViewModel fullReport = new ReportsViewModel()
            {
                HallBookingsCount = hallBookings,
                RoomBookingsCount = roomBookings,
                MemberSignUpCount = memberSignUps,
                TotalBookings = hallBookings + roomBookings,

                TotalAdultTakings = memberAdultTakings,
                TotalJuvTakings = memberJuvTakings,
                TotalFamTakings = memberFamTakings,

                TotalMemberTakings = memberAdultTakings + memberJuvTakings + memberFamTakings

            };

            if (wasError == true)
            {
                fullReport.ErrorString = errorText;
                fullReport.WasError = wasError;
            }
            else
            {
                fullReport.ErrorString = "";
                fullReport.WasError = false;
            }

            return fullReport;
        }


    }
}