using PRN231.TicketBooking.BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Common.Dto.Response
{
    public class SurveyAnswerResponse
    {
        public Survey Survey { get; set; }
        public List<SurveyAnswerDetailDto> SurveyAnswerDetailDtos { get; set; } = new List<SurveyAnswerDetailDto>();
    }

    public class SurveyAnswerDetailDto
    {
        public SurveyQuestionDetail SurveyQuestionDetail { get; set; }
        public List<SurveyResponseDetail> SurveyResponseDetails { get; set; } = new List<SurveyResponseDetail>();
    }
}
