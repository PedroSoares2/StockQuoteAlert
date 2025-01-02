using System.Text.Json.Serialization;

namespace StockQuote.Domain.Entities;
public class Stock
{
    [JsonPropertyName("currency")]
    public string Currency { get; set; }    
    
    [JsonPropertyName("symbol")]
    public string Symbol { get; set; }

    [JsonPropertyName("shortName")]
    public string ShortName { get; set; }

    [JsonPropertyName("longName")]
    public string LongName { get; set; }

    [JsonPropertyName("regularMarketPrice")]
    public double RegularMarketPrice { get; set; }
}
