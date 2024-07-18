using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Common.Dto.Response;
using PRN231.TicketBooking.DAO.dao;
using PRN231.TicketBooking.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Repository.Implementation
{
    public class SurveyResponseDetailRepository : GenericRepository<SurveyResponseDetail>, ISurveyResponseDetailRepository
    {
        public SurveyResponseDetailRepository(IGenericDAO<SurveyResponseDetail> dao, IServiceProvider serviceProvider) : base(dao, serviceProvider)
        {
        }

        public async Task<bool> AddAnswerToSurvey(List<AnswerDetail> answerDetails, string? accountId)
        {
            bool result = false;
            try
            {
                List<SurveyResponseDetail> data = new List<SurveyResponseDetail>();
                foreach (AnswerDetail answerDetail in answerDetails)
                {
                    data.Add(new SurveyResponseDetail()
                    {
                        Id = Guid.NewGuid(),
                        AccountId = accountId,
                        SurveyQuestionId = answerDetail.SurveyQuestionDetailId,
                        Rating = answerDetail.Rating,
                        TextAnswer = answerDetail.TextAnswer,
                    });
                }
                await this.InsertRange(data);
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
        public async Task<PagedResult<SurveyResponseDetail>> GetAllSurveyResponseDetail(Expression<Func<SurveyResponseDetail, bool>>? filter, int pageNumber, int pageSize, Expression<Func<SurveyResponseDetail, object>>? orderBy = null, bool isAscending = true, params Expression<Func<SurveyResponseDetail, object>>[]? includes)
        {
            PagedResult<SurveyResponseDetail> result = null;
            try
            {
                result = new PagedResult<SurveyResponseDetail>();
                result = await _dao.GetAllDataByExpression(filter, pageNumber, pageSize, orderBy, isAscending, includes);
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }
    }
}
