using CCCSports.Data;
using MVCControlsToolkit.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CCCSports.Web.Models
{
    public class RoomBookingInputModel
    {

        public ApplicationUser CustomerUser { get; set; }

        [Required]
        [Display(Name="Name of your function/event")]
        [StringLength(20, ErrorMessage = "Function name too long")]
        public string FunctionName { get; set; }

        [Required]
        [Display(Name ="Function Description", Description ="A short description of what you are doing")]
        [StringLength(50, ErrorMessage = "Function description too long")]
        public string FunctionDescription { get; set; }

        [Display(Name = "Booking Date")]
        [Required]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DateRange(SMinimum = "Today+1h", ErrorMessage = "You can't make a booking in the past.")]
        public DateTime FunctionDate { get; set; }

        [Display(Name = "Function End Time")]
        [Required]
        [DataType(DataType.Time)DisplayFormat(DataFormatString = "{0:g}", ApplyFormatInEditMode = true)]
        public TimeSpan FunctionStartTime { get; set; }

        [Display(Name = "Function Start Time")]
        [Required]
        [DataType(DataType.Time)DisplayFormat(DataFormatString = "{0:g}", ApplyFormatInEditMode = true)]
        public TimeSpan FunctionEndTime { get; set; }





    }
}