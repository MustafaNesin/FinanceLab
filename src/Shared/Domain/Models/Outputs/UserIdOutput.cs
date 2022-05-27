using JetBrains.Annotations;

namespace FinanceLab.Shared.Domain.Models.Outputs;

[PublicAPI]
public sealed record UserIdOutput(string UserId);