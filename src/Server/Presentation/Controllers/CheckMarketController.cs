using FinanceLab.Server.Domain.Models.Queries;
using FinanceLab.Shared.Application.Constants;
using Microsoft.AspNetCore.Mvc;

namespace FinanceLab.Server.Presentation.Controllers;

public class CheckMarketController : BaseController
{
    [HttpGet(ApiRouteConstants.CheckMarketExist)]
    public async Task<IActionResult> CheckMarketAsync([FromQuery] string BaseCoinCode, [FromQuery] string QuoteCoinCode)
    {
        var request = new CheckMarketQuery(BaseCoinCode, QuoteCoinCode);
        var result = await Mediator.Send(request);
        return Ok(result);
    }
    
}