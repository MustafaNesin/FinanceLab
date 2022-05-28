using System.Security.Claims;
using FinanceLab.Client.Application.Abstractions;
using FinanceLab.Shared.Application.Constants;
using FinanceLab.Shared.Domain.Models.Dtos;
using FinanceLab.Shared.Domain.Models.Outputs;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;

namespace FinanceLab.Client.Infrastructure.Providers;

public class HostAuthenticationStateProvider : AuthenticationStateProvider, IHostAuthenticationStateProvider
{
    private static readonly TimeSpan UserCacheRefreshInterval = TimeSpan.FromMinutes(1);
    private readonly IHttpClientService _httpClientService;
    private readonly ILogger<HostAuthenticationStateProvider> _logger;
    private readonly IStateContainerService _stateContainerService;
    private ClaimsPrincipal _cachedUser = new(new ClaimsIdentity());
    private DateTimeOffset _userLastCheck = DateTimeOffset.FromUnixTimeSeconds(0);

    public HostAuthenticationStateProvider(IHttpClientService httpClientService,
        IStateContainerService stateContainerService, ILogger<HostAuthenticationStateProvider> logger)
        => (_httpClientService, _stateContainerService, _logger) = (httpClientService, stateContainerService, logger);

    public void SetAuthenticationState(UserDto? user)
    {
        var claimsPrincipal = CreateClaimsPrincipal(user);
        var authenticationState = new AuthenticationState(claimsPrincipal);
        var authenticationStateTask = Task.FromResult(authenticationState);

        _cachedUser = claimsPrincipal;
        _userLastCheck = DateTimeOffset.UtcNow;
        _stateContainerService.SetUser(user, false);

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
        _cachedUser = await FetchUserAsync();
        _userLastCheck = now;

        return new AuthenticationState(_cachedUser);
    }

    private async Task<ClaimsPrincipal> FetchUserAsync()
    {
        var response = await _httpClientService.GetAsync<UserOutput>(ApiRouteConstants.GetUser);
        var user = response.Data?.User;

        _stateContainerService.SetUser(user, false);

        return CreateClaimsPrincipal(user);
    }

    private static ClaimsPrincipal CreateClaimsPrincipal(UserDto? user)
    {
        if (user is null)
            return new ClaimsPrincipal(new ClaimsIdentity()); // Anonymous

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.GivenName, user.FirstName),
            new(ClaimTypes.Surname, user.LastName)
        };

        if (user.Role is not null)
            claims.Add(new Claim(ClaimTypes.Role, user.Role));

        return new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies"));
    }
}