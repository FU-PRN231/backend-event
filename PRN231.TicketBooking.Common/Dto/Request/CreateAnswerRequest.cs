using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Common.Dto.Request
{
    public class CreateAnswerRequest
    {
        public string? AccountId { get; set; }
        public Guid SurveyId { get; set; }
        public List<AnswerDetail> AnswerDetails { get; set; } = new List<AnswerDetail>();
    }

    public class AnswerDetail
    {
        public Guid SurveyQuestionDetailId { get; set; }
        public string? TextAnswer { get; set; }
        public int? Rating { get; set; }
    }
}
