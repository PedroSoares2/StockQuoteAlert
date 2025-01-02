using StockQuote.Application.Commands;
using StockQuote.Domain.Enums;

namespace StockQuote.Application.Interfaces;
public interface IAnalyzeStockCommandHandler
{
    public StockAnalysisState Handle(AnalyzeStockCommand command);
}
