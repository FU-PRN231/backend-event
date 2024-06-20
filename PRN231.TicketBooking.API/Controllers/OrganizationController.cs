using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Service.Contract;

namespace PRN231.TicketBooking.API.Controllers
{
    [Route("organization")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;
        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }
        [HttpGet("get-all-organization")]
        public async Task<AppActionResult> GetAllOrganization([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            return await _organizationService.GetAllOrganization(pageNumber, pageSize);
        }
    }
}
