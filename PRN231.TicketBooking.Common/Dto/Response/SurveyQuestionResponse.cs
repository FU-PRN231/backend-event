using PRN231.TicketBooking.BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Common.Dto.Response
{
    public class SurveyQuestionResponse
    {
        public Survey Survey { get; set; }
        public List<SurveyQuestionDetail> surveyQuestionDetails { get; set; } = new List<SurveyQuestionDetail>();
    }
}
