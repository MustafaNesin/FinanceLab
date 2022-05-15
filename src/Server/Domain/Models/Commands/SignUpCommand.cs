using MediatR;

namespace FinanceLab.Server.Domain.Models.Commands;

public sealed record SignUpCommand(string FirstName, string LastName, string UserName, string Password) : IRequest;