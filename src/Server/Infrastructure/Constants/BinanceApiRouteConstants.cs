namespace FinanceLab.Server.Infrastructure.Constants;

public static class BinanceApiConstants
{
    public const string ClientName = "Binance";
    public const string BaseAddress = "https://api.binance.com";
    public const string TickerPriceRoute = "/api/v3/ticker/price";
    public const int CacheExpirationSeconds = 10;
}