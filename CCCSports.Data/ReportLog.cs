using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCCSports.Data
{
    public class ReportLog
    {
        [Key]
        public string ReportName { get; set; }

        public int HallBookingsCount { get; set; }

        public int RoomBookingsCount { get; set; }

        public int EquipmentBookingsCount { get; set; }

        public int MemberSignUpCount { get; set; }

        public int TotalBookings { get; set; }


        public int TotalAdultTakings { get; set; }
        public int TotalJuvTakings { get; set; }
        public int TotalFamTakings { get; set; }
        public int TotalMemberTakings { get; set; }

    }
}
