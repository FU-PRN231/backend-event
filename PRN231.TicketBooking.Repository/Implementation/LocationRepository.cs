using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.DAO.dao;
using PRN231.TicketBooking.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Repository.Implementation
{
    public class LocationRepository : GenericRepository<Location>, ILocationRepository
    {
        private readonly IGenericDAO<Location> _locationDAO;
        private readonly IGenericDAO<Event> _eventDAO;
        public LocationRepository(IGenericDAO<Location> dao, IServiceProvider serviceProvider, IGenericDAO<Location> locationDAO, IGenericDAO<Event> eventDAO) : base(dao, serviceProvider)
        {
            _locationDAO = locationDAO;
            _eventDAO = eventDAO;
        }

        public async Task<Location> GetLocationByEventId(Guid eventId)
        {
            Location result = null;
            try
            {
                result = new Location();
                var eventEntity = await _eventDAO.GetById(eventId);
                if (eventEntity == null)
                {
                    return null;
                }
                result = await _locationDAO.GetByExpression(filter: x=>x.Id == eventEntity.LocationId);
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }
    }
}
