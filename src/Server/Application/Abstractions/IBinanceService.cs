namespace FinanceLab.Server.Application.Abstractions;

public interface IBinanceService
{
    Task<double> GetPriceAsync(string symbol);
}