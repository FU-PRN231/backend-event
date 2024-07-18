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
    [Route("survey")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private ISurveyService _service;

        public SurveyController(ISurveyService service)
        {
            _service = service;
        }

        [HttpPost("insert-survey-form")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Permission.ORGANIZATION)]
        public async Task<AppActionResult> CreateSurveyForm([FromBody]CreateSurveyFormRequest dto)
        {
            return await _service.CreateSurveyForm(dto);
        }

        [HttpPost("add-answer-to-survey")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Permission.ORGANIZATION)]
        public async Task<AppActionResult> CreateAnswerForSurvey([FromBody]CreateAnswerRequest dto)
        {
            return await _service.CreateAnswerForSurvey(dto);
        }

        [HttpGet("get-survey-by-id/{id}")]

        public async Task<AppActionResult> GetSurveyById(Guid id)
        {
            return await _service.GetSurveyById(id);
        }

        [HttpGet("get-all-survey")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Permission.ORGANIZATION)]
        public async Task<AppActionResult> GellAllSurvey()
        {
            return await _service.GellAllSurvey();
        }

        [HttpGet("get-survey-by-organization-id/{organizationId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Permission.ORGANIZATION)]
        public async Task<AppActionResult> GellSurveyOfOrganization(Guid organizationId)
        {
            return await _service.GellSurveyOfOrganization(organizationId);
        }

        [HttpGet("get-survey-response-by-survey-id/{surveyId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Permission.ORGANIZATION)]
        public async Task<AppActionResult> GetSurveyResponseBySurveyId(Guid surveyId)
        {
            return await _service.GetSurveyResponseBySurveyId(surveyId);
        }
    }
}
