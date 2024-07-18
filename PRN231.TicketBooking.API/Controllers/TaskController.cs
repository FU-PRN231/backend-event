using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Common.Util;
using PRN231.TicketBooking.Service.Contract;

namespace PRN231.TicketBooking.API.Controllers
{
    [Route("task")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService; 
        }

        [HttpGet("get-all-event-task-by-status/{eventId}/{pageNumber}/{pageSize}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Permission.ORGANIZATION)]
        public async Task<AppActionResult> GetAllEventTaskByStatus(Guid eventId, BusinessObject.Enum.TaskStatus taskStatus, int pageNumber = 1, int pageSize = 10)
        {
            return await _taskService.GetAllEventTaskByStatus(eventId, taskStatus, pageNumber, pageSize);
        }

        [HttpPost("assign-task-for-event")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Permission.ORGANIZATION)]

        public async Task<AppActionResult> AssignTaskForEvent(TaskRequestDto taskRequestDto)
        {
            return await _taskService.AssignTaskForEvent(taskRequestDto);   
        }

        [HttpDelete("delete-task")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Permission.ORGANIZATION)]
        public async Task<AppActionResult> DeleteTask(Guid taskId, bool confirmed)
        {
            return await _taskService.DeleteTask(taskId, confirmed);  
        }
        [HttpGet("get-all-task-of-event/{eventId}/{pageNumber}/{pageSize}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Permission.ORGANIZATION)]
        public async Task<AppActionResult> GetAllTaskOfEvent(Guid eventId, int pageNumber = 1, int pageSize = 10)
        {
            return await _taskService.GetAllTaskOfEvent(eventId, pageNumber, pageSize);
        }

        [HttpGet("get-a-task-by-id/{taskId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Permission.ORGANIZATION)]
        public async Task<AppActionResult> GetATaskById(Guid taskId)
        {
            return await _taskService.GetATaskById(taskId); 
        }

        [HttpPut("update-status-of-task")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Permission.ORGANIZATION)]
        public async Task<AppActionResult> UpdateStatusOfTask(Guid taskId, bool isSuccessful)
        {
            return await _taskService.UpdateStatusOfTask(taskId, isSuccessful);     
        }

        [HttpPut("update-task-for-event")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Permission.ORGANIZATION)]
        public async Task<AppActionResult> UpdateTaskForEvent(Guid eventId, UpdateTask updateTask)
        {
            return await _taskService.UpdateTaskForEvent(eventId, updateTask);
        }
    }
}
