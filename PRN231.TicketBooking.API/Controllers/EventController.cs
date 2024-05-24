using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Service.Contract;

namespace PRN231.TicketBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService service;

        public EventController(IEventService service)
        {
            this.service = service;
        }
        [HttpGet]
        public async Task<AppActionResult> GetAll()
        {

            return new AppActionResult();
        }
   /*     [HttpGet]
        [Route("{id:Guid}")]
        public async Task<AppActionResult> GetById([FromRoute] Guid id)
        {
            return Ok();
        }

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
