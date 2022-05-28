using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace FinanceLab.Client.Domain.Models;

[PublicAPI]
public readonly struct Response<T>
{
    [MemberNotNullWhen(true, nameof(Data))]
    [MemberNotNullWhen(false, nameof(ProblemDetails))]
    public bool IsSuccessful { get; }

    public T? Data { get; }
    public ProblemDetails? ProblemDetails { get; }

    public Response()
        => (IsSuccessful, Data, ProblemDetails) = (true, default, default);

    public Response(T data)
        => (IsSuccessful, Data, ProblemDetails) = (true, data, default);

    public Response(ProblemDetails problemDetails)
        => (IsSuccessful, Data, ProblemDetails) = (false, default, problemDetails);
}