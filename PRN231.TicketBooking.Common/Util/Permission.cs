using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Common.Util
{
    public class Permission
    {
        public const string ADMIN = "ADMIN";
        public const string CUSTOMER = "CUSTOMER";
        public const string SPONSOR = "SPONSOR";
        public const string ORGANIZER = "ORGANIZER";
        public const string PM = "PM";

        public const string ORGANIZATION = $"{ADMIN}, {ORGANIZER}, {PM}";
        public const string FUNDINGMANAGEMENT = $"{ADMIN}, {ORGANIZER}, {PM}, {SPONSOR}";
        public const string REGISTERED = $"{ADMIN}, {ORGANIZER}, {PM}, {SPONSOR}, {CUSTOMER}";
    }
}



