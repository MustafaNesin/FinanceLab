using JetBrains.Annotations;

namespace FinanceLab.Server.Domain.Models.Entities;

[PublicAPI]
public record Market(string Symbol, string BaseCoinCode, string QuoteCoinCode)
{
    public Market(string baseCoinCode, string quoteCoinCode)
        : this(baseCoinCode + quoteCoinCode, baseCoinCode, quoteCoinCode)
    {
    }
}