using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Repository.Contract;
using PRN231.TicketBooking.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Service.Implementation
{
    public class TaskService : GenericBackendService, ITaskService
    {
        private ITaskRepository _taskRepository;
        private IUnitOfWork _unitOfWork;
        public TaskService(IServiceProvider serviceProvider,
            ITaskRepository taskRepository,
            IUnitOfWork unitOfWork
            ) : base(serviceProvider)
        {
            _taskRepository = taskRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AppActionResult> AssignTaskForEvent(TaskRequestDto taskRequestDto)
        {
            var result = new AppActionResult();
            try
            {
                var eventRepository = Resolve<IEventRepository>();
                var eventDb = eventRepository.GetByExpression(p => p!.Id == taskRequestDto.EventId);
                if (eventDb == null)
                {
                    result = BuildAppActionResultError(result, $"Sự kiện với {taskRequestDto.EventId} không tồn tại");
                }
                foreach (var item in taskRequestDto.TaskDetails)
                {
                    var task = new TaskModel
                    {
                        Id = Guid.NewGuid(),
                        Name = item.Name,
                        Description = item.Description,
                        Cost = item.Cost,
                        CreatedDate = item.CreatedDate,
                        EndDate = item.EndDate,
                        EventId = taskRequestDto.EventId,
                        PersonInChargeName = item.PersonInChargeName,
                        PhoneNumber = item.PhoneNumber,
                        Status = item.TaskStatus
                    };
                    await _taskRepository.Insert(task);
                    await _unitOfWork.SaveChangeAsync();
                }
                result.Messages.Add("Tạo task thành công");
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public async Task<AppActionResult> DeleteTask(Guid taskId, bool confirmed)
        {
            var result = new AppActionResult();
            try
            {
                if (confirmed)
                {
                    var taskDb = await _taskRepository.GetByExpression(p => p.Id == taskId);
                    if (taskDb == null)
                    {
                        result = BuildAppActionResultError(result, $"Task này không tồn tại");
                    }
                    await _taskRepository.DeleteById(taskId);
                    await _unitOfWork.SaveChangeAsync();
                    result.Messages.Add("Xóa task thành công");
                }
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public async Task<AppActionResult> GetAllEventTaskByStatus(Guid eventId, BusinessObject.Enum.TaskStatus taskStatus, int pageNumber, int pageSize)
        {
            var result = new AppActionResult();
            try
            {
                var eventRepository = Resolve<IEventRepository>();
                var eventDb = await eventRepository.GetByExpression(p => p.Id == eventId);
                if (eventDb == null)
                {
                    result = BuildAppActionResultError(result, $"Sự kiện với {eventId} không tìm thấy");
                }
                var taskDb = _taskRepository.GetAllDataByExpression(p => p.EventId == eventId && p.Status == taskStatus, pageNumber, pageSize, null, false, p => p.Event!) ;
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public async Task<AppActionResult> GetAllTaskOfEvent(Guid eventId, int pageNumber, int pageSize)
        {
            var result = new AppActionResult();
            try
            {
                var eventRepository = Resolve<IEventRepository>();
                var eventDb = await eventRepository.GetByExpression(p => p.Id == eventId);
                if (eventDb == null)
                {
                    result = BuildAppActionResultError(result, $"Sự kiện với {eventId} không tìm thấy");
                }
                var taskDb = await _taskRepository.GetAllDataByExpression(p => p.Id == eventId, pageNumber, pageSize, null, false, p => p.Event!);
                result.Result = taskDb;
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public async Task<AppActionResult> GetATaskById(Guid taskId)
        {
            var result = new AppActionResult();
            try
            {
                result.Result = await _taskRepository.GetByExpression(p => p!.Id == taskId, p => p.Event!);
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public async Task<AppActionResult> UpdateStatusOfTask(Guid taskId, bool isSuccessful)
        {
            var result = new AppActionResult();
            try
            {
                var taskDb = await _taskRepository.GetByExpression(p => p.Id == taskId);
                if (taskDb == null)
                {
                    result = BuildAppActionResultError(result, $"Task này không tồn tại");
                }
                else
                {
                    if (!isSuccessful) taskDb.Status = BusinessObject.Enum.TaskStatus.FAILED;
                    if (taskDb.Status == BusinessObject.Enum.TaskStatus.NOT_YET) taskDb.Status = BusinessObject.Enum.TaskStatus.ONGOING;
                    else taskDb.Status = BusinessObject.Enum.TaskStatus.FINNISHED;
                    await _unitOfWork.SaveChangeAsync();
                }
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public async Task<AppActionResult> UpdateTaskForEvent(Guid eventId, UpdateTask updateTask)
        {
            var result = new AppActionResult();
            try
            {
                var eventRepository = Resolve<IEventRepository>();
                var eventDb = await eventRepository.GetByExpression(p => p.Id == eventId);
                if (eventDb == null)
                {
                    result = BuildAppActionResultError(result, $"Sự kiện với {eventId} không tìm thấy");
                }
                var taskDb = await  _taskRepository.GetByExpression(p => p.Id == updateTask.Id);
                if (taskDb == null)
                {
                    result = BuildAppActionResultError(result, $"Task này không tồn tại");
                }
                taskDb!.Name = updateTask.Name;
                taskDb!.Description = updateTask.Description;
                taskDb!.CreatedDate = updateTask.CreatedDate;
                taskDb!.EndDate = updateTask.EndDate;
                taskDb!.Cost = updateTask.Cost;
                taskDb!.PersonInChargeName = updateTask.PersonInChargeName;
                taskDb!.Description = updateTask.Description;
                taskDb!.PhoneNumber = updateTask.PhoneNumber;
                taskDb!.Status = updateTask.TaskStatus;

                await _taskRepository.Update(taskDb);
                await _unitOfWork.SaveChangeAsync();
                result.Messages.Add("Update task thành công");
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }
    }
}
