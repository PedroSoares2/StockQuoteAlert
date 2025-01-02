using StockQuote.Application.Interfaces;
using StockQuote.Domain.Entities;
using System.Net.Http.Headers;
using System.Text.Json;

namespace StockQuote.Infrastructure.Services;
public class StockQuoteRequestService : IStockQuoteRequestService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiBaseUrl;
    private readonly string _token;

    public StockQuoteRequestService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _apiBaseUrl = "https://brapi.dev/api/quote";
        _token = "m9GigZdogMmh8Q2BcKD716";
    }

    public async Task<Stock> GetStock(string symbol, int range, int interval)
    {
        try
        {
            var url = $"{_apiBaseUrl}/{symbol}?range={range}d&interval={interval}d";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var responseContent = await _httpClient.GetAsync(url).Result.Content.ReadAsStringAsync();

            var jsonData = JsonSerializer.Deserialize<StockQuoteResponse>(responseContent);

            var stock = JsonSerializer.Deserialize<Stock>(jsonData.Results[0]);

            return stock;
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }
    }
}
