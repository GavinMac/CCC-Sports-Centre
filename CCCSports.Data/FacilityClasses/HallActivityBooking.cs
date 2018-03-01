using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCCSports.Data.FacilityClasses
{
    public class HallActivityBooking
    {
        [Key]
        public int ActivityBookingId { get; set; }

        public ApplicationUser CustomerUser { get; set; }

        public string CustomerName { get; set; }

        public string Activity { get; set; }

        public DateTime BookingDate { get; set; }

        public DateTime BookingEndTime { get; set; }
        public DateTime BookingStartTime { get; set; }

        public bool BookCourt1 { get; set; }
        public bool BookCourt2 { get; set; }
        public bool BookCourt3 { get; set; }
        public bool BookCourt4 { get; set; }


    }
}
