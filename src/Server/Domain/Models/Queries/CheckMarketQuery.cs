using MediatR;

namespace FinanceLab.Server.Domain.Models.Queries;

public class CheckMarketQuery : IRequest<bool>
{
    public string BaseCoinCode { get; set; }
    public string QuoteCoinCode { get; set; }

    public CheckMarketQuery(string baseCoinCode, string quoteCoinCode)
    {
        BaseCoinCode = baseCoinCode;
        QuoteCoinCode = quoteCoinCode;
    }
}