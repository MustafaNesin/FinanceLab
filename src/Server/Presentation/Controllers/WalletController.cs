using System.Net;
using System.Security.Claims;
using FinanceLab.Server.Domain.Models.Queries;
using FinanceLab.Shared.Application.Constants;
using FinanceLab.Shared.Domain.Constants;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;

namespace FinanceLab.Server.Presentation.Controllers;

public class WalletController : BaseController
{
    [HttpGet(ApiRouteConstants.WalletGet)]
    public async Task<IActionResult> GetAsync([FromRoute] string userName)
    {
        var signedInUserRole = HttpContext.User.FindFirstValue(ClaimTypes.Role);
        var signedInUserName = HttpContext.User.FindFirstValue(ClaimTypes.Name);

        // Giriş yapmış kullanıcı admin değilse başkasının cüzdanını sorgulayamaz
        if (signedInUserRole is not RoleConstants.Admin && !userName.Equals(signedInUserName))
        {
            var problemDetails = StatusCodeProblemDetails.Create((int)HttpStatusCode.Unauthorized);
            return Unauthorized(problemDetails);
        }

        var getUserWalletQuery = new GetUserWalletQuery(userName);
        var userWallet = await Mediator.Send(getUserWalletQuery);
        return Ok(userWallet);
    }
}