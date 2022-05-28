using System.Net;
using FinanceLab.Server.Domain.Models.Commands;
using FinanceLab.Server.Domain.Models.Queries;
using FinanceLab.Shared.Application.Constants;
using FinanceLab.Shared.Domain.Models.Dtos;
using FinanceLab.Shared.Domain.Models.Inputs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceLab.Server.Presentation.Controllers;

[AllowAnonymous]
public sealed class AuthController : BaseController
{
    [HttpPost(ApiRouteConstants.SignIn)]
    public async Task<ActionResult<UserDto>> SignInAsync(SignInInput input)
    {
        if (HttpContext.User.Identity?.IsAuthenticated == true)
            Throw(HttpStatusCode.BadRequest, "You are already logged in.");

        var signInCommand = new SignInCommand(input.UserName, input.Password, input.Remember);
        await Mediator.Send(signInCommand);

        var getUserQuery = new GetUserQuery(input.UserName);
        var user = await Mediator.Send(getUserQuery);

        return Ok(user);
    }

    [Authorize]
    [HttpPost(ApiRouteConstants.SignOut)]
    public async Task<IActionResult> SignOutAsync()
    {
        var request = new SignOutCommand();
        await Mediator.Send(request);

        return Ok();
    }

    [HttpPost(ApiRouteConstants.SignUp)]
    public async Task<IActionResult> SignUpAsync(SignUpInput input)
    {
        if (HttpContext.User.Identity?.IsAuthenticated == true)
            Throw(HttpStatusCode.BadRequest, "You are already logged in.");

        var request = new SignUpCommand(
            input.UserName, input.FirstName, input.LastName, input.Password, input.GameDifficulty);

        await Mediator.Send(request);

        return Ok();
    }
}