using FinanceLab.Server.Domain.Models.Commands;
using FinanceLab.Server.Domain.Models.Queries;
using FinanceLab.Shared.Application.Constants;
using FinanceLab.Shared.Domain.Models.Inputs;
using FinanceLab.Shared.Domain.Models.Outputs;
using Microsoft.AspNetCore.Mvc;

namespace FinanceLab.Server.Presentation.Controllers;

public sealed class TradeController : BaseController
{
    [HttpGet(ApiRouteConstants.GetTradeList)]
    public async Task<ActionResult<TradeListOutput>> GetListAsync([FromQuery] TradeListInput input)
    {
        var userName = EnsureAuthorizationForUserName(input.UserName);
        var request = new GetTradeListQuery(input.UserName, input.Filter, input.Page, input.Sort);
        var tradeList = await Mediator.Send(request);

        return Ok(tradeList);
    }

    [HttpGet(ApiRouteConstants.PostTrade)]
    public async Task<ActionResult<TradeOutput>> PostAsync([FromQuery] TradeInput input)
    {
        var request = new TradeCommand(input.Side, input.BaseCoinCode, input.QuoteCoinCode, input.Amount);
        await Mediator.Send(request);

        return Ok();
    }
}