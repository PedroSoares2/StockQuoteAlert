using System.Text.Json.Serialization;

namespace StockQuote.Domain.Entities
{
    public class ResponseContent
    {
        [JsonPropertyName("results")]
        public dynamic Results { get; set; }
    }
}
