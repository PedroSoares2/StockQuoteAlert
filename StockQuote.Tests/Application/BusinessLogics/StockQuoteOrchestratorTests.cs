using Moq;
using StockQuote.Application.BusinessLogics;
using StockQuote.Application.Commands;
using StockQuote.Application.Interfaces;
using StockQuote.Domain.Entities;
using StockQuote.Domain.Enums;

namespace StockQuote.Tests.Application.BusinessLogics;

public class StockQuoteOrchestratorTests
{
    private readonly Mock<IAnalyzeStockCommandHandler> _mockAnalyzeHandler;
    private readonly Mock<ISendEmailCommandHandler> _mockEmailHandler;
    private readonly Mock<IStockQuoteRequestService> _mockRequestService;
    private readonly StockQuoteOrchestrator _orchestrator;

    public StockQuoteOrchestratorTests()
    {
        _mockAnalyzeHandler = new Mock<IAnalyzeStockCommandHandler>();
        _mockEmailHandler = new Mock<ISendEmailCommandHandler>();
        _mockRequestService = new Mock<IStockQuoteRequestService>();

        _orchestrator = new StockQuoteOrchestrator(
            _mockAnalyzeHandler.Object,
            _mockEmailHandler.Object,
            _mockRequestService.Object
        );
    }

    [Fact]
    public async Task ExecuteStockQuoteAnalysiss_WhenCall_ThenShouldGetStock()
    {
        var stock = new Stock { Symbol = "PETR4", RegularMarketPrice = 150.0 };
        _mockRequestService.Setup(x => x.GetStock(It.IsAny<string>())).ReturnsAsync(stock);

        await _orchestrator.ExecuteStockQuoteAnalysis("PETR4", 100, 200);

        _mockRequestService.Verify(x => x.GetStock("PETR4"), Times.Once);
    }

    [Fact]
    public async Task ExecuteStockQuoteAnalysiss_WhenCall_ThenShouldHandleAnalyzeCommand()
    {
        var stock = new Stock { Symbol = "PETR4", RegularMarketPrice = 150.0 };
        _mockRequestService.Setup(x => x.GetStock(It.IsAny<string>())).ReturnsAsync(stock);

        await _orchestrator.ExecuteStockQuoteAnalysis("PETR4", 100, 200);

        _mockAnalyzeHandler.Verify(x => x.Handle(It.IsAny<AnalyzeStockCommand>()), Times.Once);
    }

    [Fact]
    public async Task ExecuteStockQuoteAnalysis_WhenStateIsBuy_ThenShouldSendEmail()
    {
        var stock = new Stock { Symbol = "PETR4", RegularMarketPrice = 150.0 };
        _mockRequestService.Setup(x => x.GetStock(It.IsAny<string>())).ReturnsAsync(stock);
        _mockAnalyzeHandler.Setup(x => x.Handle(It.IsAny<AnalyzeStockCommand>())).Returns(StockAnalysisState.Buy);

        await _orchestrator.ExecuteStockQuoteAnalysis("PETR4", 100, 200);

        _mockEmailHandler.Verify(x => x.Handle(It.IsAny<SendEmailCommand>()), Times.Once);
    }

    [Fact]
    public async Task ExecuteStockQuoteAnalysis_WhenStateIsSell_ThenShouldSendEmail()
    {
        var stock = new Stock { Symbol = "PETR4", RegularMarketPrice = 150.0 };
        _mockRequestService.Setup(x => x.GetStock(It.IsAny<string>())).ReturnsAsync(stock);
        _mockAnalyzeHandler.Setup(x => x.Handle(It.IsAny<AnalyzeStockCommand>())).Returns(StockAnalysisState.Sell);

        await _orchestrator.ExecuteStockQuoteAnalysis("PETR4", 100, 200);

        _mockEmailHandler.Verify(x => x.Handle(It.IsAny<SendEmailCommand>()), Times.Once);
    }

    [Fact]
    public async Task ExecuteStockQuoteAnalysis_WhenStateIsHold_ThenShouldNotSendEmail()
    {
        var stock = new Stock { Symbol = "PETR4", RegularMarketPrice = 150.0 };
        _mockRequestService.Setup(x => x.GetStock(It.IsAny<string>())).ReturnsAsync(stock);
        _mockAnalyzeHandler.Setup(x => x.Handle(It.IsAny<AnalyzeStockCommand>())).Returns(StockAnalysisState.Hold);

        await _orchestrator.ExecuteStockQuoteAnalysis("PETR4", 100, 200);

        _mockEmailHandler.Verify(x => x.Handle(It.IsAny<SendEmailCommand>()), Times.Never);
    }
}
