﻿using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using PRN231.TicketBooking.Common.ConfigurationModel;
using PRN231.TicketBooking.Common.Util;
using PRN231.TicketBooking.Service.Contract;

namespace PRN231.TicketBooking.Service.Implementation;

public class EmailService : GenericBackendService, IEmailService
{
    private readonly EmailConfiguration _emailConfiguration;
    private readonly BackEndLogger _logger;

    public EmailService(BackEndLogger logger, IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _logger = logger;
        _emailConfiguration = Resolve<EmailConfiguration>()!;
    }

    public void SendEmail(string recipient, string subject, string body)
    {
        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Cóc Event Company", _emailConfiguration.User));
            message.To.Add(new MailboxAddress("Khách hàng", recipient));
            message.Subject = subject;
            message.Importance = MessageImportance.High;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = body;
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                client.Authenticate(_emailConfiguration.User, _emailConfiguration.ApplicationPassword);
                client.Send(message);
                client.Disconnect(true);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, this);
        }
    }
}