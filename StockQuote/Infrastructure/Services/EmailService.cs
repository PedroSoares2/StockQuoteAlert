using StockQuote.Application.Interfaces;
using System.Net;
using System.Net.Mail;

namespace StockQuote.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendAlert(string toEmail, string subject, string body)
        {
            var fromEmail = "pedropauloss12@gmail.com";
            var password = "wjossmnkwcptxltd";

            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromEmail, password),
                };

                var mailMessage = new MailMessage(from: fromEmail, to: toEmail, subject: subject, body: body)
                {
                    IsBodyHtml = true
                };

                client.Send(mailMessage);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}
