using CCCSports.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CCCSports.Web.Models
{
    public class MembershipInputModel
    {

        [Display(Name ="Is this going to be a family membership? (£75 per anum)")]
        public bool IsFamily { get; set; }


    }
}