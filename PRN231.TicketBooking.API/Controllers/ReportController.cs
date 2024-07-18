using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Util;
using PRN231.TicketBooking.Service.Contract;

namespace PRN231.TicketBooking.API.Controllers
{
    [Route("report")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private IReportService _service;
        public ReportController(IReportService service)
        {
            _service = service;
        }

        [HttpGet("get-overall-report")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Permission.ORGANIZATION)]
        public async Task<AppActionResult> GetOverallReport(Guid? organizationId, int timePeriod = 1)
        {
            return await _service.GetRevenueReport(organizationId, timePeriod);
        }


        [HttpGet("get-event-detail-report/{eventId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Permission.ORGANIZATION)]
        public async Task<AppActionResult> GetOverallReport(Guid eventId)
        {
            return await _service.GetEventDetailReport(eventId);
        }

        [HttpGet("get-sponsor-report/{sponsorId}/{timePeriod}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Permission.ORGANIZATION)]
        public async Task<AppActionResult> GetSponsorReport(Guid sponsorId, int timePeriod = 1)
        {
            return await _service.GetSponsorReport(sponsorId, timePeriod);
        }
    }
}
