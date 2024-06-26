using Microsoft.AspNetCore.Http;

namespace PRN231.TicketBooking.Service.Contract
{
    public interface IEmailService
    {
        public void SendEmail(string recipient, string subject, string body);
        public void SendEmailWithFiles(string recipient, string subject, string body, List<IFormFile> files);

    }
}