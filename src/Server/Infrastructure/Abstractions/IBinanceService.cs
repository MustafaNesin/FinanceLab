namespace FinanceLab.Server.Infrastructure.Abstractions;

public interface IBinanceService
{
    Task<double> GetTickerPriceAsync(string symbol);
}