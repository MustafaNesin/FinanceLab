using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Security.Claims;
using FinanceLab.Shared.Application.Abstractions;
using FinanceLab.Shared.Application.Constants;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceLab.Server.Presentation.Controllers;

[Authorize]
[ApiController]
public abstract class BaseController : ControllerBase
{
    private IMediator? _mediator;
    private ISharedResources? _sharedResources;

    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();

    protected ISharedResources L
        => _sharedResources ??= HttpContext.RequestServices.GetRequiredService<ISharedResources>();

    [NonAction]
    protected string EnsureAuthorizationForUserName(string? userName)
    {
        if (userName is null)
            return HttpContext.User.FindFirstValue(ClaimTypes.Name);

        var signedInUserRole = HttpContext.User.FindFirstValue(ClaimTypes.Role);
        var signedInUserName = HttpContext.User.FindFirstValue(ClaimTypes.Name);

        // Giriş yapmış kullanıcı admin değilse başkasının verisiyle işlem yapamaz
        if (signedInUserRole is not RoleConstants.Admin && !userName.Equals(signedInUserName))
            Throw(HttpStatusCode.Unauthorized);

        return userName;
    }

    [NonAction]
    [DoesNotReturn]
    protected static void Throw(HttpStatusCode httpStatusCode)
        => throw new ProblemDetailsException((int)httpStatusCode);

    [NonAction]
    [DoesNotReturn]
    protected static void Throw(HttpStatusCode httpStatusCode, Exception innerException)
        => throw new ProblemDetailsException((int)httpStatusCode, innerException);

    [NonAction]
    [DoesNotReturn]
    protected static void Throw(HttpStatusCode httpStatusCode, string title)
        => throw new ProblemDetailsException((int)httpStatusCode, title);

    [NonAction]
    [DoesNotReturn]
    protected static void Throw(HttpStatusCode httpStatusCode, string title, Exception innerException)
        => throw new ProblemDetailsException((int)httpStatusCode, title, innerException);
}