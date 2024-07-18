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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Permission.ORGANIZATION)]
        public async Task<AppActionResult> GetAllOrganization([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            return await _organizationService.GetAllOrganization(pageNumber, pageSize);
        }

        [HttpPost("create-organization")]
        public async Task<AppActionResult> CreateOrganization(CreateOrganizationDto organizationDto)
        {
            return await _organizationService.CreateOrganization(organizationDto);      
        }

        [HttpDelete("delete-organization")]
        public async Task<AppActionResult> DeleteOrganization(int id)
        {
            return await _organizationService.DeleteOrganization(id);   
        }

        [HttpPut("update-organization")]
        public async Task<AppActionResult> UpdateOrganization(UpdateOrganizationDTO organizationDTO)
        {
            return await _organizationService.UpdateOrganization(organizationDTO);        
        }
    }
}
