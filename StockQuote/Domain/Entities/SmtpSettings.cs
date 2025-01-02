namespace StockQuote.Domain.Entities;
public class SmtpSettings
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string Sender { get; set; }
    public string Password { get; set; }
    public string Recipient { get; set; }
}