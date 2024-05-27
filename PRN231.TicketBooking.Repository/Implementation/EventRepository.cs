using Humanizer;
using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto.Response;
using PRN231.TicketBooking.DAO.dao;
using PRN231.TicketBooking.Repository.Contract;

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
                result = await _eventDAO.GetAllDataByExpression(null, pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }
    }
}