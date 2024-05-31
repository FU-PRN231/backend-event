using AutoMapper;
using MailKit.Search;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using PRN231.TicketBooking.BusinessObject.Enum;
using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Repository.Contract;
using PRN231.TicketBooking.Repository.Implementation;
using PRN231.TicketBooking.Service.Contract;
using PRN231.TicketBooking.Service.Payment.PaymentRequest;
using PRN231.TicketBooking.Service.Payment.PaymentService;
using QRCoder;
using System.Transactions;

namespace PRN231.TicketBooking.Service.Implementation
{
    public class OrderService : GenericBackendService, IOrderService
    {
        private IOrderRepository _orderRepository;
        private IMapper _mapper;
        private IFirebaseService _firebaseService;
        private IUnitOfWork _unitOfWork;

        public OrderService(IServiceProvider serviceProvider, IFirebaseService firebaseService, IMapper mapper, IOrderRepository orderRepository, IUnitOfWork unitOfWork) : base(serviceProvider)
        {
            _firebaseService = firebaseService;
            _mapper = mapper;
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AppActionResult> CreateOrderWithPayment(OrderRequestDto orderRequestDto, HttpContext context)
        {
            var firebaseService = Resolve<IFirebaseService>();
            var result = new AppActionResult();
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var paymentGatewayService = Resolve<IPaymentGatewayService>();
                    var accountRepository = Resolve<IAccountRepository>();
                    var seatRepository = Resolve<IRepository<SeatRank>>();
                    var accountDb = await accountRepository.GetByExpression(p => p!.Id == orderRequestDto.AccountId);
                    if (accountDb == null)
                    {
                        result = BuildAppActionResultError(result, $"Tài khoản với {orderRequestDto.AccountId} không tồn tại");
                        return result;
                    }
                    if (string.IsNullOrEmpty(accountDb!.PhoneNumber))
                    {
                        result = BuildAppActionResultError(result, "Số điện thoại của tài khoản không được để trống");
                        return result;
                    }

                    var seatRankDb = await seatRepository.GetByExpression(p => p.Id == orderRequestDto.SeatRankId);
                    if (seatRankDb == null)
                    {
                        result = BuildAppActionResultError(result, $"Hạng ghế với {orderRequestDto.SeatRankId} không tồn tại");
                        return result;
                    }

                    var order = new Order
                    {
                        Id = Guid.NewGuid(),
                        AccountId = accountDb.Id,
                        PurchaseDate = DateTime.Now,
                        SeatRankId = orderRequestDto.SeatRankId,
                        Status = OrderStatus.PENDING,
                        Total = seatRankDb.Price,
                    };
             
                    if (!BuildAppActionResultIsError(result))
                    {
                        await _orderRepository.Insert(order);
                        await _unitOfWork.SaveChangeAsync();
                        scope.Complete();
                    }

                    var payment = new PaymentInformationRequest
                    {
                        AccountID = order.AccountId,
                        Amount = (double)order.Total,
                        CustomerName = $"{order.Account!.FirstName} {order.Account.LastName}",
                        OrderID = order.Id.ToString(),
                    };
                    result.Result = await paymentGatewayService!.CreatePaymentUrlVnpay(payment, context);
                }
                catch (Exception ex)
                {
                    result = BuildAppActionResultError(result, ex.Message);
                }
                return result;
            }
        }

        public async Task<AppActionResult> GetAllOrder(int pageNumber, int pageSize)
        {
            var result = new AppActionResult();
            try
            {
                result.Result = await _orderRepository.GetAllDataByExpression(null, pageNumber, pageSize, null, false, p => p.SeatRank!, p => p.Account!);
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public async Task<AppActionResult> GetAllOrderByAccountId(string accountId, int pageNumber, int pageSize)
        {
            var result = new AppActionResult();
            try
            {
                result.Result = await _orderRepository.GetAllDataByExpression(p => p.AccountId == accountId, pageNumber, pageSize, null, false, p => p.SeatRank!, p => p.Account!);
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }

        public async Task<AppActionResult> UpdateStatus(Guid orderId, OrderStatus orderStatus)
        {
            var result = new AppActionResult();
            try
            {
                var orderDb = await _orderRepository.GetById(orderId);
                if (orderDb == null)
                {
                    result = BuildAppActionResultError(result, "Đơn hàng này không tồn tại");
                }
                else
                {
                    orderDb.Status = orderStatus;
                    await _unitOfWork.SaveChangeAsync();
                }
            }
            catch (Exception ex)
            {
                result = BuildAppActionResultError(result, ex.Message);
            }
            return result;
        }
    }
}
