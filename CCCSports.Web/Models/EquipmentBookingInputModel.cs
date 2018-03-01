using CCCSports.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CCCSports.Web.Models
{
    public class EquipmentBookingInputModel
    {

        public ApplicationUser CustomerUser { get; set; }

        [Required]
        [Display(Name="Equipment Required")]
        public string EquipmentName { get; set; }

        [Required]
        [Display(Name ="Amount Required")]
        public int AmountToBook { get; set; }


    }
}