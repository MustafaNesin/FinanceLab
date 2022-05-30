using FinanceLab.Server.Application.Handlers.Commands;
using FinanceLab.Server.Domain.Models.Entities;
using FinanceLab.Shared.Application.Validators;
using FinanceLab.Shared.Domain.Models.Dtos;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hellang.Middleware.ProblemDetails;
using JetBrains.Annotations;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceLab.Server.Application.Extensions;

[PublicAPI]
public static class DependencyInjectionExtensions
{
    // ReSharper disable once IdentifierTypo
    public static IServiceCollection AddHandlers(this IServiceCollection services)
        => services.AddMediatR(typeof(SignUpCommandHandler));

    // ReSharper disable once IdentifierTypo
    public static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddSingleton(TypeAdapterConfig.GlobalSettings);
        services.AddScoped<IMapper, ServiceMapper>();
        
        TypeAdapterConfig<Market, MarketDto>.NewConfig()
            .ConstructUsing(market => new MarketDto(market.BaseCoinCode, market.QuoteCoinCode));

        return services;
    }

    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddFluentValidation();
        services.AddValidatorsFromAssemblyContaining<SignInInputValidator>();
        return services;
    }

    public static IServiceCollection AddProblemDetailsWithConventions(this IServiceCollection services)
        => services.AddProblemDetails(options =>
        {
            options.Map<ValidationException>((ctx, ex) =>
            {
                var factory = ctx.RequestServices.GetRequiredService<ProblemDetailsFactory>();
                var errors = ex.Errors
                    .GroupBy(failure => failure.PropertyName)
                    .ToDictionary(
                        group => group.Key,
                        group => group.Select(failure => failure.ErrorMessage).ToArray());

                return factory.CreateValidationProblemDetails(ctx, errors);
            });

            options.MapToStatusCode<NotImplementedException>(StatusCodes.Status501NotImplemented);
            options.MapToStatusCode<HttpRequestException>(StatusCodes.Status503ServiceUnavailable);
            options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
        });
}