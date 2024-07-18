using Microsoft.AspNetCore.Authorization;
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
        [Authorize("REGISTERED")]
        public async Task<AppActionResult> CheckIn(CheckInEventRequest checkInEventRequest)
        {
            return await _attendeeService.CheckInAttendee(qrString);
        }

        [HttpGet("get-all-attendee-by-eventId/{eventId}")]
        [Authorize("ORGANIZATION")]
        public async Task<AppActionResult> GetAllAttendeeByEventId(Guid eventId)
        {
            return await _attendeeService.GetAllAttendeeByEventId(eventId);
        }
    }
}
