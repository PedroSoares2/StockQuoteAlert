namespace StockQuote.Application.Commands;
public class SendEmailCommand
{
    public string ToEmail { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}
