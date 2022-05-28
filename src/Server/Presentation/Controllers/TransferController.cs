using FinanceLab.Shared.Application.Constants;
using FinanceLab.Shared.Domain.Models.Inputs;
using FinanceLab.Shared.Domain.Models.Outputs;
using Microsoft.AspNetCore.Mvc;

namespace FinanceLab.Server.Presentation.Controllers;

public sealed class TransferController : BaseController
{
    [HttpGet(ApiRouteConstants.GetTransferList)]
    public async Task<ActionResult<TransferListOutput>> GetListAsync([FromQuery] TransferListInput input)
        => throw new NotImplementedException();

    [HttpGet(ApiRouteConstants.PostTransfer)]
    public async Task<IActionResult> PostAsync([FromQuery] TransferInput input)
        => throw new NotImplementedException();
}