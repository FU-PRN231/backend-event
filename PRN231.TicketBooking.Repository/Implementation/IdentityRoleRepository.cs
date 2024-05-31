using Microsoft.AspNetCore.Identity;
using PRN231.TicketBooking.Common.Util;
using PRN231.TicketBooking.DAO.dao;
using PRN231.TicketBooking.Repository.Contract;

namespace PRN231.TicketBooking.Repository.Implementation
{
    public class IdentityRoleRepository : GenericRepository<IdentityRole>, IIdentityRoleRepository
    {

        public IdentityRoleRepository(IGenericDAO<IdentityRole> dao, IServiceProvider serviceProvider) : base(dao, serviceProvider)
        {
        }
        public async Task<Microsoft.AspNetCore.Identity.IdentityRole> GetIdentityRoleByName(string name)
        {
            return await this.GetByExpression(i => i.NormalizedName.Equals(name.ToLower()));
        }

    }
}