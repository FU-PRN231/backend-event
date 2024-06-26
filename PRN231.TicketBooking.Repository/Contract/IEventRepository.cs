using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Common.Dto.Response;

namespace PRN231.TicketBooking.Repository.Contract
{
    public interface IEventRepository : IRepository<Event>
    {
        public Task<PagedResult<Event>> GetEvents(int pageNumber, int pageSize);
        public Task<PagedResult<Event>> GetEventsByOrganizationId(Guid organizationId, int pageNumber, int pageSize);
        public Task<PagedResult<Event>> GetEventsWithStatus(Guid? organizationId, DateTime today, int happened, int pageNumber, int pageSize);
        public Task<int[]> CountingEventsWithStatus(Guid? organizationId, DateTime today);
		public Task<Event> GetEventById(Guid id);

        public Task<AppActionResult> AddEvent(Event eventEntity);
        public Task<AppActionResult> UpdateEvent(Event eventEntity);
        public Task<List<StaticFile>> GetStaticFilesByEventId(Guid eventId);
        Task<PagedResult<Event>> GetAvailableEvents(DateTime today, int pageNumber, int pageSize);
        public Task<AppActionResult> GetEventReport(Guid? EventId,int timePeriod);
    }
}