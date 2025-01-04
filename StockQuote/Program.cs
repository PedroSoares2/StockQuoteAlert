using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StockQuote.Application.BusinessLogics;
using StockQuote.Application.Handlers;
using StockQuote.Application.Interfaces;
using StockQuote.Application.Services;
using StockQuote.Domain.Entities;
using StockQuote.Infrastructure.Services;
using System.Globalization;

class Program
{
    const string JOB_TIME = "1";
    static void Main(string[] args)
    {
        InputValidator.Validate(args);

        string symbol = args[0].ToUpper();
        Double.TryParse(args[1], NumberStyles.Any, CultureInfo.InvariantCulture, out double sellingReferencePrice);
        Double.TryParse(args[2], NumberStyles.Any, CultureInfo.InvariantCulture, out double purchaseReferencePrice);

        var builder = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                services.AddSingleton<IConfiguration>(configuration);

                services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));
                services.Configure<ApiSettings>(configuration.GetSection("ApiSettings"));

                services.AddTransient<HttpClient>();
                services.AddSingleton<StockQuoteOrchestrator>();
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

        using (var scope = host.Services.CreateScope())
        {
            var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
            var stockOrchestrator = scope.ServiceProvider.GetRequiredService<StockQuoteOrchestrator>();

            recurringJobManager.AddOrUpdate(
                "ProcessStockQuoteAnalysisJob",
                () => stockOrchestrator.ExecuteStockQuoteAnalysis(symbol, sellingReferencePrice, purchaseReferencePrice),
                $"*/{JOB_TIME} * * * *"
            );
        }

        host.Run();
    }
}
