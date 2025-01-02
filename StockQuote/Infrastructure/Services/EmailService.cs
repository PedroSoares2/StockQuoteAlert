using Microsoft.Extensions.Options;
using StockQuote.Application.Interfaces;
using StockQuote.Domain.Entities;
using System.Net;
using System.Net.Mail;

namespace StockQuote.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _smtpSettings;
        public EmailService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task SendAlert(string subject, string body)
        {
            try
            {
                var client = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port)
                {
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(_smtpSettings.Sender, _smtpSettings.Password),
                };

                var mailMessage = new MailMessage(from: _smtpSettings.Sender, to: _smtpSettings.Recipient, subject: subject, body: body)
                {
                    IsBodyHtml = true
                };

                await client.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
