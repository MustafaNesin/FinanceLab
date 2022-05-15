using FinanceLab.Client.Domain.Models;
using JetBrains.Annotations;

namespace FinanceLab.Client.Application.Abstractions;

[PublicAPI]
public interface IHttpClientService
{
    Task<Response<object>> GetAsync(string? requestUri, CancellationToken cancellationToken = default);

    Task<Response<TResponse>> GetAsync<TResponse>(string? requestUri, CancellationToken cancellationToken = default);

    Task<Response<object>> PostAsync(string? requestUri, CancellationToken cancellationToken = default);

    Task<Response<object>> PostAsync<TRequest>(
        string? requestUri, TRequest value, CancellationToken cancellationToken = default);

    Task<Response<TResponse>> PostAsync<TRequest, TResponse>(
        string? requestUri, TRequest value, CancellationToken cancellationToken = default);
}