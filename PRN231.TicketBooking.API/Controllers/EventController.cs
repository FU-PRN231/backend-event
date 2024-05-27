using Microsoft.AspNetCore.Mvc;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Service.Contract;

namespace PRN231.TicketBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<AppActionResult> GetAllEvent([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            return await _eventService.GetAllEvent(pageNumber, pageSize);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<AppActionResult> GetById([FromRoute] Guid id)
        {
            return await _eventService.GetEventById(id);
        }
/*
        [HttpPost]
        public async Task<AppActionResult> Create()
        {
            return Ok();
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<AppActionResult> Update([FromRoute] Guid id)
        {
            return Ok();
        }
*/
    }
}