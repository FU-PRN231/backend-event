using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Common.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Repository.Contract
{
    public interface ISurveyResponseDetailRepository : IRepository<SurveyResponseDetail>
    {
        public Task<bool> AddAnswerToSurvey(List<AnswerDetail> answerDetails, string accountId);
        public Task<PagedResult<SurveyResponseDetail>> GetAllSurveyResponseDetail(Expression<Func<SurveyResponseDetail, bool>>? filter, int pageNumber, int pageSize, Expression<Func<SurveyResponseDetail, object>>? orderBy = null, bool isAscending = true, params Expression<Func<SurveyResponseDetail, object>>[]? includes);
    }
}
