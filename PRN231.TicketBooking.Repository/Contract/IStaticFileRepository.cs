using PRN231.TicketBooking.BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Repository.Contract
{
    public interface IStaticFileRepository : IRepository<StaticFile>
    {
        public Task<bool> AddStaticFileFromEvent(StaticFile staticFile);
        public Task<StaticFile> GetStaticFileById(Guid id);
        public Task<StaticFile> UploadStaticFile(StaticFile staticFile);
    }
}
