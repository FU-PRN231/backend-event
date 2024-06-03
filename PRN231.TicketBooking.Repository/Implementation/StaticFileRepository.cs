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
    public class StaticFileRepository : GenericRepository<StaticFile>, IStaticFileRepository
    {
        private readonly IGenericDAO<StaticFile> _dao;
        public StaticFileRepository(IGenericDAO<StaticFile> dao, IServiceProvider serviceProvider) : base(dao, serviceProvider)
        {
            _dao = dao; 
        }
    }
}
