using System.Security.Claims;
using FinanceLab.Server.Domain.Models.Commands;
using FinanceLab.Server.Domain.Models.Queries;
using FinanceLab.Shared.Application.Constants;
using FinanceLab.Shared.Domain.Models.Dtos;
using FinanceLab.Shared.Domain.Models.Inputs;
using FinanceLab.Shared.Domain.Models.Outputs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceLab.Server.Presentation.Controllers;

public sealed class UserController : BaseController
{
    [HttpGet(ApiRouteConstants.GetUser)]
    public async Task<ActionResult<UserDto>> GetAsync([FromQuery] UserNameInput input)
    {
        // Her kullanıcı birbirinin bilgilerini görüntüleyebilir
        var userName = input.UserName ?? HttpContext.User.FindFirstValue(ClaimTypes.Name);
        var request = new GetUserQuery(userName);
        var user = await Mediator.Send(request);

        return Ok(user);
    }

    [Authorize(Roles = RoleConstants.Admin)]
    [HttpGet(ApiRouteConstants.GetUserList)]
    public async Task<ActionResult<UserListOutput>> GetListAsync([FromQuery] UserListInput input)
    {
        var request = new GetUserListQuery(input.Filter, input.Page, input.Sort);
        var userList = await Mediator.Send(request);

        return Ok(userList);
    }

    [HttpPost(ApiRouteConstants.NewGame)]
    public async Task<IActionResult> NewGameAsync([FromQuery] NewGameInput input)
    {
        var userName = EnsureAuthorizationForUserName(input.UserName);
        var request = new NewGameCommand(userName, input.GameDifficulty);
        await Mediator.Send(request);

        return Ok();
    }
}