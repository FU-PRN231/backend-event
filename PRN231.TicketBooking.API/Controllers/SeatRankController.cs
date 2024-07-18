using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Common.Util;
using PRN231.TicketBooking.Service.Contract;

namespace PRN231.TicketBooking.API.Controllers
{
    [Route("seat-rank")]
    [ApiController]
    public class SeatRankController : ControllerBase
    {
        private readonly ISeatRankService _seatRankService;

        public SeatRankController(ISeatRankService seatRankService)
        {
            _seatRankService = seatRankService;
        }

        [HttpGet("get-all-seat-rank/{pageNumber}/{pageSize}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Permission.REGISTERED)]
        public async Task<AppActionResult> GetAllSeatRank([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            return await _seatRankService.GetAllSeatRank(pageNumber, pageSize);
        }

        [HttpGet("get-by-id/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Permission.REGISTERED)]
        public async Task<AppActionResult> GetById([FromRoute] Guid id)
        {
            return await _seatRankService.GetSeatRankById(id);
        }

        [HttpGet("get-all-seat-rank-by-event/{eventId}/{pageNumber}/{pageSize}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Permission.REGISTERED)]
        public async Task<AppActionResult> GetAllSeatRankByEvent(Guid eventId, int pageNumber = 1, int pageSize = 10)
        {
            return await _seatRankService.GetAllSeatRankByEvent(eventId, pageNumber, pageSize);
        }

        [HttpGet("get-seat-rank-by-filter/{pageNumber}/{pageSize}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Permission.REGISTERED)]
        public async Task<AppActionResult> GetSeatRankByFilter(FilterSeatRankDto filterSeatRankDto, int pageNumber, int pageSize)
        {
            return await _seatRankService.GetSeatRankByFilter(filterSeatRankDto, pageNumber, pageSize);
        }

        [HttpPut("update-seat-rank")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Permission.ORGANIZATION)]
        public async Task<AppActionResult> UpdateSeatRank(UpdateSeatRankDto updateSeatRankDto)
        {
            return await _seatRankService.UpdateSeatRank(updateSeatRankDto);    
        }

        [HttpDelete("delete-seat-rank")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Permission.ORGANIZATION)]
        public async Task<AppActionResult> DeleteSeatRank(Guid seatrankId)
        {
            return await _seatRankService.DeleteSeatRank(seatrankId);   
        }
    }
}
