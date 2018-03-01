using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCCSports.Data
{
    public class EquipmentBooking
    {
        [Key]
        public int EquipmentBookingId { get; set; }

        public string EquipmentName { get; set; }

        public ApplicationUser CustomerUser { get; set; }

        public string CustomerName { get; set; }

        public DateTime HireStartTime { get; set; }
        public DateTime HireEndTime { get; set; }


        //Navigational properties
        public ICollection<Equipment> SelectedEquipment { get; set; }

        public EquipmentBooking()
        {
            SelectedEquipment = new List<Equipment>();

        }


    }
}
