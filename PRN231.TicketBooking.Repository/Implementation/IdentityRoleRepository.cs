using Microsoft.AspNetCore.Identity;
using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto.Response;
using PRN231.TicketBooking.DAO.Data;
using PRN231.TicketBooking.Repository.Contract;
using System.Linq.Expressions;

namespace PRN231.TicketBooking.Repository.Implementation
{
    public class IdentityRoleRepository : GenericRepository<IdentityRole>, IIdentityRoleRepository
    {
        public IdentityRoleRepository(BookingTicketDbContext context) : base(context)
        {
        }

        public async Task<Microsoft.AspNetCore.Identity.IdentityRole> GetIdentityRoleByName(string name)
        {
            return await this.GetByExpression(i => i.NormalizedName.Equals(name.ToLower()));    
        }
    }
}
