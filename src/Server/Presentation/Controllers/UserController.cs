using System.Security.Claims;
using FinanceLab.Server.Domain.Models.Queries;
using FinanceLab.Shared.Application.Constants;
using FinanceLab.Shared.Domain.Constants;
using FinanceLab.Shared.Domain.Models.Inputs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceLab.Server.Presentation.Controllers;

public class UserController : BaseController
{
    [HttpGet(ApiRouteConstants.GetUser)]
    public async Task<IActionResult> GetAsync(string? userName)
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
}