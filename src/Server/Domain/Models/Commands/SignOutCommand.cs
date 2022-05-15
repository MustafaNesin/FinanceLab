using MediatR;

namespace FinanceLab.Server.Domain.Models.Commands;

public sealed record SignOutCommand : IRequest;