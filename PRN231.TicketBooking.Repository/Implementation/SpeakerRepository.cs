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
    public class SpeakerRepository : GenericRepository<Speaker>, ISpeakerRepository
    {
        private readonly IGenericDAO<Speaker> _dao;

        public SpeakerRepository(IGenericDAO<Speaker> dao, IServiceProvider serviceProvider) : base(dao, serviceProvider)
        {
            _dao = dao; 
        }
    }
}
