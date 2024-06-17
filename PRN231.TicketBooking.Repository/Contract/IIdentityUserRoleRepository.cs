using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Repository.Contract
{
    public interface IIdentityUserRoleRepository : IRepository<IdentityUserRole<string>>
    {
        public Task<bool> AssignRole(string userId, string roleId);
        public Task<List<string>> GetRoleListByAccountId(string userId);
	}
}
