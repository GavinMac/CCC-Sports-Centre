using CCCSports.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CCCSports.Web.Models
{
    public class ProductInputModel
    {

        [Required]
        [Display(Name ="Product Name")]
        [StringLength(50, ErrorMessage = "Name name too long. Must be less than 50 characters")]
        public string InputProductName { get; set; }

        [Required]
        [Display(Name = "Product Description")]
        [StringLength(500, ErrorMessage = "Description too long, must be less than 500 characters")]
        public string InputProductDesc { get; set; }

        [Required]
        [Display(Name = "Amount in Stock")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int InputProductStock { get; set; }

        [Required]
        [Display(Name = "Product Cost £")]
        [Range(0.01, int.MaxValue, ErrorMessage = "Please enter a value bigger than £0.01")]
        public double InputProductCost { get; set; }


        public static ProductInputModel CreateFromProduct(Product e)
        {
            return new ProductInputModel()
            {
                InputProductName = e.ProductName,
                InputProductDesc = e.ProductDesc,
                InputProductStock = e.ProductStock,
                InputProductCost = e.ProductCost
            };
        }


    }
}