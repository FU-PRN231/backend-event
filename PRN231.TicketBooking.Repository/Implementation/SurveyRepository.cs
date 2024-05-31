using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Common.Util;
using PRN231.TicketBooking.DAO.dao;
using PRN231.TicketBooking.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Repository.Implementation
{
    public class SurveyRepository:GenericRepository<Survey>, ISurveyRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public SurveyRepository(IUnitOfWork unitOfWork, IGenericDAO<Survey> dao, IServiceProvider serviceProvider) : base(dao, serviceProvider)
        {
            _unitOfWork = unitOfWork;
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
    }
}
