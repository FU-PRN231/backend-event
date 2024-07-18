using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Repository.Contract
{
    public interface ISeatRankRepository : IRepository<SeatRank>
    {
        public Task<SeatRank> GetSeatRankById(Guid id);

        public Task<PagedResult<SeatRank>> GetSeatRanks(int pageNumber, int pageSize);
        public Task<AppActionResult> AddSeatRankFromEvent(SeatRank seatRank);
        public Task<SeatRank> UpdateSeatRank(SeatRank seatRank);
        public Task<PagedResult<SeatRank>> GetAllSeatRank(Expression<Func<SeatRank, bool>>? filter, int pageNumber, int pageSize, Expression<Func<SeatRank, object>>? orderBy = null, bool isAscending = true, params Expression<Func<SeatRank, object>>[]? includes);
    }
}
