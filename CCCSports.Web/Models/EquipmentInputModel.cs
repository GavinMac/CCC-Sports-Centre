using CCCSports.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CCCSports.Web.Models
{
    public class EquipmentInputModel
    {

        [Required]
        [StringLength(20, ErrorMessage = "Name name too long")]
        [Display(Name ="Name")]
        public string EquipmentName { get; set; }
        
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        [Display(Name ="Amount in Stock")]
        public int AmountInStock { get; set; }

        [Required]
        [Display(Name ="Condition")]
        public string EquipmentCondition { get; set; }

        [Required]
        [Display(Name ="Cost")]
        [Range(0.01, int.MaxValue, ErrorMessage = "Please enter a value bigger than £0.01")]
        public double EquipmentCost { get; set; }

        public static EquipmentInputModel CreateFromEquipment(Equipment e)
        {
            return new EquipmentInputModel()
            {
                EquipmentName = e.EquipmentName,
                AmountInStock = e.AmountInStock,
                EquipmentCondition = e.EquipmentCondition,
                EquipmentCost = e.EquipmentCost
            };
        }


    }
}