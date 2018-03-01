using CCCSports.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CCCSports.Web.Models
{
    public class HireCartViewModel
    {
        [Key]
        public int CartId { get; set; }

        public ApplicationUser CartCustomer { get; set; }

        public int AmountInCart { get; set; }

        public double TotalCost { get; set; }


        public ICollection<Equipment> Items { get; set; }


    }
}