using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Repository.Contract
{
    public interface ISurveyResponseDetailRepository : IRepository<SurveyResponseDetail>
    {
        public Task<bool> AddAnswerToSurvey(List<AnswerDetail> answerDetails, string accountId);
    }
}
