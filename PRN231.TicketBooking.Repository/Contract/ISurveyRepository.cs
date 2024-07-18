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
    public interface ISurveyRepository:IRepository<Survey>
    {
        public Task<Guid> AddSurvey(CreateSurveyFormRequest dto);
        public Task<PagedResult<Survey>> GetAllServey(Expression<Func<Survey, bool>>? filter, int pageNumber, int pageSize, Expression<Func<Survey, object>>? orderBy = null, bool isAscending = true, params Expression<Func<Survey, object>>[]? includes);
        public Task<Survey> GetServeyById(Guid surveyId, params Expression<Func<Survey, object>>[]? includeProperties);
    }
}
