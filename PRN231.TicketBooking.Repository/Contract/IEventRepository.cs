using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Common.Dto.Response;

namespace PRN231.TicketBooking.Repository.Contract
{
    public interface IEventRepository : IRepository<Event>
    {
        public Task<PagedResult<Event>> GetEvents(int pageNumber, int pageSize);
        public Task<Event> GetEventById(Guid id);

        public Task<AppActionResult> AddEvent(Event eventEntity);
    }
}