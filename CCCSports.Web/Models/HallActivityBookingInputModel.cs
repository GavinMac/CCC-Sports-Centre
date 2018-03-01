using CCCSports.Data;
using MVCControlsToolkit.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CCCSports.Web.Models
{
    public class HallActivityBookingInputModel
    {
        public ApplicationUser CustomerUser { get; set; }

        [Display(Name ="Activity", Description="What activity are you doing?")]
        [StringLength(20,ErrorMessage ="Activity name too long")]
        [Required]
        public string Activity { get; set; }

        [Display(Name = "Booking Date")]
        [Required]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DateRange(SMinimum = "Today+1h", ErrorMessage ="You can't make a booking in the past.")]
        public DateTime BookingDate { get; set; }

        [Display(Name = "Start Time")]
        [Required]
        [DataType(DataType.Time)DisplayFormat(DataFormatString = "{0:g}", ApplyFormatInEditMode = true)]
        public TimeSpan BookingStartTime { get; set; }
        

        [Display(Name ="End Time")]
        [Required]
        [DataType(DataType.Time)DisplayFormat(DataFormatString = "{0:g}", ApplyFormatInEditMode = true)]
        public TimeSpan BookingEndTime { get; set; }

        [Required]
        [Display(Name ="Court 1")]
        public bool BookCourt1 { get; set; }

        [Required]
        [Display(Name = "Court 2")]
        public bool BookCourt2 { get; set; }

        [Required]
        [Display(Name = "Court 3")]

        public bool BookCourt3 { get; set; }

        [Required]
        [Display(Name = "Court 4")]
        public bool BookCourt4 { get; set; }

    }
}