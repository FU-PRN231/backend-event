using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.DAO.dao;
using PRN231.TicketBooking.Repository.Contract;

namespace PRN231.TicketBooking.Repository.Implementation
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {

        public EventRepository( IGenericDAO<Event> dao, IServiceProvider serviceProvider) : base(dao, serviceProvider)
        {
        }
    }
}