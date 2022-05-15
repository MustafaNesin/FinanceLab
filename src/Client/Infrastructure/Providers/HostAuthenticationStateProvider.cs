using System.Security.Claims;
using FinanceLab.Client.Application.Abstractions;
using FinanceLab.Shared.Application.Constants;
using FinanceLab.Shared.Domain.Models.Outputs;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;

namespace FinanceLab.Client.Infrastructure.Providers;

public class HostAuthenticationStateProvider : AuthenticationStateProvider, IHostAuthenticationStateProvider
{
    private static readonly TimeSpan UserCacheRefreshInterval = TimeSpan.FromMinutes(1);
    private readonly IHttpClientService _httpClientService;
    private readonly ILogger<HostAuthenticationStateProvider> _logger;
    private ClaimsPrincipal _cachedUser = new(new ClaimsIdentity());
    private DateTimeOffset _userLastCheck = DateTimeOffset.FromUnixTimeSeconds(0);

    public HostAuthenticationStateProvider(IHttpClientService httpClientService,
        ILogger<HostAuthenticationStateProvider> logger)
        => (_httpClientService, _logger) = (httpClientService, logger);

    public void RenewAuthenticationState()
    {
        var authenticationStateTask = GetAuthenticationStateAsync(false);
        NotifyAuthenticationStateChanged(authenticationStateTask);
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync() => GetAuthenticationStateAsync(true);

    private async Task<AuthenticationState> GetAuthenticationStateAsync(bool useCache)
    {
        var now = DateTimeOffset.UtcNow;
        if (useCache && now < _userLastCheck + UserCacheRefreshInterval)
        {
            _logger.LogDebug("Taking user from cache");
            return new AuthenticationState(_cachedUser);
        }

        _logger.LogDebug("Fetching user");
        _cachedUser = await FetchUser();
        _userLastCheck = now;

        return new AuthenticationState(_cachedUser);
    }

    private async Task<ClaimsPrincipal> FetchUser()
    {
        var response = await _httpClientService.GetAsync<SignedInUserOutput>(ApiRouteConstants.UserGet);

        if (!response.IsSuccessful)
            return new ClaimsPrincipal(new ClaimsIdentity()); // Anonymous

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, response.Data.UserName),
            new(ClaimTypes.GivenName, response.Data.FirstName),
            new(ClaimTypes.Surname, response.Data.LastName)
        };

        if (response.Data.Role is not null)
            claims.Add(new Claim(ClaimTypes.Role, response.Data.Role));

        return new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies"));
    }
}