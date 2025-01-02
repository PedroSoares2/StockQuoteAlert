using StockQuote.Application.Commands;
using StockQuote.Application.Interfaces;
using StockQuote.Domain.Enums;

namespace StockQuote.Application.Handlers;
public class AnalyzeStockCommandHandler : IAnalyzeStockCommandHandler
{
    private readonly IStockAnalysis _stockAnalysis;
    public AnalyzeStockCommandHandler(IStockAnalysis stockAnalysis)
    {
        _stockAnalysis = stockAnalysis;
    }

    public StockAnalysisState Handle(AnalyzeStockCommand command)
    {
        return _stockAnalysis.GetStockAnalysisState(command.Stock, command.PurchaseReferencePrice, command.SellingReferencePrice);
    }
}

