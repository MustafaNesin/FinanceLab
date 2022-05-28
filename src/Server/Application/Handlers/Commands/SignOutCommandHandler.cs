using FinanceLab.Server.Domain.Models.Commands;
using FinanceLab.Shared.Application.Abstractions;
using JetBrains.Annotations;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace FinanceLab.Server.Application.Handlers.Commands;

[UsedImplicitly]
public sealed class SignOutCommandHandler : BaseRequestHandler<SignOutCommand>
{
    private readonly HttpContext _httpContext;

    public SignOutCommandHandler(IHttpContextAccessor httpContextAccessor, ISharedResources sharedResources)
        : base(sharedResources)
        => _httpContext = httpContextAccessor.HttpContext!;

    public override async Task<Unit> Handle(SignOutCommand request, CancellationToken cancellationToken)
    {
        await _httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Unit.Value;
    }
}