namespace StockQuote.Domain.Exceptions;
public class InvalidNumberFormatException : ArgumentException
{
    public InvalidNumberFormatException(string argumentName): base($"{argumentName} precisa ser um número válido.") { }
}
