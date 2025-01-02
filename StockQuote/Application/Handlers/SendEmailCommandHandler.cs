using StockQuote.Application.Commands;
using StockQuote.Application.Interfaces;

namespace StockQuote.Application.Handlers;
public class SendEmailCommandHandler : ISendEmailCommandHandler
{
    private readonly IEmailService _emailService;
    public SendEmailCommandHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Handle(SendEmailCommand command)
    {
        await _emailService.SendAlert(command.ToEmail, command.Subject, command.Body);
    }
}
