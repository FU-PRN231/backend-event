using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Service.Contract;
using PRN231.TicketBooking.BusinessObject.Enum;
using PRN231.TicketBooking.Service.Payment.PaymentResponse;

namespace PRN231.TicketBooking.API.Controllers
{
    [Route("order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;   
        }

        [HttpPost("create-order-with-payment")]
        public async Task<AppActionResult> CreateOrderWithPayment(OrderRequestDto orderRequestDto)
        {
            return await _orderService.CreateOrderWithPayment(orderRequestDto, HttpContext);
        }

        [HttpPost("purchase-order/{orderId}")]
        public async Task<AppActionResult> PurchaseOrder(Guid orderId)
        {
            return await _orderService.PurchaseOrder(orderId, HttpContext);     
        }

        [HttpPost("cancel-order/{orderId}")]
        public async Task<AppActionResult> CancelOrder(Guid orderId)
        {
            return await _orderService.CancelOrder(orderId);
        }
        
        [HttpGet("VNPayIpn")]
        public async Task<IActionResult> VNPayIPN()
        {
            try
            {
                var response = new VNPayResponseDto
                {
                    PaymentMethod = Request.Query["vnp_BankCode"],
                    OrderDescription = Request.Query["vnp_OrderInfo"],
                    OrderId = Request.Query["vnp_TxnRef"],
                    PaymentId = Request.Query["vnp_TransactionNo"],
                    TransactionId = Request.Query["vnp_TransactionNo"],
                    Token = Request.Query["vnp_SecureHash"],
                    VnPayResponseCode = Request.Query["vnp_ResponseCode"],
                    PayDate = Request.Query["vnp_PayDate"],
                    Amount = Request.Query["vnp_Amount"],
                    Success = true
                };

                if (response.VnPayResponseCode == "00")
                {
                    var orderId = response.OrderId.ToString().Split(" ");
                    //await _orderService.UpdatesSucessStatus(Guid.Parse(orderId[0]));

                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            return Ok(new
            {
                RspCode = "00",
                Message = "Confirm Success"
            });
        }

        [HttpGet("get-all-order/{pageNumber}/{pageSize}")]
        public async Task<AppActionResult> GetAllOrder(int pageNumber = 1, int pageSize = 10)
        {
            return await _orderService.GetAllOrder(pageNumber, pageSize);
        }

        [HttpGet("get-all-order-by-status/{pageNumber}/{pageSize}")]
        public async Task<AppActionResult> GetAllOrderByStatus(OrderStatus orderStatus, int pageNumber = 1, int pageSize = 10)
        {
            return await _orderService.GetAllOrderByStatus(orderStatus, pageNumber, pageSize);  
        }

        [HttpGet("get-all-order-by-accountId/{accountId}/{pageNumber}/{pageSize}")]
        public async Task<AppActionResult> GetAllOrderByAccountId(string accountId, int pageNumber = 1, int pageSize = 10)
        {
            return await _orderService.GetAllOrderByAccountId(accountId, pageNumber, pageSize); 
        }

        [HttpGet("get-all-order-detail-by-order-id/{orderId}/{pageNumber}/{pageSize}")]
        public async Task<AppActionResult> GetAllOrderDetailByOrderId(Guid orderId, int pageNumber = 1, int pageSize = 10)
        {
            return await _orderService.GetAllOrderDetailByOrderId(orderId, pageNumber, pageSize);
        }

        [HttpGet("get-event-order-by-status/{eventId}/{pageNumber}/{pageSize}")]
        public async Task<AppActionResult> GetEventOrderByStatus(Guid eventId, OrderStatus orderStatus, int pageNumber = 1, int pageSize = 10)
        {
            return await _orderService.GetEventOrderByStatus(eventId, orderStatus, pageNumber, pageSize);
        }

        [HttpGet("get-all-order-by-event-id/{eventId}/{pageNumber}/{pageSize}")]
        public async Task<AppActionResult> GetAllOrderByEventId(Guid eventId, int pageNumber = 1, int pageSize = 10)
        {
            return await _orderService.GetAllOrderByEventId(eventId, pageNumber, pageSize);
        }

        [HttpPut("update-status")]
        public async Task<AppActionResult> UpdateStatus(Guid orderId)
        {
            return await _orderService.UpdateStatus(orderId, true);
        }

        [HttpGet("send-ticket-email")]
        public async Task<AppActionResult> SendTicketEmail(Guid orderId)
        {
            return await _orderService.SendTicketEmail(orderId);
        }
    }
}
