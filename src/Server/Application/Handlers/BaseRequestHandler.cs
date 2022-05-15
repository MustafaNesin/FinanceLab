using System.Diagnostics.CodeAnalysis;
using System.Net;
using Hellang.Middleware.ProblemDetails;
using JetBrains.Annotations;
using MediatR;

namespace FinanceLab.Server.Application.Handlers;

[PublicAPI]
public abstract class BaseRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);

    [DoesNotReturn]
    public static void Throw(HttpStatusCode httpStatusCode)
        => throw new ProblemDetailsException((int)httpStatusCode);

    [DoesNotReturn]
    public static void Throw(HttpStatusCode httpStatusCode, Exception innerException)
        => throw new ProblemDetailsException((int)httpStatusCode, innerException);

    [DoesNotReturn]
    public static void Throw(HttpStatusCode httpStatusCode, string title)
        => throw new ProblemDetailsException((int)httpStatusCode, title);

    [DoesNotReturn]
    public static void Throw(HttpStatusCode httpStatusCode, string title, Exception innerException)
        => throw new ProblemDetailsException((int)httpStatusCode, title, innerException);
}

[PublicAPI]
public abstract class BaseRequestHandler<TRequest> : BaseRequestHandler<TRequest, Unit>
    where TRequest : IRequest<Unit>
{
}