using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCCSports.Data
{
    public class Equipment
    {

        [Key]
        public int EquipmentId { get; set; }

        public string EquipmentName { get; set; }

        public int AmountInStock { get; set; }

        public string EquipmentCondition { get; set; }

        public double EquipmentCost { get; set; }

    }
}
