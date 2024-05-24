using Microsoft.AspNetCore.Identity;

namespace PRN231.TicketBooking.Repository.Contract
{
    public interface IIdentityRoleRepository
    {
        public Task<IdentityRole> GetIdentityRoleByName(string name);
    }
}