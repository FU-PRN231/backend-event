using AutoMapper;
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

        public SeatRankService(IServiceProvider serviceProvider, IUnitOfWork unitOfWork, IMapper mapper) : base(serviceProvider)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AppActionResult> GetAllSeatRank(int pageNumber, int pageSize)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                var eventRepository = Resolve<ISeatRankRepository>();
                var data = await eventRepository.GetSeatRanks(pageNumber, pageSize);
                result = new AppActionResult()
                {
                    Result = data,
                    IsSuccess = true
                };
                return BuildAppActionResultError(result, "Get list seat rank successfully!");
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
                var data = await eventRepository.GetSeatRankById(id);
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
                return BuildAppActionResultError(result, "Get seat rank successfully!");
            }
            catch (Exception ex)
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
