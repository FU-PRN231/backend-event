using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto.Response;
using PRN231.TicketBooking.Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Repository.Contract
{
    public interface ISponsorMoneyHistoryRepository : IRepository<SponsorMoneyHistory>
    {
        public Task<List<SponsorMoneyHistory>> GetAllSponsorMoneyHistoryByEventId(Guid eventId);
        public Task<PagedResult<SponsorMoneyHistory>> GetAllSponsorMoneyHistory(Expression<Func<SponsorMoneyHistory, bool>>? filter, int pageNumber, int pageSize, Expression<Func<SponsorMoneyHistory, object>>? orderBy = null, bool isAscending = true, params Expression<Func<SponsorMoneyHistory, object>>[]? includes);
    }
}
