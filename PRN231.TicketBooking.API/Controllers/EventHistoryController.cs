using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231.TicketBooking.BusinessObject.Enum;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Service.Contract;

namespace PRN231.TicketBooking.API.Controllers
{
    [Route("event-history")]
    [ApiController]
    public class EventHistoryController : ControllerBase
    {
        private ISponsorEventHistoryService _sponsorEventHistory;

        public EventHistoryController(ISponsorEventHistoryService sponsorEventHistoryService)
        {
            _sponsorEventHistory = sponsorEventHistoryService;  
        }

        [HttpGet("get-sponsor-history-by-id/{id}")]
        public async Task<AppActionResult> GetSponsorHistoryById(Guid id)
        {
            return await _sponsorEventHistory.GetSponsorHistoryById(id);        
        }

        [HttpGet("get-sponsor-history-by-sponsor-id/{sponsorId}")]
        public async Task<AppActionResult> GetSponsorHistoryBySponsorId(Guid sponsorId, int pageNumber = 1, int pageSize = 10)
        {
            return await _sponsorEventHistory.GetSponsorHistoryBySponsorId(sponsorId, pageNumber, pageSize);
        }

        [HttpGet("get-sponsor-history-by-type")]
        public async Task<AppActionResult> GetSponsorHistoryByType(SponsorType sponsorType, int pageNumber = 1, int pageSize = 10)
        {
            return await _sponsorEventHistory.GetSponsorHistoryByType(sponsorType, pageNumber, pageSize);
        }

        [HttpGet("get-sponsor-history-of-event/{eventId}")]
        public async Task<AppActionResult> GetSponsorHistoryOfEvent(Guid eventId, int pageNumber = 1 , int pageSize = 10)
        {
            return await _sponsorEventHistory.GetSponsorHistoryOfEvent(eventId, pageNumber, pageSize);

        }
    }
}
