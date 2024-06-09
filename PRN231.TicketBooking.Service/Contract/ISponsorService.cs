using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;

namespace PRN231.TicketBooking.Service.Contract
{
    public interface ISponsorService
    {
        public Task<AppActionResult> AddSponsorToEvent(CreateSponsorDto dto);
        public Task<AppActionResult> GetAttendeeInformation(string qr);
        public Task<AppActionResult> GetAllSponsor(int pageNumber, int pageSize);
        public Task<AppActionResult> AddSponsorMoneyToEvent(AddSponsorMoneyDto dto);
        public Task<AppActionResult> GetAllSponsorItemOfAnEvent(Guid eventId, int pageNumber, int pageSize);
        public Task<AppActionResult> GetSponsorHistoryByEventId(Guid eventId, int pageNumber, int pageSize);
    }
}