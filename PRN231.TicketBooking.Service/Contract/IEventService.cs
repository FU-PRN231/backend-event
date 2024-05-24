using PRN231.TicketBooking.Common.Dto;

namespace PRN231.TicketBooking.Service.Contract
{
    public interface IEventService
    {
        public Task<AppActionResult> GetAllEvent();
    }
}