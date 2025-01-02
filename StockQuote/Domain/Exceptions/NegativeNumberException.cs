public class NegativeNumberException : ArgumentException
{
    public NegativeNumberException(string argumentName): base($"{argumentName} não pode ser negativo.") { }
}