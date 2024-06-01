using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;

namespace PRN231.TicketBooking.Service.Contract
{
    public interface IEventService
    {
        public Task<AppActionResult> GetAllEvent(int pageNumber, int pageSize);
        public Task<AppActionResult> GetEventById(Guid id);
        public Task<AppActionResult> AddEvent(CreateEventRequest dto);
    }
}