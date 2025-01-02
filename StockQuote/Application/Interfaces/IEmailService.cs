namespace StockQuote.Application.Interfaces;
public interface IEmailService
{
    Task SendAlert(string subject, string body);
}
