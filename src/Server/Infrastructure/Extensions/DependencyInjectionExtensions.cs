using FinanceLab.Server.Domain.Models.Entities;
using FinanceLab.Server.Infrastructure.Abstractions;
using FinanceLab.Server.Infrastructure.Constants;
using FinanceLab.Server.Infrastructure.Events;
using FinanceLab.Server.Infrastructure.Services;
using FinanceLab.Server.Infrastructure.Stores;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceLab.Server.Infrastructure.Extensions;

[PublicAPI]
public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddCookieAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
        services.AddSingleton<ITicketStore, MemoryCacheTicketStore>();
        services.AddScoped<AppCookieAuthenticationEvents>();
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

        services.AddOptions<CookieAuthenticationOptions>(CookieAuthenticationDefaults.AuthenticationScheme)
            .Configure(options => options.EventsType = typeof(AppCookieAuthenticationEvents))
            .Configure<ITicketStore>((o, ticketStore) => o.SessionStore = ticketStore);

        return services;
    }

    public static IServiceCollection AddBinanceService(this IServiceCollection services)
    {
        services.AddHttpClient(BinanceApiConstants.ClientName,
            client => client.BaseAddress = new Uri(BinanceApiConstants.BaseAddress));
        services.AddSingleton<IBinanceService, BinanceService>();

        return services;
    }
}