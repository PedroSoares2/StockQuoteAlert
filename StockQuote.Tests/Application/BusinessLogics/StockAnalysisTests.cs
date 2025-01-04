using StockQuote.Application.Services;
using StockQuote.Domain.Entities;
using StockQuote.Domain.Enums;

namespace StockQuote.Tests.Application.BusinessLogics
{
    public class StockAnalysisTests
    {
        private readonly StockAnalysis _stockAnalysis;
        public StockAnalysisTests()
        {
            _stockAnalysis = new StockAnalysis(); ;
        }

        [Fact]
        public void GetStockAnalysisState_WhenPriceExceedsSellingReference_ThenShouldSell()
        {
            var stock = new Stock { RegularMarketPrice = 40 };
            var purchaseReferencePrice = 35;
            var sellingReferencePrice = 39;

            var result = _stockAnalysis.GetStockAnalysisState(stock, purchaseReferencePrice, sellingReferencePrice);

            Assert.Equal(StockAnalysisState.Sell, result);
        }

        [Fact]
        public void GetStockAnalysisState_WhenPriceIsBelowPurchaseReference_ThenShouldBuy()
        {
            var stock = new Stock { RegularMarketPrice = 40 };
            var purchaseReferencePrice = 45;
            var sellingReferencePrice = 50;

            var result = _stockAnalysis.GetStockAnalysisState(stock, purchaseReferencePrice, sellingReferencePrice);

            Assert.Equal(StockAnalysisState.Buy, result);
        }

        [Fact]
        public void GetStockAnalysisState_WhenPriceIsBetweenPurchaseAndSellingReferences_ThenShouldHold()
        {
            var stock = new Stock { RegularMarketPrice = 40 };
            var purchaseReferencePrice = 35;
            var sellingReferencePrice = 45;

            var result = _stockAnalysis.GetStockAnalysisState(stock, purchaseReferencePrice, sellingReferencePrice);

            Assert.Equal(StockAnalysisState.Hold, result);
        }
    }
}
