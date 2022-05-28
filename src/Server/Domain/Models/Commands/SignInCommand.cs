using MediatR;

namespace FinanceLab.Server.Domain.Models.Commands;

public sealed record SignInCommand(
    string UserName,
    string Password,
    bool Remember) : IRequest;