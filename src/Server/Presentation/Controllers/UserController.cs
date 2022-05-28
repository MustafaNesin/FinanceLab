using System.Net;
using System.Security.Claims;
using FinanceLab.Server.Domain.Models.Commands;
using FinanceLab.Server.Domain.Models.Queries;
using FinanceLab.Shared.Application.Constants;
using FinanceLab.Shared.Domain.Models.Inputs;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceLab.Server.Presentation.Controllers;

public class UserController : BaseController
{
    [HttpGet(ApiRouteConstants.GetUser)]
    public async Task<IActionResult> GetAsync([FromRoute] string? userName)
    {
        userName ??= HttpContext.User.FindFirstValue(ClaimTypes.Name);

        var request = new GetUserQuery(userName);
        var user = await Mediator.Send(request);
        return Ok(user);
    }

    [Authorize(Roles = RoleConstants.Admin)]
    [HttpGet(ApiRouteConstants.GetUserList)]
    public async Task<IActionResult> GetListAsync([FromQuery] UserListInput input)
    {
        var request = new GetUserListQuery(input.Page, input.PageSize, input.Search, input.Sort, input.SortDirection);
        var userList = await Mediator.Send(request);
        return Ok(userList);
    }

    [HttpPost(ApiRouteConstants.NewGame)]
    public async Task<IActionResult> NewGameAsync([FromRoute] string userName, [FromQuery] NewGameInput input)
    {
        var signedInUserRole = HttpContext.User.FindFirstValue(ClaimTypes.Role);
        var signedInUserName = HttpContext.User.FindFirstValue(ClaimTypes.Name);

        // Giriş yapmış kullanıcı admin değilse başkasının oyununu yeniden başlatamaz
        if (signedInUserRole is not RoleConstants.Admin && !userName.Equals(signedInUserName))
            throw new ProblemDetailsException((int)HttpStatusCode.Unauthorized);

        var request = new NewGameCommand(userName, input.GameDifficulty);
        await Mediator.Send(request);
        return Ok();
    }
}