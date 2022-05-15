using System.Net;
using System.Security.Claims;
using FinanceLab.Server.Application.Abstractions;
using FinanceLab.Server.Domain.Models.Queries;
using FinanceLab.Shared.Domain.Models.Outputs;
using JetBrains.Annotations;
using Mapster;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;

namespace FinanceLab.Server.Application.Handlers.Queries;

[UsedImplicitly]
public sealed class GetSignedInUserQueryHandler : BaseRequestHandler<GetSignedInUserQuery, SignedInUserOutput>
{
    private readonly IMongoDbContext _dbContext;
    private readonly HttpContext _httpContext;

    public GetSignedInUserQueryHandler(IMongoDbContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContext = httpContextAccessor.HttpContext!;
    }

    public override async Task<SignedInUserOutput> Handle(GetSignedInUserQuery request,
        CancellationToken cancellationToken)
    {
        if (_httpContext.User.Identity?.IsAuthenticated != true)
            Throw(HttpStatusCode.Unauthorized, "You are not logged in.");

        var userId = _httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _dbContext.Users
            .Find(u => u.Id == userId)
            .FirstOrDefaultAsync(cancellationToken);

        if (user is not null)
            return user.Adapt<SignedInUserOutput>();

        await _httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        Throw(HttpStatusCode.Unauthorized, "You are not logged in.");

        return default!;
    }
}