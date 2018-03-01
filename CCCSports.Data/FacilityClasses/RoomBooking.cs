using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCCSports.Data.FacilityClasses
{
    public class RoomBooking
    {

        [Key]
        public int BookingId { get; set; }

        public ApplicationUser CustomerUser { get; set; }

        public string CustomerName { get; set; }

        public string FunctionName { get; set; }

        public string FunctionDescription { get; set; }

        public DateTime BookingDate { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }





    }
}
