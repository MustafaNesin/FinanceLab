using System.Text.Json;
using FinanceLab.Client.Application.Abstractions;
using FinanceLab.Client.Infrastructure.Converters;
using FinanceLab.Client.Infrastructure.Providers;
using FinanceLab.Client.Infrastructure.Services;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FinanceLab.Client.Infrastructure.Extensions;

[PublicAPI]
public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddAuthorization(this IServiceCollection services)
    {
        services.AddAuthorizationCore();
        services.AddSingleton<AuthenticationStateProvider, HostAuthenticationStateProvider>();

        services.AddSingleton<IHostAuthenticationStateProvider>(serviceProvider =>
            (HostAuthenticationStateProvider)serviceProvider.GetRequiredService<AuthenticationStateProvider>());

        return services;
    }

    public static IServiceCollection AddHttpClientService(this IServiceCollection services, string baseAddress)
    {
        services.AddHttpClient(Options.DefaultName, client => client.BaseAddress = new Uri(baseAddress));
        services.AddSingleton<IHttpClientService, HttpClientService>();
        return services;
    }

    public static IServiceCollection AddJsonSerializerOptions(this IServiceCollection services)
        => services.AddSingleton(new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            Converters = { new ProblemDetailsJsonConverter(), new HttpValidationProblemDetailsJsonConverter() }
        });
}