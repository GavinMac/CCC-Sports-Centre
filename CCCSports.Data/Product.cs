using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCCSports.Data
{
    public class Product
    {

        [Key]
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public string ProductDesc { get; set; }

        [Required]
        public int ProductStock { get; set; }

        [Required]
        public double ProductCost { get; set; }

        public int ProductRating { get; set; }


    }
}
