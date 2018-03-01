using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCCSports.Data.FacilityClasses
{
    public class ActivityHall
    {
        [Key]
        public string FacilityName { get; set; }

        public bool IsBooked { get; set; }

        public bool BookCourt1 { get; set; }
        public string Court1Status { get; set; }

        public bool BookCourt2 { get; set; }
        public string Court2Status { get; set; }

        public bool BookCourt3 { get; set; }
        public string Court3Status { get; set; }

        public bool BooKCourt4 { get; set; }
        public string Court4Status { get; set; }

        public string HallStatus { get; set; }




    }
}
