using Microsoft.AspNetCore.Mvc;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;
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
        public async Task<AppActionResult> GetAllEvent([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            return await _eventService.GetAllEvent(pageNumber, pageSize);
        }

        [HttpGet("get-available-event")]
        public async Task<AppActionResult> GetAvailableEvent([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            return await _eventService.GetAvailableEvent(pageNumber, pageSize);
        }

        [HttpGet("get-all-event-by-organization-id/{organizationId}/{pageNumber}/{pageSize}")]
        public async Task<AppActionResult> GetAllEventByOrganizationId(Guid organizationId, int pageNumber = 1, int pageSize = 10)
        {
            return await _eventService.GetAllEventByOrganizationId(organizationId, pageNumber, pageSize);
        }

        [HttpGet("get-event-by-id/{id}")]
        public async Task<AppActionResult> GetById(Guid id)
        {
            return await _eventService.GetEventById(id);
        }

        [HttpPost("add-event")]
        public async Task<AppActionResult> AddEvent([FromForm] CreateEventRequest createEventRequest)
        {
            return await _eventService.AddEvent(createEventRequest);
        }

        [HttpPut("update-event/{id}")]
        public async Task<AppActionResult> UpdateEvent(Guid id, [FromForm] UpdateEventRequest updateEventRequest)
        {
            return await _eventService.UpdateEvent(id, updateEventRequest);
        }
    }
}