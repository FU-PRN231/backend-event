using PRN231.TicketBooking.BusinessObject.Enum;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;

namespace PRN231.TicketBooking.Service.Contract
{
    public interface IEventService
    {
        public Task<AppActionResult> GetAllEvent(int pageNumber, int pageSize);        
        public Task<AppActionResult> GetAvailableEvent(int pageNumber, int pageSize);
        public Task<AppActionResult> GetAllEventByOrganizationId(Guid organizationId, int pageNumber, int pageSize);
        public Task<AppActionResult> GetEventById(Guid id);
        public Task<AppActionResult> GetAllEventByStatus(EventCensorStatus status, int pageNumber, int pageSize);
        public Task<AppActionResult> AddEvent(CreateEventRequest dto);
        public Task<AppActionResult> UpdateEvent(Guid id, UpdateEventRequest request);
        public Task<AppActionResult> GetEventByStatus(Guid? organizationId, int happened, int pageNumber, int pageSize);
        public Task<AppActionResult> CountingEventByStatus(Guid? organizationId);
        public Task<AppActionResult> UpdateEventStatus(Guid eventId, EventCensorStatus status);
        public Task<AppActionResult> GetAllEventBySponsorId(Guid sponsorId, int pageNumber, int pageSize);
    }
}