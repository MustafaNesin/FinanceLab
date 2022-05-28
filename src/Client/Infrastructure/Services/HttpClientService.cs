using System.Net.Http.Json;
using System.Text.Json;
using FinanceLab.Client.Application.Abstractions;
using FinanceLab.Client.Domain.Models;

namespace FinanceLab.Client.Infrastructure.Services;

public sealed class HttpClientService : IHttpClientService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public HttpClientService(IHttpClientFactory httpClientFactory, JsonSerializerOptions jsonSerializerOptions)
        => (_httpClientFactory, _jsonSerializerOptions) = (httpClientFactory, jsonSerializerOptions);

    public async Task<Response<object>> GetAsync(
        string? requestUri, CancellationToken cancellationToken = default)
    {
        var httpClient = _httpClientFactory.CreateClient();
        using var httpResponse = await httpClient.GetAsync(
            requestUri, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

        return await ConvertHttpResponseAsync(httpResponse, cancellationToken);
    }

    public async Task<Response<TResponse>> GetAsync<TResponse>(
        string? requestUri, CancellationToken cancellationToken = default)
    {
        var httpClient = _httpClientFactory.CreateClient();
        using var httpResponse = await httpClient.GetAsync(
            requestUri, HttpCompletionOption.ResponseHeadersRead, cancellationToken);

        return await ConvertHttpResponseAsync<TResponse>(httpResponse, cancellationToken);
    }

    public async Task<Response<object>> PostAsync(string? requestUri, CancellationToken cancellationToken = default)
    {
        var httpClient = _httpClientFactory.CreateClient();
        var httpResponse = await httpClient.PostAsync(requestUri, default, cancellationToken);

        return await ConvertHttpResponseAsync(httpResponse, cancellationToken);
    }

    public async Task<Response<object>> PostAsync<TRequest>(
        string? requestUri, TRequest value, CancellationToken cancellationToken = default)
    {
        var httpClient = _httpClientFactory.CreateClient();
        using var httpResponse = await httpClient.PostAsJsonAsync(
            requestUri, value, _jsonSerializerOptions, cancellationToken);

        return await ConvertHttpResponseAsync(httpResponse, cancellationToken);
    }

    public async Task<Response<TResponse>> PostAsync<TRequest, TResponse>(
        string? requestUri, TRequest value, CancellationToken cancellationToken = default)
    {
        var httpClient = _httpClientFactory.CreateClient();
        using var httpResponse = await httpClient.PostAsJsonAsync(
            requestUri, value, _jsonSerializerOptions, cancellationToken);

        return await ConvertHttpResponseAsync<TResponse>(httpResponse, cancellationToken);
    }

    private async Task<Response<object>> ConvertHttpResponseAsync(
        HttpResponseMessage httpResponse, CancellationToken cancellationToken)
    {
        if (httpResponse.IsSuccessStatusCode)
            return new Response<object>();

        var problem = await httpResponse.Content
            .ReadFromJsonAsync<HttpValidationProblemDetails>(_jsonSerializerOptions, cancellationToken)
            .ConfigureAwait(false);

        return new Response<object>(problem!);
    }

    private async Task<Response<TResponse>> ConvertHttpResponseAsync<TResponse>(
        HttpResponseMessage httpResponse, CancellationToken cancellationToken)
    {
        if (httpResponse.IsSuccessStatusCode)
        {
            var data = await httpResponse.Content
                .ReadFromJsonAsync<TResponse>(_jsonSerializerOptions, cancellationToken)
                .ConfigureAwait(false);

            return new Response<TResponse>(data!);
        }

        var problem = await httpResponse.Content
            .ReadFromJsonAsync<HttpValidationProblemDetails>(_jsonSerializerOptions, cancellationToken)
            .ConfigureAwait(false);

        return new Response<TResponse>(problem!);
    }
}