namespace StockQuote.Application.Interfaces;
public interface IEmailService
{
    Task SendAlert(string toEmail, string subject, string body);
}
