using Humanizer;
using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Common.Dto.Response;
using PRN231.TicketBooking.Common.Util;
using PRN231.TicketBooking.DAO.dao;
using PRN231.TicketBooking.Repository.Contract;
using System.Linq.Expressions;

namespace PRN231.TicketBooking.Repository.Implementation
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        private readonly IGenericDAO<Event> _eventDAO;
        private readonly IUnitOfWork _unitOfWork;

        public EventRepository(IGenericDAO<Event> dao, IServiceProvider serviceProvider, IUnitOfWork unitOfWork) : base(dao, serviceProvider)
        {
            _eventDAO = dao;
            _unitOfWork = unitOfWork;
        }

        public async Task<AppActionResult> AddEvent(Event evnt)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                var data = await _eventDAO.Insert(evnt);
                result.Result = data;
                result.Messages.Add("Insert event successfully!");
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Messages.Add(ex.Message);
            }
            return result;
        }

        public async Task<Event> GetEventById(Guid id)
        {
            Event result = null;
            try
            {
                result = new Event();
                result = await _eventDAO.GetById(id);
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }

        public async Task<PagedResult<Event>> GetEvents(int pageNumber, int pageSize)
        {
            PagedResult<Event> result = null;
            try
            {
                result = new PagedResult<Event>();
                result = await _eventDAO.GetAllDataByExpression(
                    filter: null,
                    pageNumber: pageNumber,
                    pageSize: pageSize,
                    includes: new Expression<Func<Event, object>>[] {
                        e => e.Location,
                        e => e.Organization
                    }
                );
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }

        public async Task<AppActionResult> UpdateEvent(Event eventEntity)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                var data = await _eventDAO.Update(eventEntity);
                result.Result = data;
                result.Messages.Add("Update event successfully!");
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Messages.Add(ex.Message);
            }
            return result;
        }
    }
}