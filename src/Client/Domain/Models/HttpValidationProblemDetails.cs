// Copied from: https://github.com/dotnet/aspnetcore/blob/b998a44e59339840d347a61e7275149d9ae5c20a/src/Http/Http.Extensions/src/HttpValidationProblemDetails.cs.
// ReSharper disable All

namespace FinanceLab.Client.Domain.Models;

/// <summary>
///     A <see cref="ProblemDetails" /> for validation errors.
/// </summary>
public class HttpValidationProblemDetails : ProblemDetails
{
    /// <summary>
    ///     Initializes a new instance of <see cref="HttpValidationProblemDetails" />.
    /// </summary>
    public HttpValidationProblemDetails()
        : this(new Dictionary<string, string[]>(StringComparer.Ordinal))
    {
    }

    /// <summary>
    ///     Initializes a new instance of <see cref="HttpValidationProblemDetails" /> using the specified
    ///     <paramref name="errors" />.
    /// </summary>
    /// <param name="errors">The validation errors.</param>
    public HttpValidationProblemDetails(IDictionary<string, string[]> errors)
        : this(new Dictionary<string, string[]>(errors ?? throw new ArgumentNullException(nameof(errors)),
            StringComparer.Ordinal))
    {
    }

    private HttpValidationProblemDetails(Dictionary<string, string[]> errors)
    {
        Title = "One or more validation errors occurred.";
        Errors = errors;
    }

    /// <summary>
    ///     Gets the validation errors associated with this instance of <see cref="HttpValidationProblemDetails" />.
    /// </summary>
    public IDictionary<string, string[]> Errors { get; } = new Dictionary<string, string[]>(StringComparer.Ordinal);
}