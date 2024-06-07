using Microsoft.AspNetCore.Mvc;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Service.Contract;

namespace PRN231.TicketBooking.API.Controllers
{
    [Route("sponsor")]
    [ApiController]
    public class SponsorController : ControllerBase
    {
        private readonly ISponsorService _service;

        public SponsorController(ISponsorService service)
        {
            _service = service;
        }

        [HttpPost("add-sponsor")]
        public async Task<AppActionResult> AddSponsorToEvent([FromForm] CreateSponsorDto dto)
        {
            return await _service.AddSponsorToEvent(dto);
        }

        [HttpPost("add-sponsor-money-to-event")]
        public async Task<AppActionResult> AddSponsorMoneytoEvent(AddSponsorMoneyDto dto)
        {
            return await _service.AddSponsorMoneyToEvent(dto);
        }

        [HttpGet("get-all-sponsor-item-of-an-event/{eventId}/{pageNumber}/{pageSize}")]
        public async Task<AppActionResult> GetAllSponsorItemOfAnEvent(Guid eventId, int pageNumber = 1, int pageSize = 10)
        {
            return await _service.GetAllSponsorItemOfAnEvent(eventId, pageNumber, pageSize);
        }

        [HttpGet("get-sponsor-history-by-event-id/{eventId}/{pageNumber}/{pageSize}")]
        public async Task<AppActionResult> GetSponsorHistoryByEventId(Guid eventId, int pageNumber = 1, int pageSize = 10)
        {
            return await _service.GetSponsorHistoryByEventId(eventId, pageNumber, pageSize);       
        }

        [HttpGet("get-all-sponsors")]
        public async Task<AppActionResult> GetAllSponsors(int pageNumber = 1, int pageSize = 10)
        {
            return await _service.GetAllSponsor(pageNumber, pageSize);
        }

    }
}