using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CCCSports.Web.Models.Reports
{
    public class ReportsViewModel
    {

        public int HallBookingsCount { get; set; }

        public int RoomBookingsCount { get; set; }

        public int EquipmentBookingsCount { get; set; }

        public int MemberSignUpCount { get; set; }

        public int TotalBookings { get; set; }


        public int TotalAdultTakings { get; set; }
        public int TotalJuvTakings { get; set; }
        public int TotalFamTakings { get; set; }
        public int TotalMemberTakings { get; set; }


        public string ErrorString { get; set; }
        public bool WasError { get; set; }


    }
}