using Microsoft.AspNet.Identity.EntityFramework;
using PRN231.TicketBooking.BusinessObject.Models;

namespace PRN231.TicketBooking.Common.Dto.Response
{
    public class AccountResponse
    {
        public Account User { get; set; }
        public IEnumerable<IdentityRole> Role { get; set; } = new List<IdentityRole>();
    }
}