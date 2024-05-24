using Microsoft.AspNetCore.Http;
using PRN231.TicketBooking.Service.Payment.PaymentRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN231.TicketBooking.Service.Payment.PaymentService
{
    public interface IPaymentGatewayService
    {
        Task<string> CreatePaymentUrlVnpay(PaymentInformationRequest request, HttpContext httpContext);
    }
}
