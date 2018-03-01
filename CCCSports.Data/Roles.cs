using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCCSports.Data
{
    /// <summary>
    /// Role names
    /// </summary>
    public class Roles
    {
        public const string ROLE_ADMIN = "Admin"; //Admin has full privelages

        public const string ROLE_MANAGER = "Manager"; //The manager has manager privelages

        public const string ROLE_CLERK = "BookingsClerk"; //Clerks have access to everything a bookings clerk should

        public const string ROLE_MCLERK = "MembershipClerk"; //Clerks have access to everything a membership clerk should

        public const string ROLE_STAFF = "Staff"; //This is for cafe staff and any other staff

        public const string ROLE_MEMBER = "Member"; //When a user makes a membership they have member privelages

        public const string ROLE_USER = "User"; //User has only basic privelages
    }
}
