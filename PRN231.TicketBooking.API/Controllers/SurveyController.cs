using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;
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
        public async Task<AppActionResult> CreateSurveyForm([FromBody]CreateSurveyFormRequest dto)
        {
            return await _service.CreateSurveyForm(dto);
        }

        [HttpPost("add-answer-to-survey")]
        public async Task<AppActionResult> CreateAnswerForSurvey([FromBody]CreateAnswerRequest dto)
        {
            return await _service.CreateAnswerForSurvey(dto);
        }

        [HttpGet("get-survey-by-id/{id}")]
        public async Task<AppActionResult> GetSurveyById(Guid id)
        {
            return await _service.GetSurveyById(id);
        }
    }
}
