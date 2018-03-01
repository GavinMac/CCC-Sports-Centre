using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCCSports.Data
{
    public class EquipmentHireCart
    {
        [Key]
        public int CartId { get; set; }

        public int AmountInCart { get; set; }
        public double TotalCost { get; set; }

        //Navigational properties
        public ICollection<Equipment> Items { get; set; }


        //[ForeignKey("UserId")]
        //public int UserId { get; set; }
        //public virtual ApplicationUser CartCustomer { get; set; }


        public EquipmentHireCart()
        {
            Items = new HashSet<Equipment>();
        }
              
    }
}
