using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCCSports.Data.FacilityClasses
{
    class HallClassBooking
    {
        [Key]
        public int BookingId { get; set; }

        public ApplicationUser CustomerUser { get; set; }

        public string CustomerName { get; set; }

        public string ClassName { get; set; }

        public DateTime ClassDate { get; set; }

        public DateTime ClassEndTime { get; set; }
        public DateTime BookingTime { get; set; }

    }
}
