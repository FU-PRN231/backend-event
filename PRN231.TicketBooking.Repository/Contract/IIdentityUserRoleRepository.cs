using Microsoft.AspNetCore.Identity;
using PRN231.TicketBooking.Common.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Repository.Contract
{
    public interface IIdentityUserRoleRepository : IRepository<IdentityUserRole<string>>
    {
        public Task<bool> AssignRole(string userId, string roleId);
        public Task<List<string>> GetRoleListByAccountId(string userId);

        public Task<PagedResult<IdentityUserRole<string>>> GetAllIdentityUserRole(Expression<Func<IdentityUserRole<string>, bool>>? filter, int pageNumber, int pageSize, Expression<Func<IdentityUserRole<string>, object>>? orderBy = null, bool isAscending = true, params Expression<Func<IdentityUserRole<string>, object>>[]? includes);

    }
}
