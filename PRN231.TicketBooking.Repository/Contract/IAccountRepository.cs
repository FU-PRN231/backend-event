using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Common.Dto.Response;
using System.Linq.Expressions;

namespace PRN231.TicketBooking.Repository.Contract
{
    public interface IAccountRepository : IRepository<Account>
    {
        //public Task<List<Account>> CreateSponsorAccount(CreateSponsorDto dto);

        public Task<Account> GetAccountByEmail(string email, bool? IsDeleted = false, bool? IsVerified = true);

        public Task<PagedResult<Account>> GetAllIAccount(Expression<Func<Account, bool>>? filter, int pageNumber, int pageSize, Expression<Func<Account, object>>? orderBy = null, bool isAscending = true, params Expression<Func<Account, object>>[]? includes);
    }
}