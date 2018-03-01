using CCCSports.Data.FacilitiesClasses;
using System.ComponentModel.DataAnnotations;

namespace CCCSports.Web.Models
{
    public class CafeItemInputModel
    {

        [Required]
        [StringLength(50, ErrorMessage = "Name name too long")]
        [Display(Name = "Item Name")]
        public string InputItemName { get; set; }

        [Required]
        [Display(Name = "Item Cost £")]
        [Range(0.01, int.MaxValue, ErrorMessage = "Please enter a value bigger than £0.01")]
        public double InputItemCost { get; set; }

        [Required]
        [Display(Name = "Amount in Stock")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int InputItemStock { get; set; }

        public static CafeItemInputModel CreateFromProduct(CafeItem e)
        {
            return new CafeItemInputModel()
            {
                InputItemName = e.ItemName,
                InputItemCost = e.ItemCost,
                InputItemStock = e.ItemStock
            };
        }


    }
}