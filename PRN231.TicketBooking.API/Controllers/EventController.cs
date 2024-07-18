using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PRN231.TicketBooking.BusinessObject.Enum;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Common.Util;
using PRN231.TicketBooking.Service.Contract;

namespace PRN231.TicketBooking.API.Controllers
{
    [Route("event")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet("get-all-event")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Permission.ORGANIZATION)]
        public async Task<AppActionResult> GetAllEvent([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            return await _eventService.GetAllEvent(pageNumber, pageSize);
        }

        [HttpGet("get-available-event")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Permission.REGISTERED)]
        public async Task<AppActionResult> GetAvailableEvent([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            return await _eventService.GetAvailableEvent(pageNumber, pageSize);
        }

        [HttpGet("get-all-event-by-organization-id/{organizationId}/{pageNumber}/{pageSize}")]
        public async Task<AppActionResult> GetAllEventByOrganizationId(Guid organizationId, int pageNumber = 1, int pageSize = 10)
        {
            return await _eventService.GetAllEventByOrganizationId(organizationId, pageNumber, pageSize);
        }


        [HttpGet("get-all-event-by-status/{pageNumber}/{pageSize}")]
        public async Task<AppActionResult> GetEventByStatus(EventCensorStatus status, int pageNumber = 1, int pageSize = 10)
        {
            return await _eventService.GetAllEventByStatus(status, pageNumber, pageSize);
        }
        [HttpGet("get-event-by-id/{id}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Permission.REGISTERED)]
        public async Task<AppActionResult> GetById(Guid id)
        {
            return await _eventService.GetEventById(id);
        }

        [HttpPost("add-event")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Permission.ORGANIZATION)]
        public async Task<AppActionResult> AddEvent([FromForm] CreateEventRequest createEventRequest)
        {
            return await _eventService.AddEvent(createEventRequest);
        }

        [HttpPut("update-event/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Permission.ORGANIZATION)]
        public async Task<AppActionResult> UpdateEvent(Guid id, [FromForm] UpdateEventRequest updateEventRequest)
        {
            return await _eventService.UpdateEvent(id, updateEventRequest);
        }

        [HttpPost("update-event-status")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Permission.ORGANIZATION)]
        public async Task<AppActionResult> UpdateEventStatus(Guid eventId, EventCensorStatus status)
        {
            return await _eventService.UpdateEventStatus(eventId, status);
        }

        [HttpGet("get-sponsor-event/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Permission.SPONSOR)]
        public async Task<AppActionResult> GetSponsorEvent(Guid id, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            return await _eventService.GetAllEventBySponsorId(id, pageNumber, pageSize);
        }

        [HttpGet("get-organization-event/{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Permission.ORGANIZER)]
        public async Task<AppActionResult> GetOrganizationByEventId(Guid id, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            return await _eventService.GetAllEventByOrganizationId(id, pageNumber, pageSize);
        }
    }
}