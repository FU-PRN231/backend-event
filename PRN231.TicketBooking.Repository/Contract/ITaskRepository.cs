using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Repository.Contract
{
    public interface ITaskRepository : IRepository<BusinessObject.Models.Task>
    {
        public Task<PagedResult<BusinessObject.Models.Task>> GetAllEventTaskByStatus(int pageNumber, int pageSize, Guid eventId, BusinessObject.Enum.TaskStatus taskStatus);
        public Task<PagedResult<BusinessObject.Models.Task>> GetAllTaskOfEvent(int pageNumber, int pageSize, Guid eventId);
        public Task<BusinessObject.Models.Task> getTaskById(Guid taskId);
        public Task<BusinessObject.Models.Task> getTaskByIdIncludeEvent(Guid taskId);
    }
}
