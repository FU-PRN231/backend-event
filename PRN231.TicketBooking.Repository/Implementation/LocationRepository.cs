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

        public async Task<List<Location>> GetAllLocation(DateTime StartTime, DateTime EndTime)
        {
            List<Location> result = null;
            try
            {
                result = new List<Location>();
                var eventEntities = await _eventDAO.GetAllDataByExpression(filter: x=>x.StartTime<EndTime && x.EndTime>StartTime, 0, 0);
                if (eventEntities.Items == null || eventEntities.Items.Count==0)
                {
                     var item = await _locationDAO.GetAllDataByExpression(null, 0, 0);
                    if (item != null)
                    {
                        result = item.Items;
                    }
                }
                else
                {
                    var occupiedLocationIds = eventEntities.Items.Select(e => e.LocationId).Distinct().ToList();
                    var item = await _locationDAO.GetAllDataByExpression(filter: x => !occupiedLocationIds.Contains(x.Id), 0, 0);
                    if(item != null)
                    {
                        result = item.Items;
                    }
                }
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }
    }
}
