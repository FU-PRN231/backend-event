using PRN231.TicketBooking.BusinessObject.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Common.Dto.Request
{
    public class CreateSurveyFormRequest
    {
        public string Name { get; set; } = null!;
        public Guid EventId { get; set; }
        public string CreateBy { get; set; }
        public List<QuestionDetailRequest> QuestionDetailRequests { get; set; } = new List<QuestionDetailRequest>();
    }

    public class QuestionDetailRequest
    {
        public int No { get; set; }
        public string Question { get; set; } = null!;
        public AnswerType? AnswerType { get; set; }
        public int? RatingMax { get; set; }
    }
}
