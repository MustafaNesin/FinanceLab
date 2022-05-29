using System.Security.Claims;
using FinanceLab.Server.Application.Abstractions;
using FinanceLab.Server.Domain.Models.Commands;
using FinanceLab.Server.Domain.Models.Queries;
using FinanceLab.Shared.Application.Constants;
using FinanceLab.Shared.Domain.Models.Inputs;
using FinanceLab.Shared.Domain.Models.Outputs;
using Microsoft.AspNetCore.Mvc;

namespace FinanceLab.Server.Presentation.Controllers;

public sealed class TradeController : BaseController
{
    private readonly IBinanceService _binanceService;
    private readonly HttpContext _httpContext;

    public TradeController(IHttpContextAccessor httpContextAccessor, IBinanceService binanceService)
    {
        _httpContext = httpContextAccessor.HttpContext!;
        _binanceService = binanceService;
    }

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
        var userName = _httpContext.User.FindFirstValue(ClaimTypes.Name);
        var price = await _binanceService.GetTickerPriceAsync(input.BaseCoinCode + input.QuoteCoinCode);
        var request = new TradeCommand(userName, input.Side, input.BaseCoinCode, input.QuoteCoinCode, input.Quantity,
            price);
        await Mediator.Send(request);

        return Ok();
    }
}