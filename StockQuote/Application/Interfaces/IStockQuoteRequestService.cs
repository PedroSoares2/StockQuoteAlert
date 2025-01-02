using StockQuote.Domain.Entities;

namespace StockQuote.Application.Interfaces;
public interface IStockQuoteRequestService
{
    Task<Stock> GetStock(string symbol, int range, int interval);
}
