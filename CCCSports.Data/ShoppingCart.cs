using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCCSports.Data
{
    public class ShoppingCart
    {
        public int ShopingCartId { get; set; }
        public ApplicationUser CartCustomer { get; set; }
        public int AmountInCart { get; set; }
        public double TotalCost { get; set; }


        //Navigational properties
        public ICollection<Product> Items { get; set; }

        public ShoppingCart()
        {
            Items = new HashSet<Product>();
        }


    }
}
