using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto.Request;

namespace PRN231.TicketBooking.Repository.Contract
{
    public interface IAccountRepository : IRepository<Account>
    {
        //public Task<List<Account>> CreateSponsorAccount(CreateSponsorDto dto);

        public Task<Account> GetAccountByEmail(string email, bool? IsDeleted = false, bool? IsVerified = true);
    }
}