using JetBrains.Annotations;
using MediatR;

namespace FinanceLab.Server.Domain.Models.Commands;

[PublicAPI]
public sealed record SignInCommand(
    string UserName,
    string Password,
    bool Remember) : IRequest;