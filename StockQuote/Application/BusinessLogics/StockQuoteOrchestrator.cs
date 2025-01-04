using StockQuote.Application.Commands;
using StockQuote.Application.Interfaces;
using StockQuote.Domain.Enums;

namespace StockQuote.Application.BusinessLogics;
public class StockQuoteOrchestrator
{
    private readonly IAnalyzeStockCommandHandler _analyzeHandler;
    private readonly ISendEmailCommandHandler _emailHandler;
    private readonly IStockQuoteRequestService _stockQuoteRequestService;
    private StockAnalysisState _stockAnalysisState;

    public StockQuoteOrchestrator(IAnalyzeStockCommandHandler analyzeHandler, ISendEmailCommandHandler emailHandler, IStockQuoteRequestService stockQuoteRequestService)
    {
        _analyzeHandler = analyzeHandler;
        _emailHandler = emailHandler;
        _stockQuoteRequestService = stockQuoteRequestService;
        _stockAnalysisState = StockAnalysisState.Hold;
    }

    public async Task ExecuteStockQuoteAnalysis(string symbol, double sellingReferencePrice, double purchaseReferencePrice)
    {
        var stock = await _stockQuoteRequestService.GetStock(symbol);

        var analyseCommand = new AnalyzeStockCommand
        {
            Stock = stock,
            SellingReferencePrice = sellingReferencePrice,
            PurchaseReferencePrice = purchaseReferencePrice
        };

        var resultAnalysis = _analyzeHandler.Handle(analyseCommand);

        if (resultAnalysis is not StockAnalysisState.Hold && resultAnalysis != _stockAnalysisState)
        {
            var sendEmailCommand = new SendEmailCommand
            {
                Subject = $"Cotação de {stock.Symbol}",
                Body = $"O valor de {stock.Symbol} R${stock.RegularMarketPrice} está abaixo do referencial de compra. É uma ótima oportunidade para comprar!"
            };

            if (resultAnalysis == StockAnalysisState.Sell)
                sendEmailCommand.Body = $"O valor de {stock.Symbol} R${stock.RegularMarketPrice} está acima do referencial de venda. É uma ótima oportunidade para vender!";

            await _emailHandler.Handle(sendEmailCommand);
        }

        _stockAnalysisState = resultAnalysis;
    }
}

