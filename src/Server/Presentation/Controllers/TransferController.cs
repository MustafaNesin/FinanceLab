using System.Security.Claims;
using FinanceLab.Server.Domain.Models.Commands;
using FinanceLab.Server.Domain.Models.Queries;
using FinanceLab.Shared.Application.Constants;
using FinanceLab.Shared.Domain.Models.Inputs;
using FinanceLab.Shared.Domain.Models.Outputs;
using Microsoft.AspNetCore.Mvc;

namespace FinanceLab.Server.Presentation.Controllers;

public sealed class TransferController : BaseController
{
    private readonly HttpContext _httpContext;

    public TransferController(IHttpContextAccessor httpContextAccessor)
        => _httpContext = httpContextAccessor.HttpContext!;

    [HttpGet(ApiRouteConstants.GetTransferList)]
    public async Task<ActionResult<TransferListOutput>> GetListAsync([FromQuery] TransferListInput input)
    {
        var userName = EnsureAuthorizationForUserName(input.UserName);
        var request = new GetTransferListQuery(input.UserName, input.Filter, input.Page, input.Sort);
        var transferList = await Mediator.Send(request);

        return Ok(transferList);
    }

    [HttpGet(ApiRouteConstants.PostTransfer)]
    public async Task<IActionResult> PostAsync([FromQuery] TransferInput input)
    {
        var userName = _httpContext.User.FindFirstValue(ClaimTypes.Name);
        var request = new TransferCommand(userName, input.CoinCode, input.Amount);
        await Mediator.Send(request);

        return Ok();
    }
}