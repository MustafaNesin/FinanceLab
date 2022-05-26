using FinanceLab.Server.Domain.Models.Queries;
using FinanceLab.Shared.Application.Constants;
using Microsoft.AspNetCore.Mvc;

namespace FinanceLab.Server.Presentation.Controllers;

public class WalletController : BaseController
{
    [HttpGet(ApiRouteConstants.WalletGet)]
    public async Task<IActionResult> GetListAsync([FromQuery] string username)
    {
        if (User.Identity.Name.Equals(username))
        {
            var UserId = username;
            var request = new GetUserWalletQuery(UserId);
            var userWallet = Mediator.Send(request);
            return Ok(userWallet);
        }

        return BadRequest();
    }
}