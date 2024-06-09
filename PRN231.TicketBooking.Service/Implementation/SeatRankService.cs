using AutoMapper;
using Microsoft.Extensions.Logging;
using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Common.Dto.Response;
using PRN231.TicketBooking.Common.Util;
using PRN231.TicketBooking.Repository.Contract;
using PRN231.TicketBooking.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Service.Implementation
{
    public class SeatRankService : GenericBackendService, ISeatRankService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISeatRankRepository _repository;
        public SeatRankService(IServiceProvider serviceProvider, IUnitOfWork unitOfWork, IMapper mapper, ISeatRankRepository repository) : base(serviceProvider)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AppActionResult> DeleteSeatRank(Guid seatrankId)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                var seatRankDb = _repository.GetByExpression(p => p!.Id == seatrankId);
                if (seatRankDb == null)
                {
                    result = BuildAppActionResultError(result, $"Ghế ngồi  với {seatrankId} không tồn tại");
                    return result;
                }
                await _repository.DeleteById(seatrankId);
                await _unitOfWork.SaveChangeAsync();
                result.Messages.Add("Xóa ghế ngồi này thành công");
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result; 
        }

        public async Task<AppActionResult> GetAllSeatRank(int pageNumber, int pageSize)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                var data = await _repository.GetSeatRanks(pageNumber, pageSize);
                result = new AppActionResult()
                {
                    Result = data,
                    IsSuccess = true
                };
                return BuildAppActionResultSuccess(result, "Get list seat rank successfully!");
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public async Task<AppActionResult> GetAllSeatRankByEvent(Guid eventId, int pageNumber, int pageSize)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                var eventRepository = Resolve<IEventRepository>();
                var eventDb = await eventRepository.GetByExpression(p => p.Id == eventId);
                if (eventDb == null)
                {
                    result = BuildAppActionResultError(result, $"Sự kiện này với {eventId} không tồn tại");
                    return result;
                }
                result.Result = _repository.GetAllDataByExpression(p => p.EventId == eventId, pageNumber, pageSize, null, false, p => p.Event!);
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public async Task<AppActionResult> GetSeatRankByFilter(FilterSeatRankDto filterSeatRankDto, int pageNumber, int pageSize)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                result.Result = await _repository.GetAllDataByExpression(p => p.Price <= filterSeatRankDto.Price || (p.StartTime >= filterSeatRankDto.StartTime &&
            p.EndTime <= filterSeatRankDto.EndTime) || p.Description.Contains(filterSeatRankDto.Description!), pageNumber, pageSize, null, false, p => p.Event!);
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public async Task<AppActionResult> GetSeatRankById(Guid id)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                var eventRepository = Resolve<ISeatRankRepository>();
                var data = await eventRepository.GetByExpression(p => p!.Id == id, p => p.Event!);
                if (data == null)
                {
                    result = new AppActionResult()
                    {
                        Result = data,
                        IsSuccess = false
                    };
                    return BuildAppActionResultError(result, "Seat rank not found!");
                }
                result = new AppActionResult()
                {
                    Result = data,
                    IsSuccess = true
                };
                return BuildAppActionResultSuccess(result, "Get seat rank successfully!");
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public async Task<AppActionResult> UpdateSeatRank(UpdateSeatRankDto updateSeatRankDto)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                var eventRepository = Resolve<IEventRepository>();
                var eventDb = await eventRepository.GetByExpression(p => p.Id == updateSeatRankDto.EventId);
                if (eventDb == null)
                {
                    result = BuildAppActionResultError(result, $"Sự kiện này với {updateSeatRankDto.EventId} không tồn tại");
                    return result;
                }
                var seatRankDb = await _repository.GetByExpression(p => p!.Id == updateSeatRankDto.Id);
                if (seatRankDb == null)
                {
                    result = BuildAppActionResultError(result, $"Ghế ngồi  với {updateSeatRankDto.Id} không tồn tại");
                    return result;
                }
                seatRankDb.Name = updateSeatRankDto.Name;   
                seatRankDb.Price = updateSeatRankDto.Price; 
                seatRankDb.Description = updateSeatRankDto.Description; 
                seatRankDb.StartTime = updateSeatRankDto.StartTime; 
                seatRankDb.EndTime = updateSeatRankDto.EndTime; 
                seatRankDb.RemainingCapacity = updateSeatRankDto.RemainingCapacity; 
                seatRankDb.Quantity = updateSeatRankDto.Quantity;

                await _repository.Update(seatRankDb);
                await _unitOfWork.SaveChangeAsync();
                result.Messages.Add("Cập nhập chỗ ngồi thành công");
            } catch (Exception ex) 
            { 
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        /*  public async Task<AppActionResult> AddSeatRankFromEvent(CreateSeatRankDtoRequest dto)
          {
              AppActionResult result = new AppActionResult();
              try
              {
                  // Save Account => Save Sponsor =>
                  var eventRepository = Resolve<IEventRepository>();
                  var eventDb = await eventRepository!.GetById(dto.EventId);
                  if (eventDb == null)
                  {
                      result = BuildAppActionResultError(result, $"Không tồn tại sự kiện với Id {dto.EventId}");
                      return result;
                  }
                  var seatRankRepository = Resolve<ISeatRankRepository>();
                  var seatRankEntity = _mapper.Map<SeatRank>(dto);
                  seatRankEntity.Id = Guid.NewGuid();
                  var item = await seatRankRepository.AddSeatRank(seatRankEntity);
                  if (item == null)
                  {
                      result.Messages[0] = "Create Seat Rank fail!";
                      result.IsSuccess = false;
                      return BuildAppActionResultError(result, "Create Seat Rank fail!");
                  }
                  var seatRankResponse = _mapper.Map<CreateSeatRankResponse>(seatRankEntity);
                  result.Result = seatRankResponse;
                  result.Messages[0] = "Create Seat Rank successfully!";
                  return BuildAppActionResultError(result, "Create Seat Rank successfully!");
              }
              catch (Exception ex)
              {
                  result = BuildAppActionResultError(result, ex.Message);
              }
              return result;
          }
        */
    }
}
