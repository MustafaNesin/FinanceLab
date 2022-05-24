using System.Text.Json;
using FinanceLab.Server.Application.Abstractions;
using FinanceLab.Server.Domain.Models.Entities;
using FinanceLab.Shared.Application.Constants;
using Microsoft.Extensions.Caching.Memory;

namespace FinanceLab.Server.Application.Services;

public class BinanceService : IBinanceService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IMemoryCache _memoryCache;

    public BinanceService(IMemoryCache memoryCache, IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _memoryCache = memoryCache;
    }

    public async Task<double> GetPriceAsync(string symbol)
    {
        double value;

        if (_memoryCache.TryGetValue(symbol, out value))
            return value;
        var httpClient = _httpClientFactory.CreateClient("binance");

        var streamTask = await httpClient.GetStreamAsync(BinanceApiRouteConstants.TickerEndpoint);
        var coins = await JsonSerializer.DeserializeAsync<List<Coin>>(streamTask,
            new JsonSerializerOptions(JsonSerializerDefaults.Web));

        foreach (var coin in coins)
            _memoryCache.Set(coin.Symbol, coin.Price, TimeSpan.FromSeconds(10));

        return _memoryCache.Get<double>(symbol);
    }
}