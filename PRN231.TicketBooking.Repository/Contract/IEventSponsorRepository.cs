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
    public interface IEventSponsorRepository : IRepository<EventSponsor>
    {
        public Task<EventSponsor> AddEventSponsorFromEvent(EventSponsor eventSponsor);
        public Task<List<Event>> GetEventsBySponsorId(Guid sponsorId, int pageNumber, int pageSize);
        public Task<PagedResult<EventSponsor>> GetAllEventSponsor(Expression<Func<EventSponsor, bool>>? filter, int pageNumber, int pageSize, Expression<Func<EventSponsor, object>>? orderBy = null, bool isAscending = true, params Expression<Func<EventSponsor, object>>[]? includes);
    }
}
