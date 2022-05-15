using FinanceLab.Client.Application.Abstractions;
using FinanceLab.Client.Application.Services;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceLab.Client.Application.Extensions;

[PublicAPI]
public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddStateContainerService(this IServiceCollection services)
        => services.AddSingleton<IStateContainerService, StateContainerService>();
}