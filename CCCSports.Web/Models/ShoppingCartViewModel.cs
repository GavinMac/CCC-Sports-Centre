using CCCSports.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CCCSports.Web.Models
{
    public class ShoppingCartViewModel
    {

        [Display(Name ="Items in Cart")]
        public int AmountInCart { get; set; }

        [Display(Name ="Cart Total")]
        public double TotalCost { get; set; }

        public IEnumerable<Product> Items { get; set; }

    }
}