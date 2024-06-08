using PRN231.TicketBooking.BusinessObject.Enum;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Service.Contract
{
    public interface ITaskService
    {
        Task<AppActionResult> AssignTaskForEvent(TaskRequestDto taskRequestDto);
        Task<AppActionResult> GetAllTaskOfEvent(Guid eventId, int pageNumber, int pageSize);
        Task<AppActionResult> GetAllEventTaskByStatus(Guid eventId, BusinessObject.Enum.TaskStatus taskStatus, int pageNumber, int pageSize);
        Task<AppActionResult> GetATaskById(Guid taskId);
        Task<AppActionResult> DeleteTask(Guid taskId, bool confirmed);
        Task<AppActionResult> UpdateStatusOfTask(Guid taskId, bool isSuccessful);
        Task<AppActionResult> UpdateTaskForEvent(Guid eventId, UpdateTask updateTask);
    }
}
