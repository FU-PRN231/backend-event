using AutoMapper;
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
    public class SurveyQuestionDetailRepository : GenericRepository<SurveyQuestionDetail>, ISurveyQuestionDetailRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericDAO<SurveyQuestionDetail> _dao;

        public SurveyQuestionDetailRepository(IUnitOfWork unitOfWork, IMapper mapper, IGenericDAO<SurveyQuestionDetail> dao, IServiceProvider serviceProvider) : base(dao, serviceProvider)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _dao = dao;
        }

        public async Task<bool> InsertRangeSurveyQuestionDetail(Guid surveyId, List<QuestionDetailRequest> details)
        {
            bool result = false;
            try
            {
                List<SurveyQuestionDetail> data = _mapper.Map<List<SurveyQuestionDetail>>(details);
                data.ForEach(item => item.SurveyId = surveyId);
                await this.InsertRange(data);
                result = true;
            } catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public async Task<PagedResult<SurveyQuestionDetail>> GetAllSurveyDetail(Expression<Func<SurveyQuestionDetail, bool>>? filter, int pageNumber, int pageSize, Expression<Func<SurveyQuestionDetail, object>>? orderBy = null, bool isAscending = true, params Expression<Func<SurveyQuestionDetail, object>>[]? includes)
        {
            PagedResult<SurveyQuestionDetail> result = null;
            try
            {
                result = new PagedResult<SurveyQuestionDetail>();
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
