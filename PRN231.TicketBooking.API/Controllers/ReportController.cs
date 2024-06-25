using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231.TicketBooking.Common.Dto;
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
        public async Task<AppActionResult> GetOverallReport(Guid? organizationId, int timePeriod = 1)
        {
            return await _service.GetRevenueReport(organizationId, timePeriod);
        }


        [HttpGet("get-event-detail-report/{eventId}")]
        public async Task<AppActionResult> GetOverallReport(Guid eventId)
        {
            return await _service.GetEventDetailReport(eventId);
        }
    }
}
