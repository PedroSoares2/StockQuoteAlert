using Microsoft.Extensions.Options;
using StockQuote.Application.Interfaces;
using StockQuote.Domain.Entities;
using System.Net.Http.Headers;
using System.Text.Json;

namespace StockQuote.Infrastructure.Services;
public class StockQuoteRequestService : IStockQuoteRequestService
{
    private readonly HttpClient _httpClient;
    private readonly ApiSettings _apiSettings;

    public StockQuoteRequestService(HttpClient httpClient, IOptions<ApiSettings> apiSettings)
    {
        _httpClient = httpClient;
        _apiSettings = apiSettings.Value;
    }

    public async Task<Stock> GetStock(string symbol)
    {
        try
        {
            var url = $"{_apiSettings.BaseUrl}/{symbol}?range=1d&interval=1d";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiSettings.ApiToken);

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
