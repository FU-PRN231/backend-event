using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Common.Dto.Response;
using PRN231.TicketBooking.DAO.dao;
using PRN231.TicketBooking.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Repository.Implementation
{
    public class TaskRepository : GenericRepository<BusinessObject.Models.Task>, ITaskRepository
    {
        private readonly IGenericDAO<BusinessObject.Models.Task> _dao;
        public TaskRepository(IGenericDAO<BusinessObject.Models.Task> dao, IServiceProvider serviceProvider) : base(dao, serviceProvider)
        {
            _dao = dao; 
        }

        public async Task<PagedResult<BusinessObject.Models.Task>> GetAllEventTaskByStatus(int pageNumber, int pageSize, Guid eventId, BusinessObject.Enum.TaskStatus taskStatus)
        {
            PagedResult<BusinessObject.Models.Task> result = null;
            try
            {
                result = new PagedResult<BusinessObject.Models.Task>();
                result = await _dao.GetAllDataByExpression(p => p.EventId == eventId && p.Status == taskStatus, pageNumber, pageSize, null, false, p => p.Event!);
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }

        public async Task<PagedResult<BusinessObject.Models.Task>> GetAllTaskOfEvent(int pageNumber, int pageSize, Guid eventId)
        {
            PagedResult<BusinessObject.Models.Task> result = null;
            try
            {
                result = new PagedResult<BusinessObject.Models.Task>();
                result = await _dao.GetAllDataByExpression(p => p.EventId == eventId, pageNumber, pageSize, null, false, p => p.Event!);
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }

        public async Task<BusinessObject.Models.Task> getTaskById(Guid taskId)
        {
            BusinessObject.Models.Task result = null;
            try
            {
                result = new BusinessObject.Models.Task();
                result = await _dao.GetByExpression(p => p.Id == taskId);
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }

        public async Task<BusinessObject.Models.Task> getTaskByIdIncludeEvent(Guid taskId)
        {
            BusinessObject.Models.Task result = null;
            try
            {
                result = new BusinessObject.Models.Task();
                result = await _dao.GetByExpression(p => p!.Id == taskId, p => p.Event!);
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }
    }
}
