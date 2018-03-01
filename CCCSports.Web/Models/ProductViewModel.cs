using CCCSports.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace CCCSports.Web.Models
{
    public class ProductViewModel
    {

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public int ProductRating { get; set; }
        public double ProductCost { get; set; }
        public int ProductStock { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }

        public static Expression<Func<Product, ProductViewModel>> ViewModel
        {
            get
            {
                return e => new ProductViewModel()
                {
                    ProductId = e.ProductId,
                    ProductName = e.ProductName,
                    ProductDesc = e.ProductDesc,
                    ProductCost = e.ProductCost,
                    ProductRating = e.ProductRating,
                    ProductStock = e.ProductStock
                };
            }

        }


    }
}