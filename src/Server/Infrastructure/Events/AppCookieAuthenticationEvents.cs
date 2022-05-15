using System.Security.Claims;
using FinanceLab.Server.Application.Abstractions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;

namespace FinanceLab.Server.Infrastructure.Events;

public class AppCookieAuthenticationEvents : CookieAuthenticationEvents
{
    private readonly IMongoDbContext _dbContext;

    public AppCookieAuthenticationEvents(IMongoDbContext dbContext) => _dbContext = dbContext;

    public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
    {
        // TODO: Elapsed time kontrolü yapılmalı
        var userId = context.Principal?.FindFirstValue(ClaimTypes.NameIdentifier);
        var userTask = _dbContext.Users.Find(u => u.Id == userId).FirstOrDefaultAsync();

        if (userId is not null && await userTask is not null)
            return;

        context.RejectPrincipal();
        await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    public override Task RedirectToLogin(RedirectContext<CookieAuthenticationOptions> context)
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    }

    public override Task RedirectToAccessDenied(RedirectContext<CookieAuthenticationOptions> context)
    {
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        return Task.CompletedTask;
    }
}