using JetBrains.Annotations;

namespace FinanceLab.Shared.Domain.Models.Outputs;

[PublicAPI]
public sealed record SignedInUserOutput(string UserName, string FirstName, string LastName, string? Role);