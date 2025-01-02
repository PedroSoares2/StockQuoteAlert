using StockQuote.Application.Commands;

namespace StockQuote.Application.Interfaces;
public interface ISendEmailCommandHandler
{
    public Task Handle(SendEmailCommand command);
}
