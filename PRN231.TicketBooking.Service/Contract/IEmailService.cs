namespace PRN231.TicketBooking.Service.Contract;

public interface IEmailService
{
    public void SendEmail(string recipient, string subject, string body);
}