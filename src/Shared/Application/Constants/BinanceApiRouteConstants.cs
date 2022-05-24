namespace FinanceLab.Shared.Application.Constants;

public static class BinanceApiRouteConstants
{
    public const string BinanceBase = "https://api.binance.com";

    private const string GetTicker = "/api/v3/ticker/price";

    private const string TickerSymbol = "/api/v3/ticker/price";

    public static string TickerEndpoint = GetTicker;

    public static string GetTickerEndpoint(string symbolCode) => $"{GetTicker}?symbol={symbolCode}";
}