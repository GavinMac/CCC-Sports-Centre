using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCCSports.Data
{
    public class CentreMembership
    {
        [Key, ForeignKey("MembershipUser")]
        public string MemberId { get; set; }

        public string MemberName { get; set; }

        public string MemberType { get; set; }


        //Nav       
        public virtual ApplicationUser MembershipUser { get; set; }


    }
}
