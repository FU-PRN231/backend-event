using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN231.TicketBooking.Common.Dto.Request;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Service.Contract;
using PRN231.TicketBooking.BusinessObject.Enum;

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

        [HttpPost("create-order-with-paymen")]
        public async Task<AppActionResult> CreateOrderWithPayment(OrderRequestDto orderRequestDto)
        {
            return await _orderService.CreateOrderWithPayment(orderRequestDto, HttpContext);
        }

        //[HttpGet("get-all-order/{pageNumber}/{pageSize}")]
        //public async Task<AppActionResult> GetAllOrder(int pageNumber = 1, int pageSize = 10)
        //{
        //    return await _orderService.GetAllOrder(pageNumber, pageSize);   
        //}

        //[HttpGet("get-all-order-by-account-id/{accountId}/{pageNumber}/{pageSize}")]
        //public async Task<AppActionResult> GetAllOrderByAccountId(string accountId, int pageNumber = 1, int pageSize = 10)
        //{
        //    return await _orderService.GetAllOrderByAccountId(accountId, pageNumber, pageSize);     
        //}

        [HttpPut("update-status")]
        public async Task<AppActionResult> UpdateStatus(Guid orderId, OrderStatus orderStatus)
        {
            return await _orderService.UpdateStatus(orderId, orderStatus);  
        }
    }
}
