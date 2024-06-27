using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Service.Contract;

namespace PRN231.TicketBooking.API.Controllers
{
    [Route("attendee")]
    [ApiController]
    public class AttendeeController : ControllerBase
    {
        private readonly IAttendeeService _attendeeService;

        public AttendeeController(IAttendeeService attendeeService)
        {
            _attendeeService = attendeeService;
        }
        [HttpPut]
        public async Task<AppActionResult> CheckIn(string qrString)
        {
            return await _attendeeService.CheckInAttendee(qrString);
        }

        [HttpGet("get-all-attendee-by-eventId/{eventId}")]
        public async Task<AppActionResult> GetAllAttendeeByEventId(Guid eventId)
        {
            return await _attendeeService.GetAllAttendeeByEventId(eventId);
        }
    }
}
