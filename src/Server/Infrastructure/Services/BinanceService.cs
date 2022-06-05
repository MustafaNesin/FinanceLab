using System.Net.Http.Json;
using System.Text.Json;
using FinanceLab.Server.Application.Abstractions;
using FinanceLab.Server.Infrastructure.Converters;
using FinanceLab.Server.Infrastructure.Models;
using Microsoft.Extensions.Caching.Memory;
using static FinanceLab.Server.Infrastructure.Constants.BinanceApiConstants;

namespace FinanceLab.Server.Infrastructure.Services;

public sealed class BinanceService : IBinanceService
{
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly TimeSpan _cacheExpiration = TimeSpan.FromSeconds(CacheExpirationSeconds);
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IMemoryCache _memoryCache;

    public BinanceService(IMemoryCache memoryCache, IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _memoryCache = memoryCache;
        _jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            Converters = { new DoubleInvariantConverter() }
        };
    }

    public async Task<double> GetTickerPriceAsync(string symbol)
    {
        if (_memoryCache.TryGetValue(symbol, out double value))
            return value;

        var httpClient = _httpClientFactory.CreateClient(ClientName);
        var tickerPrices = await httpClient.GetFromJsonAsync<IReadOnlyCollection<TickerPrice>>(TickerPriceRoute, 
            _jsonSerializerOptions);

        foreach (var tickerPrice in tickerPrices!)
            _memoryCache.Set(tickerPrice.Symbol, tickerPrice.Price, _cacheExpiration);

        return _memoryCache.Get<double>(symbol);
    }
}