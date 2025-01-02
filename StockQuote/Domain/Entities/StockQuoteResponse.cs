using System.Text.Json.Serialization;

namespace StockQuote.Domain.Entities
{
    public class StockQuoteResponse
    {
        [JsonPropertyName("results")]
        public dynamic Results { get; set; }
    }
}
