using FinanceLab.Shared.Application.Constants;
using FinanceLab.Shared.Domain.Models.Inputs;
using FinanceLab.Shared.Domain.Models.Outputs;
using Microsoft.AspNetCore.Mvc;

namespace FinanceLab.Server.Presentation.Controllers;

public sealed class TradeController : BaseController
{
    [HttpGet(ApiRouteConstants.GetTradeList)]
    public async Task<ActionResult<TradeListOutput>> GetListAsync([FromQuery] TradeListInput input)
        => throw new NotImplementedException();

    [HttpGet(ApiRouteConstants.PostTrade)]
    public async Task<ActionResult<TradeOutput>> PostAsync([FromQuery] TradeInput input)
        => throw new NotImplementedException();
}