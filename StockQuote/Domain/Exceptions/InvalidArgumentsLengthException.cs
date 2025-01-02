namespace StockQuote.Domain.Exceptions;
public class InvalidArgumentsLengthException : ArgumentException
{
    public InvalidArgumentsLengthException(string message) : base(message) { }
}
