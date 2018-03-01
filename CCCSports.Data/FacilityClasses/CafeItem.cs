using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCCSports.Data.FacilitiesClasses
{
    public class CafeItem
    {
        [Key]
        public int ItemId { get; set; }

        [Required]
        public string ItemName { get; set; }

        [Required]
        public int ItemStock { get; set; }

        [Required]
        public double ItemCost { get; set; }


    }
}
