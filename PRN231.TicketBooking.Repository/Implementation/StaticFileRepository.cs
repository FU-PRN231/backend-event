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
    public class StaticFileRepository : GenericRepository<StaticFile>, IStaticFileRepository
    {
        private readonly IGenericDAO<StaticFile> _dao;
        public StaticFileRepository(IGenericDAO<StaticFile> dao, IServiceProvider serviceProvider) : base(dao, serviceProvider)
        {
            _dao = dao;
        }

        public async Task<bool> AddStaticFileFromEvent(StaticFile staticFile)
        {
            try
            {
                var result = await _dao.Insert(staticFile);
                if (result == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<StaticFile> GetStaticFileById(Guid id)
        {
            try
            {
                var result = await _dao.GetById(id);
                if (result == null)
                {
                    return null;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<StaticFile> UploadStaticFile(StaticFile staticFile)
        {
            try
            {
                var result = await _dao.Update(staticFile);
                if (result == null)
                {
                    return null;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
