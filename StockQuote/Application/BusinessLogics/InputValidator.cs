using StockQuote.Domain.Exceptions;

namespace StockQuote.Application.BusinessLogics;
public class InputValidator
{
    public static void Validate(string[] args)
    {
        ValidateArgumentsLength(args);
        ValidateNegativeNumbers(args[1], "SellingReferencePrice");
        ValidateNegativeNumbers(args[2], "PurchaseReferencePrice");
    }

    private static void ValidateArgumentsLength(string[] args)
    {
        if (args.Length != 3) throw new InvalidArgumentsLengthException("Esperado 3 argumentos: TICKET  SELLINGREFERENCEPRICE   PURCHASEREFERENCEPRICE");
    }

    private static void ValidateNegativeNumbers(string input, string argumentName)
    {
        if (!double.TryParse(input, out double number)) throw new InvalidNumberFormatException(argumentName);

        if (number < 0) throw new NegativeNumberException(argumentName);
    }
}
