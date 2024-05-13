using Microsoft.AspNetCore.Mvc;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Service.Contract;

namespace PRN231.TicketBooking.API.Controllers
{
    public class SponsorController : Controller
    {

        private readonly ISponsorService _service;
        public SponsorController(ISponsorService service)
        {
            _service = service;
        }
        [HttpPost("add-sponsor")]
        public async Task<AppActionResult> AddSponsorToEvent([FromBody]CreateSponsorDto dto)
        {
            return await _service.AddSponsorToEvent(dto);
        }
    }
}
