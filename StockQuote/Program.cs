using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StockQuote.Application.BusinessLogics;
using StockQuote.Application.Handlers;
using StockQuote.Application.Interfaces;
using StockQuote.Application.Services;
using StockQuote.Infrastructure.Services;
using System.Globalization;

class Program
{
    static void Main(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.AddTransient<HttpClient>();
                services.AddTransient<StockQuoteOrchestrator>();
                services.AddTransient<IEmailService, EmailService>();
                services.AddTransient<IStockQuoteRequestService, StockQuoteRequestService>();
                services.AddTransient<IStockAnalysis, StockAnalysis>();

                services.AddTransient<IAnalyzeStockCommandHandler, AnalyzeStockCommandHandler>();
                services.AddTransient<ISendEmailCommandHandler, SendEmailCommandHandler>();

                services.AddHangfire(config =>
                {
                    config.UseMemoryStorage();
                });
                services.AddHangfireServer();
            });

        var host = builder.Build();

        string symbol = args[0].ToUpper();
        Double.TryParse(args[1], NumberStyles.Any, CultureInfo.InvariantCulture, out double targetHigh);
        Double.TryParse(args[2], NumberStyles.Any, CultureInfo.InvariantCulture, out double targetLow);

        // validaçoes

        using (var scope = host.Services.CreateScope())
        {
            var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
            var stockOrchestrator = scope.ServiceProvider.GetRequiredService<StockQuoteOrchestrator>();

            recurringJobManager.AddOrUpdate(
                "ProcessStockQuoteAnalysisJob",
                () => stockOrchestrator.ProcessStockQuoteAnalysis("PETR4", targetLow, targetHigh),
                "*/1 * * * *"
            );
        }

        host.Run();
    }
}
