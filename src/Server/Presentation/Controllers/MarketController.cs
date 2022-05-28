using FinanceLab.Server.Domain.Models.Queries;
using FinanceLab.Shared.Application.Constants;
using FinanceLab.Shared.Domain.Models.Inputs;
using FinanceLab.Shared.Domain.Models.Outputs;
using Microsoft.AspNetCore.Mvc;

namespace FinanceLab.Server.Presentation.Controllers;

public sealed class MarketController : BaseController
{
    [HttpGet(ApiRouteConstants.GetMarketList)]
    public async Task<ActionResult<MarketListOutput>> GetListAsync([FromQuery] MarketListInput input)
    {
        var request = new GetMarketListQuery(input.Filter, input.Page, input.Sort);
        var marketList = await Mediator.Send(request);

        return Ok(marketList);
    }
}