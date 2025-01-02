using StockQuote.Application.Commands;
using StockQuote.Application.Interfaces;
using StockQuote.Domain.Enums;

namespace StockQuote.Application.BusinessLogics;
public class StockQuoteOrchestrator
{
    private readonly IAnalyzeStockCommandHandler _analyzeHandler;
    private readonly ISendEmailCommandHandler _emailHandler;
    private readonly IStockQuoteRequestService _stockQuoteRequestService;

    public StockQuoteOrchestrator(IAnalyzeStockCommandHandler analyzeHandler, ISendEmailCommandHandler emailHandler, IStockQuoteRequestService stockQuoteRequestService)
    {
        _analyzeHandler = analyzeHandler;
        _emailHandler = emailHandler;
        _stockQuoteRequestService = stockQuoteRequestService;
    }

    public async Task ProcessStockQuoteAnalysis(string symbol, double purchaseReferencePrice, double sellingReferencePrice)
    {
        var stock = await _stockQuoteRequestService.GetStock(symbol, 1, 1);

        var analyseCommand = new AnalyzeStockCommand
        {
            Stock = stock,
            PurchaseReferencePrice = purchaseReferencePrice,
            SellingReferencePrice = sellingReferencePrice
        };

        var resultAnalysis = _analyzeHandler.Handle(analyseCommand);

        if(resultAnalysis is not StockAnalysisState.Hold)
        {
            var sendEmailCommand = new SendEmailCommand
            {
                ToEmail = "pedro_paulo400@hotmail.com",
                Subject = $"Cotação de {stock.Symbol}",
                Body = "É uma ótima oportunidade para comprar"
            };

            if (resultAnalysis == StockAnalysisState.Sell)
                sendEmailCommand.Body = "É uma ótima oportunidade para vender";

            await _emailHandler.Handle(sendEmailCommand);
        }
    }
}
