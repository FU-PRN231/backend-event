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
    public interface ISurveyQuestionDetailRepository:IRepository<SurveyQuestionDetail>
    {
        public Task<bool> InsertRangeSurveyQuestionDetail(Guid eventId, List<QuestionDetailRequest> details);
        public Task<PagedResult<SurveyQuestionDetail>> GetAllSurveyDetail(Expression<Func<SurveyQuestionDetail, bool>>? filter, int pageNumber, int pageSize, Expression<Func<SurveyQuestionDetail, object>>? orderBy = null, bool isAscending = true, params Expression<Func<SurveyQuestionDetail, object>>[]? includes);
    }
}
