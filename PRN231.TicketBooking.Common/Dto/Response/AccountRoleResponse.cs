using Microsoft.AspNetCore.Identity;
using PRN231.TicketBooking.BusinessObject.Models;

namespace PRN231.TicketBooking.Common.Dto.Response
{
    public class AccountRoleResponse
    {
        public Account Account { get; set; } = null!;
        public List<IdentityRole> IdentityRoles { get; set; } = null!;
    }
}