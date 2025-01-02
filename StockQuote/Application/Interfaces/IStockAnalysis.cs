using StockQuote.Domain.Entities;
using StockQuote.Domain.Enums;

namespace StockQuote.Application.Interfaces;
public interface IStockAnalysis
{
    public StockAnalysisState GetStockAnalysisState (Stock stock, double purchaseReferencePrice, double sellingReferencePrice);
}
