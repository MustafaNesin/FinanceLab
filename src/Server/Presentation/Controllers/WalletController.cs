using System.Net;
using System.Security.Claims;
using FinanceLab.Server.Domain.Models.Queries;
using FinanceLab.Shared.Application.Constants;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;

namespace FinanceLab.Server.Presentation.Controllers;

public class WalletController : BaseController
{
    [HttpGet(ApiRouteConstants.GetWallet)]
    public async Task<IActionResult> GetAsync([FromRoute] string userName)
    {
        var signedInUserRole = HttpContext.User.FindFirstValue(ClaimTypes.Role);
        var signedInUserName = HttpContext.User.FindFirstValue(ClaimTypes.Name);

        // Giriş yapmış kullanıcı admin değilse başkasının cüzdanını sorgulayamaz
        if (signedInUserRole is not RoleConstants.Admin && !userName.Equals(signedInUserName))
            throw new ProblemDetailsException((int)HttpStatusCode.Unauthorized);

        var query = new GetWalletListQuery(userName);
        var wallet = await Mediator.Send(query);
        return Ok(wallet);
    }
}