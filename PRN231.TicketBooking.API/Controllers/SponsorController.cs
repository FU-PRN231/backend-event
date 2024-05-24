using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Common.Dto;
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
        [HttpGet("get-all-sponsors")]
        public async Task<AppActionResult> GetAllSponsors()
        {
            return await _service.GetAllSponsor();
        }
    }
}
