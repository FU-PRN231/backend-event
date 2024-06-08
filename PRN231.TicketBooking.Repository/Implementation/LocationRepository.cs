using PRN231.TicketBooking.BusinessObject.Models;
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
        public LocationRepository(IGenericDAO<Location> dao, IServiceProvider serviceProvider) : base(dao, serviceProvider)
        {
        }
    }
}
