namespace FinanceLab.Server.Application.Abstractions;

public interface IBinanceService
{
    Task<double> GetTickerPriceAsync(string symbol);
}