using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Service.Contract;

namespace PRN231.TicketBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeatRankController : ControllerBase
    {
        private readonly ISeatRankService _seatRankService;

        public SeatRankController(ISeatRankService seatRankService)
        {
            _seatRankService = seatRankService;
        }

        [HttpGet]
        public async Task<AppActionResult> GetAllEvent([FromQuery] int pageNumber=1, [FromQuery] int pageSize = 10)
        {
            return await _seatRankService.GetAllSeatRank(pageNumber, pageSize);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<AppActionResult> GetById([FromRoute] Guid id)
        {
            return await _seatRankService.GetSeatRankById(id);
        }
    }
}
