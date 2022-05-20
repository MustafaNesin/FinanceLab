using FinanceLab.Server.Domain.Models.Commands;
using FinanceLab.Server.Domain.Models.Queries;
using FinanceLab.Shared.Application.Constants;
using FinanceLab.Shared.Domain.Constants;
using FinanceLab.Shared.Domain.Models.Inputs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceLab.Server.Presentation.Controllers;

[AllowAnonymous]
public class UserController : BaseController
{
    [HttpGet(ApiRouteConstants.UserGetList)]
    [Authorize(Roles = RoleConstants.Admin)]
    public async Task<IActionResult> GetListAsync([FromQuery] UserListInput input)
    {
        var request = new GetUserListQuery(input.Page, input.PageSize, input.Search, input.Sort, input.SortDirection);
        var userList = await Mediator.Send(request);
        return Ok(userList);
    }

    [HttpGet(ApiRouteConstants.UserGet)]
    public async Task<IActionResult> GetSignedInAsync()
    {
        var request = new GetSignedInUserQuery();
        var signedInUser = await Mediator.Send(request);
        return Ok(signedInUser);
    }

    [HttpPost(ApiRouteConstants.UserSignIn)]
    public async Task<IActionResult> SignInAsync(SignInInput input)
    {
        var request = new SignInCommand(input.UserName, input.Password, input.Remember);
        await Mediator.Send(request);
        return Ok();
    }

    [HttpPost(ApiRouteConstants.UserSignOut)]
    public async Task<IActionResult> SignOutAsync()
    {
        var request = new SignOutCommand();
        await Mediator.Send(request);
        return Ok();
    }

    [HttpPost(ApiRouteConstants.UserSignUp)]
    public async Task<IActionResult> SignUpAsync(SignUpInput input)
    {
        var request = new SignUpCommand(input.FirstName, input.LastName, input.UserName, input.Password);
        await Mediator.Send(request);
        return Ok();
    }
}