using StockQuote.Domain.Entities;

namespace StockQuote.Application.Commands;
public class AnalyzeStockCommand
{
    public Stock Stock { get; set; }
    public double PurchaseReferencePrice { get; set; }
    public double SellingReferencePrice { get; set; }

}
