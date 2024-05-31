using Microsoft.AspNetCore.Identity;

namespace PRN231.TicketBooking.Repository.Contract
{
    public interface IIdentityRoleRepository: IRepository<IdentityRole>
    {
        public Task<IdentityRole> GetIdentityRoleByName(string name);
    }
}