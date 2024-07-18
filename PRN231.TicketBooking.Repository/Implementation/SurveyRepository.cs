using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Common.Dto.Response;
using PRN231.TicketBooking.Common.Util;
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
    public class SurveyRepository:GenericRepository<Survey>, ISurveyRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericDAO<Survey> _dao;

        public SurveyRepository(IUnitOfWork unitOfWork, IGenericDAO<Survey> dao, IServiceProvider serviceProvider) : base(dao, serviceProvider)
        {
            _unitOfWork = unitOfWork;
            _dao = dao;
        }

        public async Task<Guid> AddSurvey(CreateSurveyFormRequest dto)
        {
            var utility = Resolve<Utility>();
            Guid Id = Guid.Empty;
            try
            {
                Id = Guid.NewGuid();
                await this.Insert(new Survey { 
                    Id = Id,
                    EventId = dto.EventId,
                    Name = dto.Name,
                    CreateBy = dto.CreateBy,
                    CreateDate = utility.GetCurrentDateInTimeZone()
                });
            } catch(Exception ex)
            {
                Id = Guid.Empty ;
            }
            return Id;
        }

        public async Task<PagedResult<Survey>> GetAllServey(Expression<Func<Survey, bool>>? filter, int pageNumber, int pageSize, Expression<Func<Survey, object>>? orderBy = null, bool isAscending = true, params Expression<Func<Survey, object>>[]? includes)
        {
            PagedResult<Survey> result = null;
            try
            {
                result = new PagedResult<Survey>();
                result = await _dao.GetAllDataByExpression(filter, pageNumber, pageSize, orderBy, isAscending, includes);
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }

        public async Task<Survey> GetServeyById(Guid surveyId, params Expression<Func<Survey, object>>[]? includeProperties)
        {
            Survey result = null;
            try
            {
                result = new Survey();
                result = await _dao.GetByExpression(s => s.Id == surveyId, includeProperties);
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }
    }
}
