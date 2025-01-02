using StockQuote.Application.Interfaces;
using StockQuote.Domain.Entities;
using StockQuote.Domain.Enums;

namespace StockQuote.Application.Services;
public class StockAnalysis : IStockAnalysis
{
    public StockAnalysisState GetStockAnalysisState(Stock stock, double purchaseReferencePrice, double sellingReferencePrice)
    {
        if (stock.RegularMarketPrice > sellingReferencePrice)
            return StockAnalysisState.Sell;
        else if(stock.RegularMarketPrice < purchaseReferencePrice)
            return StockAnalysisState.Buy;

        return StockAnalysisState.Hold;
    }
}
