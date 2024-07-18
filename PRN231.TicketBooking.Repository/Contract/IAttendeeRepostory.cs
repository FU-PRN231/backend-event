using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Repository.Contract
{
    public interface IAttendeeRepostory : IRepository<Attendee>
    {
        public Task<PagedResult<Attendee>> GetAttendeeByEvent(Guid eventId);
        public Task<Attendee> CheckInAttendee(Attendee attendee);
        public Task<Attendee> GetAttendeeByAccountIdAndEventId(string accountId, Guid eventId);
        public Task<PagedResult<Attendee>> GetAllAttendee(Expression<Func<Attendee, bool>>? filter, int pageNumber, int pageSize, Expression<Func<Attendee, object>>? orderBy = null, bool isAscending = true, params Expression<Func<Attendee, object>>[]? includes);
    }
}
