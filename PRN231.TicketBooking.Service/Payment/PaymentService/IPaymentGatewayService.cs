using Microsoft.AspNetCore.Http;
using PRN231.TicketBooking.Service.Payment.PaymentRequest;

namespace PRN231.TicketBooking.Service.Payment.PaymentService
{
    public interface IPaymentGatewayService
    {
        Task<string> CreatePaymentUrlVnpay(PaymentInformationRequest request, HttpContext httpContext);
    }
}