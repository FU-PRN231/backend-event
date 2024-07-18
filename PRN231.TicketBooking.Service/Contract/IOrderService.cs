using Microsoft.AspNetCore.Http;
using PRN231.TicketBooking.BusinessObject.Enum;
using PRN231.TicketBooking.BusinessObject.Models;
using PRN231.TicketBooking.Common.Dto;
using PRN231.TicketBooking.Common.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Service.Contract
{
    public interface IOrderService
    {
        Task<AppActionResult> CreateOrderWithPayment(OrderRequestDto orderRequestDto, HttpContext context);
        Task<AppActionResult> GetAllOrder(int pageNumber, int pageSize);
        Task<AppActionResult> GetAllOrderByStatus(OrderStatus orderStatus, int pageNumber, int pageSize);
        Task<AppActionResult> GetAllOrderByAccountId(string accountId, int pageNumber, int pageSize);
        Task<AppActionResult> GetAllOrderDetailByOrderId(Guid orderId, int pageNumber, int pageSize);
        Task<AppActionResult> UpdateStatus(Guid orderId, bool isSuccessful);
        Task<AppActionResult> GetEventOrderByStatus(Guid eventId, OrderStatus orderStatus, int pageNumber, int pageSize);
        Task<AppActionResult> GetAllOrderByEventId(Guid eventId, int pageNumber, int pageSize);
        Task<AppActionResult> CancelOrder(Guid orderId);
        Task<AppActionResult> PurchaseOrder(Guid orderId, HttpContext context);
        Task<AppActionResult> GenerateTicketQR(Guid orderId);
        Task<AppActionResult> SendTicketEmail(Guid orderId);
    }
}
