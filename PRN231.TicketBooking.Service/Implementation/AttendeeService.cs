using AutoMapper;
using Humanizer;
using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Common.Dto.Response;
using PRN231.TicketBooking.Repository.Contract;
using PRN231.TicketBooking.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Service.Implementation
{
    public class AttendeeService : GenericBackendService, IAttendeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AttendeeService(IServiceProvider serviceProvider, IUnitOfWork unitOfWork, IMapper mapper) : base(serviceProvider)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AppActionResult> CheckInAttendee(CheckInEventRequest checkInEvent)
        {
            AppActionResult result = new AppActionResult();
            try
            {
                var attendeeRepo = Resolve<IAttendeeRepostory>();
                var attendeeEntity = await attendeeRepo.GetAttendeeByEvent(checkInEvent.EventId);
                if (attendeeEntity == null)
                {
                    result.IsSuccess = false;
                    result.Messages[0] = $"Event Not Found with Id: {checkInEvent.EventId}!";
                    return result;
                }
                var orderRepo = Resolve<IOrderRepository>();
                var accountId = await orderRepo.GetAccountId(attendeeEntity.OrderDetail.OrderId);
                if (accountId == null || !accountId.Equals(attendeeEntity.OrderDetail.OrderId))
                {
                    result.IsSuccess = false;
                    result.Messages[0] = $"Account Not Found with Id: {checkInEvent.AccountId}!";
                    return result;
                }
                
                var item = await attendeeRepo.CheckInAttendee(attendeeEntity);
                
                await _unitOfWork.SaveChangeAsync();
                result.Result = _mapper.Map<UpdateAttendeeReponse>(item);
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }
    }
}
