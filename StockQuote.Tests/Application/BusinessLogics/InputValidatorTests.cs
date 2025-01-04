using StockQuote.Application.BusinessLogics;
using StockQuote.Domain.Exceptions;

namespace StockQuote.Tests.Application.BusinessLogics;
public class InputValidatorTests
{
    [Fact]
    public void Validate_WhenNumberOfArgumentsIsCorrect_ThenShouldNotThrowException()
    {
        var args = new[] { "PETR4", "100", "50" };

        var exception = Record.Exception(() => InputValidator.Validate(args));
        Assert.Null(exception);
    }

    [Fact]
    public void Validate_WhenNumberOfArgumentsIsIncorrect_ThenShouldThrowInvalidArgumentsLengthException()
    {
        var args = new[] { "PETR4", "100"};

        var exception = Record.Exception(() => InputValidator.Validate(args));

        Assert.NotNull(exception);
        Assert.IsType<InvalidArgumentsLengthException>(exception);
    }

    [Fact]
    public void Validate_WhenNoArguments_ThenShouldThrowInvalidArgumentsLengthException()
    {
        var args = Array.Empty<string>();

        var exception = Record.Exception(() => InputValidator.Validate(args));

        Assert.NotNull(exception);
        Assert.IsType<InvalidArgumentsLengthException>(exception);
    }

    [Fact]
    public void Validate_WhenArgumentNumberIsInvalid_ThenShouldThrowInvalidNumberFormatException()
    {
        var args = new[] { "PETR4", "100", "7D" };

        var exception = Record.Exception(() => InputValidator.Validate(args));

        Assert.NotNull(exception);
        Assert.IsType<InvalidNumberFormatException>(exception);
    }

    [Fact]
    public void Validate_WhenArgumentNumberIsNegative_ThenShouldThrowNegativeNumberException()

    {
        var args = new[] { "PETR4", "-100", "50" };

        var exception = Record.Exception(() => InputValidator.Validate(args));

        Assert.NotNull(exception);
        Assert.IsType<NegativeNumberException>(exception);
    }
}
